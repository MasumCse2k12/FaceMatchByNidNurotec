using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Neurotec.FaceVerificationClient;
using Neurotec.FaceVerificationServer.Rest.Api;

namespace Neurotec.Samples
{
	public partial class CreateTemplate : UserControl, ITab
	{
		#region Public constructor

		public CreateTemplate()
		{
			InitializeComponent();

			faceview = new FaceView
			{
				Dock = DockStyle.Fill,
				BorderStyle = BorderStyle.FixedSingle
			};

			icaoWarningsView = new IcaoWarningView();
			icaoWarningsView.Hide();

			panel_middle.Controls.Add(icaoWarningsView, 0, 0);
			panel_middle.Controls.Add(faceview, 1, 0);
		}

		#endregion

		#region Private fields

		private FaceView faceview;
		private IcaoWarningView icaoWarningsView;

		private NFaceVerificationClient _nfvc;

		private int _defaultQualityThreshold;

		private Bitmap _resultImage;
		private Bitmap _resultTokenImage;

		private byte[] _resultTemplate;

		#endregion

		#region Public properties

		public NFaceVerificationClient Nfvc
		{
			get { return _nfvc; }
			set
			{
				_nfvc = value;
				if (_nfvc == null)
					return;
				_defaultQualityThreshold = _nfvc.QualityThreshold;
				textBox_QualityThreshold.Text = _defaultQualityThreshold.ToString();
			}
		}

		public OperationApi Api { get; set; }

		#endregion

		#region Private properties

		private Bitmap ResultImage
		{
			get { return _resultImage; }
			set
			{
				_resultImage = value;
				button_SaveImage.Enabled = _resultImage != null;
				var tmpPreview = faceview.Preview;
				faceview.Preview = null;
				tmpPreview?.Image.Dispose();
				faceview.Image = _resultImage; // Show image
			}
		}

		private Bitmap ResultTokenImage
		{
			get { return _resultTokenImage; }
			set
			{
				_resultTokenImage = value;
				button_SaveTokenImage.Enabled = _resultTokenImage != null;
			}
		}

		private byte[] ResultTemplate
		{
			get { return _resultTemplate; }
			set
			{
				_resultTemplate = value;
				button_SaveTemplate.Enabled = _resultTemplate != null;
			}
		}

		#endregion

		#region Public methods

