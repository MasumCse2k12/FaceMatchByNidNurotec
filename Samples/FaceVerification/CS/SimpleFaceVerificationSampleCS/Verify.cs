using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Neurotec.FaceVerificationClient;
using Neurotec.FaceVerificationServer.Rest.Api;

namespace Neurotec.Samples
{
	public partial class Verify : UserControl, ITab
	{
		#region Public constructor

		public Verify()
		{
			InitializeComponent();

			faceviewLeft = new FaceView
			{
				Dock = DockStyle.Fill,
				BorderStyle = BorderStyle.FixedSingle
			};
			faceviewRight = new FaceView
			{
				Dock = DockStyle.Fill,
				BorderStyle = BorderStyle.FixedSingle
			};

			panel_Middle.Controls.Add(faceviewLeft, 0, 0);
			panel_Middle.Controls.Add(faceviewRight, 1, 0);
		}

		#endregion

		#region Private fields

		private NFaceVerificationClient _nfvc;

		private FaceView faceviewLeft;
		private FaceView faceviewRight;

		private byte[] _fvTemplate;

		private Bitmap _image;

		private int _defaultFar;

		#endregion

		#region Public properties

		public NFaceVerificationClient Nfvc
		{
			get { return _nfvc; }
			set
			{
				_nfvc = value;
				_defaultFar = _nfvc.MatchingThreshold;
				comboBox_FAR.Text = MatchingThresholdToString(_defaultFar);
			}
		}

		public OperationApi Api { get; set; }

		#endregion

		#region Private properties

		private Bitmap Image
		{
			get { return _image; }
			set
			{
				_image = value;
				faceviewLeft.Image = _image;
			}
		}

		#endregion

		#region Public methods

		public void OnTabOpen()
		{
			UpdateSourcesList();
			ResetClient();
			
			Nfvc.CapturePreview += CapturePreview;
		}

		public void OnTabClose()
		{
			Nfvc.CapturePreview -= CapturePreview;
		}

		#endregion

		#region Private methods

		private void Cancel()
		{
			_nfvc.Cancel();
		}

		private NOperationResult CreateTemplate(byte[] image)
		{
			var registrationKey = _nfvc.StartImportImage(image);

			return Validate(_nfvc, Api, registrationKey);
		}

		private string MatchingThresholdToString(int value)
		{
			double p = -value / 12.0;
			return string.Format(string.Format("{{0:P{0}}}", Math.Max(0, (int)Math.Ceiling(-p) - 2)), Math.Pow(10, p));
		}

		private int MatchingThresholdFromString(string value)
		{
			double p = Math.Log10(Math.Max(double.Epsilon, Math.Min(1,
				double.Parse(value.Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "")) / 100)));
			return Math.Max(0, (int)Math.Round(-12 * p));
		}

		private void OnPreviewChanged(NCapturePreview preview)
		{
			if (InvokeRequired)
			{
				if (!IsDisposed && IsHandleCreated)
				{
					BeginInvoke(new Action<NCapturePreview>(OnPreviewChanged), preview);
				}
				else if (preview != null)
				{
					preview.Image.Dispose();
				}
			}
			else
			{
				var tmpPreview = faceviewRight.Preview;
				faceviewRight.Preview = preview;
				tmpPreview?.Image.Dispose();
			}
		}

		private void ResetClient()
		{
			// Set default values
			_nfvc.QualityThreshold = 50;
			_nfvc.CheckIcaoCompliance = false;
			_nfvc.UseManualCapturing = false;
			_nfvc.LivenessMode = NFaceVerificationClient.NLivenessMode.None;
		}

		private void UpdateSourcesList()
		{
			comboBox_sourceSelection.Items.Clear();
			comboBox_sourceSelection.Items.AddRange(_nfvc.AvailableCameras.Concat(new string[] { "NTemplate" }).ToArray());
			comboBox_sourceSelection.SelectedIndex = 0;
		}

		private NOperationResult Validate(NFaceVerificationClient nfvc, OperationApi api, byte[] registrationKey)
		{
			// Register key on the server (connects to server)
			var serverKey = api.Validate(registrationKey);

			// Receive OperationResult
			return nfvc.FinishOperation(serverKey);
		}

		#endregion

		#region Private events

		private void CapturePreview(object sender, NFaceVerificationClient.NCapturePreviewEventHandlerArgs e)
		{
			OnPreviewChanged(e.CapturePreview);
		}

