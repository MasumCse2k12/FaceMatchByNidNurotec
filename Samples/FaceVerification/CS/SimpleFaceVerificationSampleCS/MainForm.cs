using System;
using System.Windows.Forms;

using Neurotec.FaceVerificationClient;
using Neurotec.FaceVerificationServer.Rest.Api;

namespace Neurotec.Samples
{
	public partial class MainForm : Form
	{
		#region Public constructor

		public MainForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Constants

		const string DefaultApiKey = "9tlitadjedrg1emf9e27d0dlkt"; // Trial key
		const string ApiUrl = "http://faceverification.neurotechnology.com/rs/";

		#endregion

		#region Private fields

		private NFaceVerificationClient _nfvc;
		private OperationApi _api;

		private ITab _currentTab;

		#endregion

		#region Private form events

		private void MainFormLoad(object sender, EventArgs e)
		{
			try
			{
				// When using Neurotechnology Cloud service, application id must be set to 1
				_nfvc = new NFaceVerificationClient(1);

				_api = new OperationApi(ApiUrl);
				_api.Configuration.AddApiKey("X-Auth-token", DefaultApiKey);

				var page = new TabPage("Create template");
				var createTemplate = new CreateTemplate { Dock = DockStyle.Fill, Nfvc = _nfvc, Api = _api };
				page.Controls.Add(createTemplate);
				tabControl.TabPages.Add(page);
				_currentTab = createTemplate;
				_currentTab.OnTabOpen();

				page = new TabPage("Verify");
				var verify = new Verify { Dock = DockStyle.Fill, Nfvc = _nfvc, Api = _api };
				page.Controls.Add(verify);
				tabControl.TabPages.Add(page);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
			}
		}

		private void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if (_nfvc != null)
			{
				_nfvc.Cancel();
				_nfvc.Dispose();
			}
		}

		private void TabControlSelecting(object sender, TabControlCancelEventArgs e)
		{
			if (_nfvc != null)
			{
				_nfvc.Cancel();
			}

			_currentTab?.OnTabClose();
			_currentTab = e.TabPage.Controls[0] as ITab;
			_currentTab?.OnTabOpen();
		}

		#endregion
	}
}