		public void OnTabOpen()
		{
			UpdateSourcesList();
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

		private void UpdateSourcesList()
		{
			comboBox_Source.Items.Clear();
			comboBox_Source.Items.AddRange(_nfvc.AvailableCameras.Concat(new string[] { "NTemplate", "Image" }).ToArray());
			comboBox_Source.SelectedIndex = 0;
		}

		private void EnableControls(bool enable)
		{
			button_Capture.Enabled = enable;
			button_defaultQualityThreshold.Enabled = enable;
			button_Force.Enabled = !enable && checkBox_ManualCapture.Checked;
			button_RefreshList.Enabled = enable;
			button_Stop.Enabled = false;
			comboBox_LivenessModes.Enabled = enable && checkBox_Liveness.Checked;
			comboBox_Source.Enabled = enable;
			checkBox_ICAO.Enabled = enable;
			checkBox_Liveness.Enabled = enable;
			checkBox_ManualCapture.Enabled = enable;
			textBox_QualityThreshold.Enabled = enable;
		}

		private void ConvertNTemplate()
		{
			try
			{
				var filename = Utils.OpenFile("Open NTemplate", Utils.TemplateFilter);
				if (!File.Exists(filename))
				{
					SetStatus(string.Format("File '{0}' doesn't exist.", filename));
					return;
				}

				var nTemplate = File.ReadAllBytes(filename);
				var registrationKey = _nfvc.StartImportNTemplate(nTemplate);

				var result = Validate(_nfvc, Api, registrationKey);

				if (result.Status != NFaceVerificationClient.NStatus.Success)
				{
					SetStatus(string.Format("Template conversion failed: {0}", result.Status));
					return;
				}

				SetStatus("Template successfully converted.");

				ResultTemplate = result.Template;
				ResultImage = result.Image;
			}
			catch (Exception ex)
			{
				SetStatus(string.Format("Template conversion failed: {0}", ex.Message));
			}
		}

		private void ExtractFromImage()
		{
			try
			{
				var filename = Utils.OpenFile("Open image", Utils.ImageFilter);
				if (!File.Exists(filename))
				{
					SetStatus(string.Format("File '{0}' doesn't exist.", filename));
					return;
				}

				var image = File.ReadAllBytes(filename);
				faceview.Image = Utils.ToBitmap(image);

				var registrationKey = _nfvc.StartImportImage(image);

				var result = Validate(_nfvc, Api, registrationKey);

				if (result.Status != NFaceVerificationClient.NStatus.Success)
				{
					SetStatus(string.Format("Template extraction failed: {0}", result.Status));
					return;
				}

				SetStatus("Template successfully extracted.");

				ResultTemplate = result.Template;
			}
			catch (Exception ex)
			{
				SetStatus(string.Format("Template extraction failed: {0}", ex.Message));
			}
		}

		private async Task CaptureFromCamera()
		{
			button_Stop.Enabled = true;
			try
			{
				var selectedOption = comboBox_Source.SelectedItem.ToString();
				if (!_nfvc.AvailableCameras.Contains(selectedOption))
				{
					MessageBox.Show("Source '{0}' is not available.", selectedOption);
					SetStatus("Capturing failed: Bad source selected.");
					return;
				}

				_nfvc.CurrentCamera = selectedOption;
				SetStatus(string.Format("Capturing with '{0}'", selectedOption));

				// Parameters
				_nfvc.UseManualCapturing = checkBox_ManualCapture.Checked;
				_nfvc.CheckIcaoCompliance = checkBox_ICAO.Checked;
				_nfvc.LivenessMode = checkBox_Liveness.Checked
					? (NFaceVerificationClient.NLivenessMode)Enum.Parse(typeof(NFaceVerificationClient.NLivenessMode), comboBox_LivenessModes.SelectedItem.ToString())
					: NFaceVerificationClient.NLivenessMode.None;
				_nfvc.QualityThreshold = byte.Parse(textBox_QualityThreshold.Text);

				var result = await Task.Run(() =>
				{
					var registrationKey = _nfvc.StartCreateTemplate(); // Camera starts capturing
					return Validate(_nfvc, Api, registrationKey);
				});

				if (result.Status != NFaceVerificationClient.NStatus.Success)
				{
					SetStatus(string.Format("Capturing failed: {0}", result.Status));
					return;
				}

				SetStatus(string.Format("Template created with {0} quality.", result.Quality));

				ResultTemplate = result.Template;
				ResultImage = result.Image;
				ResultTokenImage = result.TokenImage;

				// Show token image instead if it is available
				if (result.TokenImage != null)
				{
					faceview.Image = result.TokenImage;
				}
			}
			catch (Exception ex)
			{
				SetStatus(string.Format("Capturing failed: {0}", ex.Message));
			}
		}

		private void Reset()
		{
			SetStatus("");
			ResultImage = null;
			ResultTemplate = null;
			ResultTokenImage = null;
			faceview.Preview?.Image.Dispose();
			faceview.Preview = null;
			icaoWarningsView.Preview = null;
		}

		private void SetStatus(string status)
		{
			label_Status.Text = string.Format("Status: {0}", status);
		}

		private NOperationResult Validate(NFaceVerificationClient nfvc, OperationApi api, byte[] registrationKey)
		{
			// Register key on the server (connects to server)
			var serverKey = api.Validate(registrationKey);

			// Receive OperationResult
			return nfvc.FinishOperation(serverKey);
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
				var tmpPreview = faceview.Preview;
				faceview.Preview = preview;
				tmpPreview?.Image.Dispose();
				if (checkBox_ICAO.Checked)
					icaoWarningsView.Preview = preview;
			}
		}

		#endregion

		#region Private events

		private void CapturePreview(object sender, NFaceVerificationClient.NCapturePreviewEventHandlerArgs e)
		{
			OnPreviewChanged(e.CapturePreview);
		}

		private void BtnForceClick(object sender, EventArgs e)
		{
			_nfvc.Force();
		}