		private void BtnDefaultFARClick(object sender, EventArgs e)
		{
			comboBox_FAR.Text = MatchingThresholdToString(_defaultFar);
		}

		private void BtnOpenfvtemplateClick(object sender, EventArgs e)
		{
			Cancel();
			Image?.Dispose();
			Image = null;
			_fvTemplate = null;

			var filename = Utils.OpenFile("Load face verification template or image", Utils.ImageAndTemplateFilter);
			if (!File.Exists(filename))
			{
				label_viewStatusLeft.Text = "Loaded file: None";
				return;
			}

			try
			{
				var file = File.ReadAllBytes(filename);
				if (Utils.IsFVTemplate(file))
				{
					_fvTemplate = file;
					label_viewStatusLeft.Text = "Loaded template: " + filename;
				}
				else
				{
					Image = Utils.ToBitmap(file);
					label_viewStatusLeft.Text = "Loaded image: " + filename;
					var result = CreateTemplate(file);
					if (result.Status != NFaceVerificationClient.NStatus.Success)
					{
						MessageBox.Show(string.Format("Failed to create template from image with status: {0}", result.Status), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					_fvTemplate = result.Template;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				label_viewStatusLeft.Text = "Loaded file: None";
			}
		}

		private void BtnRefreshListClick(object sender, EventArgs e)
		{
			UpdateSourcesList();
		}

		private void BtnStopClick(object sender, EventArgs e)
		{
			Cancel();
		}

		private async void BtnVerifyClick(object sender, EventArgs e)
		{
			Cancel();
			faceviewRight.Image = null;

			if (_fvTemplate == null)
			{
				MessageBox.Show("Please select Face verification template or image first.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			NOperationResult result = null;
			try
			{
				var selectedOption = (string)comboBox_sourceSelection.SelectedItem;
				label_status.Text = string.Format("Status: Starting verification.");

				switch (selectedOption)
				{
					case "NTemplate":
						{
							var filename = Utils.OpenFile("Load NTemplate", Utils.TemplateFilter);

							if (!File.Exists(filename))
							{
								label_viewStatusRight.Text = "Loaded template: None";
								label_status.Text = "Status: Verification failed. No NTemplate selected.";
								break;
							}

							var nTemplate = File.ReadAllBytes(filename);
							label_viewStatusRight.Text = "Loaded template: " + filename;

							result = _nfvc.Verify(_fvTemplate, nTemplate);
						}
						break;
					default:
						{
							if (!_nfvc.AvailableCameras.Contains(selectedOption))
							{
								MessageBox.Show("Source '{0}' is not available.", selectedOption);
								label_status.Text = "Status: Verification failed. Bad source selected.";
								return;
							}

							_nfvc.CurrentCamera = selectedOption;
							label_viewStatusRight.Text = string.Format("Source: {0}", selectedOption);
							button_Stop.Enabled = true;
							result = await Task.Run(() => _nfvc.Verify(_fvTemplate));
							button_Stop.Enabled = false;
						}
						break;
				}

				if (result == null)
					return;

				if (result.Status == NFaceVerificationClient.NStatus.MatchNotFound)
				{
					label_status.Text = string.Format("Status: No match.");
				}
				else if (result.Status != NFaceVerificationClient.NStatus.Success)
				{
					label_status.Text = string.Format("Status: Verification failed with status '{0}'.", result.Status);
				}
				else
				{
					label_status.Text = string.Format("Status: Matched successfully.");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("Verification failed: {0}", ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ComboBoxFARLeave(object sender, EventArgs e)
		{
			try
			{
				_nfvc.MatchingThreshold = MatchingThresholdFromString(comboBox_FAR.Text);
				comboBox_FAR.Text = MatchingThresholdToString(_nfvc.MatchingThreshold);
			}
			catch
			{
				comboBox_FAR.Select();
				MessageBox.Show(@"FAR is not valid", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void VerifyLoad(object sender, EventArgs e)
		{
			label_status.Text = string.Empty;
			label_viewStatusLeft.Text = string.Empty;
			label_viewStatusRight.Text = string.Empty;
			try
			{
				comboBox_FAR.BeginUpdate();
				comboBox_FAR.Items.Add(0.001.ToString("P1"));
				comboBox_FAR.Items.Add(0.0001.ToString("P2"));
				comboBox_FAR.Items.Add(0.00001.ToString("P3"));
			}
			finally
			{
				comboBox_FAR.EndUpdate();
			}
		}

		#endregion
	}
}
