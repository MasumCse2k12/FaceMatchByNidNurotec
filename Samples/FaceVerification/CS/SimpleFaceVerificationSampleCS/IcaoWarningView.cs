using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Neurotec.FaceVerificationClient;

namespace Neurotec.Samples
{
	public partial class IcaoWarningView : UserControl
	{
		#region Private fields

		private NCapturePreview _preview;

		private Color _noWarning = Color.Green;
		private Color _warningColor = Color.Red;
		private Color _indeterminateColor = Color.Orange;

		#endregion

		#region Public constructor

		public IcaoWarningView()
		{
			InitializeComponent();
		}

		#endregion

		#region Public properties

		[DefaultValue(null)]
		public NCapturePreview Preview
		{
			get { return _preview; }
			set
			{
				_preview = value;
				UpdateUI();
				Invalidate();
			}
		}

		public Color NoWarningColor
		{
			get { return _noWarning; }
			set { _noWarning = value; }
		}

		public Color WarningColor
		{
			get { return _warningColor; }
			set { _warningColor = value; }
		}

		public Color IndeterminateColor
		{
			get { return _indeterminateColor; }
			set { _indeterminateColor = value; }
		}

		#endregion

		#region Private methods

		private IEnumerable<Label> GetLabels()
		{
			yield return lblFaceDetected;
			yield return lblExpression;
			yield return lblDarkGlasses;
			yield return lblBlink;
			yield return lblMouthOpen;
			yield return lblLookingAway;
			yield return lblRedEye;
			yield return lblFaceDarkness;
			yield return lblUnnaturalSkinTone;
			yield return lblColorsWashedOut;
			yield return lblPixelation;
			yield return lblSkinReflection;
			yield return lblGlassesReflection;
			yield return lblRoll;
			yield return lblYaw;
			yield return lblPitch;
			yield return lblTooClose;
			yield return lblTooFar;
			yield return lblTooNorth;
			yield return lblTooSouth;
			yield return lblTooEast;
			yield return lblTooWest;
			yield return lblSharpness;
			yield return lblGrayscaleDensity;
			yield return lblSaturation;
			yield return lblBackgroundUniformity;
		}

		private Color GetColorForFlags(NFaceVerificationClient.NIcaoWarnings warnings, params NFaceVerificationClient.NIcaoWarnings[] flags)
		{
			return flags.Any(f => warnings.HasFlag(f)) ? WarningColor : NoWarningColor;
		}

		private void UpdateUI()
		{
			if (_preview != null)
			{
				var warnings = _preview.IcaoWarnings;
				if (warnings.HasFlag(NFaceVerificationClient.NIcaoWarnings.FaceNotDetected))
				{
					foreach (var lbl in GetLabels())
					{
						lbl.ForeColor = IndeterminateColor;
					}
					lblFaceDetected.ForeColor = WarningColor;
				}
				else
				{
					lblFaceDetected.ForeColor = NoWarningColor;
					lblExpression.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.Expression);
					lblDarkGlasses.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.DarkGlasses);
					lblBlink.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.Blink);
					lblMouthOpen.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.MouthOpen);
					lblLookingAway.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.LookingAway);
					lblRedEye.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.RedEye);
					lblFaceDarkness.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.FaceDarkness);
					lblUnnaturalSkinTone.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.UnnaturalSkinTone);
					lblColorsWashedOut.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.WashedOut);
					lblPixelation.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.Pixelation);
					lblSkinReflection.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.SkinReflection);
					lblGlassesReflection.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.GlassesReflection);

					lblRoll.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.RollLeft, NFaceVerificationClient.NIcaoWarnings.RollRight);
					lblYaw.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.YawLeft, NFaceVerificationClient.NIcaoWarnings.YawRight);
					lblPitch.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.PitchDown, NFaceVerificationClient.NIcaoWarnings.PitchUp);
					lblTooClose.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.TooNear);
					lblTooFar.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.TooFar);
					lblTooNorth.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.TooNorth);
					lblTooSouth.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.TooSouth);
					lblTooWest.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.TooWest);
					lblTooEast.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.TooEast);

					lblSharpness.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.Sharpness);
					lblSaturation.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.Saturation);
					lblGrayscaleDensity.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.GrayscaleDensity);
					lblBackgroundUniformity.ForeColor = GetColorForFlags(warnings, NFaceVerificationClient.NIcaoWarnings.BackgroundUniformity);
				}
			}
			else
			{
				foreach (var lbl in GetLabels())
				{
					lbl.ForeColor = NoWarningColor;
				}
			}
		}

		#endregion

		#region Protected methods

		protected override void Dispose(bool disposing)
		{
			_preview = null;

			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#endregion
	}
}