		private async void BtnCaptureClick(object sender, EventArgs e)
		{
			EnableControls(false);
			Cancel();
			Reset();

			switch (comboBox_Source.SelectedItem.ToString())
			{
				case "NTemplate":
					ConvertNTemplate();
					break;
				case "Image":
					ExtractFromImage();
					break;
				default:
					await CaptureFromCamera();
					break;
			}

			EnableControls(true);
		}

		private void BtnDefaultQualityThresholdClick(object sender, EventArgs e)
		{
			textBox_QualityThreshold.Text = _defaultQualityThreshold.ToString();
		}

		private void BtnSaveTemplateClick(object sender, EventArgs e)
		{
			try
			{
				using (var dialog = new SaveFileDialog())
				{
					dialog.Title = @"Save template";
					dialog.Filter = Utils.TemplateFilter;
					if (dialog.ShowDialog() == DialogResult.OK)
					{
						File.WriteAllBytes(dialog.FileName, ResultTemplate);
					}
					SetStatus("Saved template successfully.");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("Failed to save template: {0}", ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void BtnSaveImageClick(object sender, EventArgs e)
		{
			try
			{
				using (var dialog = new SaveFileDialog())
				{
					dialog.Title = @"Save image";
					dialog.Filter = Utils.ImageFilter;
					if (dialog.ShowDialog() == DialogResult.OK)
					{
						ResultImage.Save(dialog.FileName);
					}
					SetStatus("Saved image successfully.");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("Failed to save image: {0}", ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void BtnSaveTokenImageClick(object sender, EventArgs e)
		{
			try
			{
				using (var dialog = new SaveFileDialog())
				{
					dialog.Title = @"Save image";
					dialog.Filter = Utils.ImageFilter;
					if (dialog.ShowDialog() == DialogResult.OK)
					{
						ResultTokenImage.Save(dialog.FileName);
					}
					SetStatus("Saved image successfully.");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("Failed to save image: {0}", ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void BtnStopClick(object sender, EventArgs e)
		{
			_nfvc.Cancel();
		}

		private void BtnRefreshListClick(object sender, EventArgs e)
		{
			UpdateSourcesList();
		}

		private void CheckBoxManualCaptureCheckedChanged(object sender, EventArgs e)
		{
			if (sender is CheckBox cb)
				_nfvc.UseManualCapturing = cb.Checked;
		}

		private void CheckBoxICAOCheckedChanged(object sender, EventArgs e)
		{
			if (sender is CheckBox cb)
			{
				_nfvc.CheckIcaoCompliance = cb.Checked;
				if (cb.Checked)
					icaoWarningsView.Show();
				else
					icaoWarningsView.Hide();
			}
		}

		private void CheckBoxLivenessCheckedChanged(object sender, EventArgs e)
		{
			if (sender is CheckBox cb)
				comboBox_LivenessModes.Enabled = cb.Checked;
		}

		private void ComboBoxSourceSelectedIndexChanged(object sender, EventArgs e)
		{
			if (sender is ComboBox cb)
			{
				switch (cb.SelectedItem.ToString())
				{
					case "NTemplate":
						button_Capture.Text = "Convert";
						break;
					case "Image":
						button_Capture.Text = "Extract";
						break;
					default:
						button_Capture.Text = "Capture";
						break;
				}
			}
		}

		private void CreateTemplateLoad(object sender, EventArgs e)
		{
			label_Status.Text = string.Empty;
			UpdateSourcesList();

			comboBox_LivenessModes.Items.AddRange(Enum.GetNames(typeof(NFaceVerificationClient.NLivenessMode))
													.Where(_ => !_.Equals("None")).ToArray()); // Don't show specified values.
			comboBox_LivenessModes.SelectedIndex = 0;
		}

		private void TextBoxQualityThresholdTextChanged(object sender, EventArgs e)
		{
			if (System.Text.RegularExpressions.Regex.IsMatch(textBox_QualityThreshold.Text, "[^0-9]"))
			{
				textBox_QualityThreshold.Text = textBox_QualityThreshold.Text.Remove(textBox_QualityThreshold.Text.Length - 1);
			}
			if (textBox_QualityThreshold.Text.Equals(string.Empty))
			{
				textBox_QualityThreshold.Text = "0";
			}
		}

		#endregion
	}
}
