using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Neurotec.Samples
{
	public static class Utils
	{
		#region Private static fields

		private static readonly OpenFileDialog _openFileDialog = new OpenFileDialog();

		#endregion

		#region Public constants

		public const string ImageFilter = "Image files (*.png, *.jpg, *.jpeg, *.jpe, *.jfif) | *.png; *.jpg; *.jpeg; *.jpe; *.jfif |All files(*.*)|*.*";
		public const string TemplateFilter = "Face template files(*.dat)|*.dat|All files(*.*)|*.*";
		public const string ImageAndTemplateFilter = "Image or face template files (*.png, *.jpg, *.jpeg, *.jpe, *.jfif, *.dat) | *.dat; *.png; *.jpg; *.jpeg; *.jpe; *.jfif" 
													+ "|Image files(*.png, *.jpg, *.jpeg, *.jpe, *.jfif) | *.png; *.jpg; *.jpeg; *.jpe; *.jfif" 
													+ "|Face template files(*.dat)|*.dat"
													+ "|All files(*.*)|*.*";

		#endregion

		#region Public static methods

		public static bool IsFVTemplate(byte[] template)
		{
			return template[0] == 70 && template[1] == 86; // Checks if file starts with "FV" symbols.
		}

		public static string OpenFile(string title, string filter)
		{
			_openFileDialog.Reset();
			_openFileDialog.Title = title;
			_openFileDialog.Filter = filter;
			if (_openFileDialog.ShowDialog() == DialogResult.OK)
			{
				return _openFileDialog.FileName;
			}
			return string.Empty;
		}

		public static Bitmap ToBitmap(byte[] imageData)
		{
			using (var ms = new MemoryStream(imageData))
			{
				return new Bitmap(ms);
			}
		}

		#endregion
	}
}
