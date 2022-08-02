using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

using Neurotec.FaceVerificationClient;

namespace Neurotec.Samples
{
	public class FaceView : Panel
	{
		#region Private constants

		private const string Separator = ",";
		private const string PleaseBlink = "Please blink";
		private const string TurnFaceOnTarget = "Turn face on target";
		private const string TurnFaceToCenter = "Turn face to the center";
		private const string TurnFaceLeft = "Turn face left";
		private const string TurnFaceRight = "Turn face right";
		private const string TurnFaceUp = "Turn face up";
		private const string TurnFaceDown = "Turn face down";
		private const string KeepStill = "Please keep still";
		private const string TurnFaceSideToSide = "Turn face from side to side";
		private const string ScoreFormat = "Score: {0}";
		private const string Blink = "Blink";
		private const string TurnHere = "Turn here";

		private const bool _rotateFaceRectangle = true;
		private const bool _showFaceRectangle = true;
		private const bool _showIcaoArrows = true;

		private const int _faceRectangleWidth = 2;

		#endregion

		#region Public constructors

		public FaceView() : base()
		{
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserPaint, true);

			_centerTransform = new Matrix();
		}

		#endregion

		#region Private readonly fields

		private readonly Color _faceRectangleColor = Color.Green;
		private readonly Color _icaoArrowsColor = Color.Red;
		private readonly Color _livenessItemColor = Color.Yellow;

		#endregion

		#region Private fields

		private Bitmap _image;

		private NCapturePreview _preview;

		private Matrix _centerTransform;

		private bool _zoomToFit = true;

		private float _zoom = 1.0f;

		private int _viewHeight;
		private int _viewWidth;

		#endregion

		#region Public properties

		/// <summary>
		/// Set image to be displayed.
		/// </summary>
		public Bitmap Image
		{
			get { return _image; }
			set
			{
				_image = value;

				if (_image != null)
					DataChanged(_image.Width, _image.Height);
				else
					DataChanged(1, 1);

				Invalidate();
			}
		}

		/// <summary>
		/// Set view to display its image and other properties.
		/// </summary>
		public NCapturePreview Preview
		{
			get { return _preview; }
			set
			{
				_preview = value;
				Image = _preview?.Image;
			}
		}

		public bool ZoomToFit
		{
			get
			{
				return _zoomToFit;
			}
			set
			{
				if (_zoomToFit != value)
				{
					_zoomToFit = value;
					DataChanged(_viewWidth, _viewHeight, true);
					if (value) ZoomViewToFit();
				}
			}
		}

		#endregion

		#region Private static methods

		private static GraphicsPath CreateArrowPath()
		{
			GraphicsPath gp = new GraphicsPath();
			gp.AddBeziers(new PointF[]
				{
					new PointF(32.380941f, 1022.1241f), new PointF(31.942188f, 1015.7183f), new PointF(29.758001f, 1008.0593f), new PointF(34.975683f, 1002.9847f),
					new PointF(63.530331f, 959.92008f), new PointF(92.084969f, 916.85544f), new PointF(120.6396f, 873.7908f), new PointF(90.970216f, 829.04496f),
					new PointF(61.300833f, 784.29911f), new PointF(31.631451f, 739.55327f), new PointF(32.4174f, 735.10024f), new PointF(30.920929f, 728.358f),
					new PointF(33.05677f, 725.25888f), new PointF(40.037212f, 725.44802f), new PointF(47.596572f, 722.95826f), new PointF(53.587747f, 727.72521f),
					new PointF(145.07152f, 773.61583f), new PointF(236.87685f, 818.88016f), new PointF(327.96427f, 865.54932f), new PointF(337.43478f, 881.9015f),
					new PointF(317.96639f, 887.43366f), new PointF(306.82511f, 892.94892f), new PointF(220.62057f, 936.55761f), new PointF(134.10541f, 979.54806f),
					new PointF(47.717131f, 1022.7908f), new PointF(42.608566f, 1022.6599f), new PointF(37.414432f, 1023.1935f), new PointF(32.380941f, 1022.1241f),
				});

			var bounds = gp.GetBounds();
			Matrix m = new Matrix();
			m.Translate(-bounds.X, -bounds.Y);
			gp.Transform(m);

			return gp;
		}

		private static GraphicsPath CreateBlinkPath()
		{
			GraphicsPath gp = new GraphicsPath();
			gp.AddBeziers(new PointF[]
				{
					new PointF(435.85713f, 829.28988f), new PointF(435.85713f, 826.14134f), new PointF(435.85713f, 822.99279f), new PointF(435.85713f, 819.84425f),
					new PointF(431.52818f, 819.40493f), new PointF(427.2124f, 818.65847f), new PointF(422.85713f, 818.61225f), new PointF(420.07381f, 823.2792f),
					new PointF(419.55592f, 829.21363f), new PointF(415.81523f, 833.2958f), new PointF(411.88195f, 832.96632f), new PointF(402.56019f, 832.2983f),
					new PointF(403.59168f, 826.82747f), new PointF(404.49412f, 822.06959f), new PointF(407.38289f, 817.62303f), new PointF(407.23492f, 812.74024f),
					new PointF(403.97397f, 810.47656f), new PointF(400.43818f, 808.64918f), new PointF(397.01142f, 806.65399f), new PointF(393.40481f, 810.75036f),
					new PointF(390.50368f, 815.74657f), new PointF(385.74505f, 818.59108f), new PointF(381.53086f, 816.45821f), new PointF(371.30273f, 811.69373f),
					new PointF(377.16061f, 806.33105f), new PointF(379.68245f, 802.38916f), new PointF(387.71098f, 797.5141f), new PointF(381.91398f, 792.84178f),
					new PointF(379.33556f, 788.89088f), new PointF(373.92253f, 785.09903f), new PointF(374.27125f, 780.28926f), new PointF(378.67829f, 776.95361f),
					new PointF(384.55646f, 773.59246f), new PointF(390.26417f, 775.09226f), new PointF(397.24491f, 778.82048f), new PointF(401.76433f, 785.81843f),
					new PointF(408.41041f, 790.09015f), new PointF(424.88655f, 801.96214f), new PointF(447.55374f, 803.93538f), new PointF(466.25184f, 796.46734f),
					new PointF(476.00331f, 792.64778f), new PointF(483.5214f, 785.18173f), new PointF(491.17644f, 778.33114f), new PointF(495.00098f, 774.23975f),
					new PointF(501.2108f, 773.54521f), new PointF(505.75694f, 776.89247f), new PointF(510.64456f, 778.33781f), new PointF(513.78144f, 782.54911f),
					new PointF(508.97685f, 786.32591f), new PointF(506.39681f, 789.80997f), new PointF(503.5728f, 793.10166f), new PointF(500.83072f, 796.45699f),
					new PointF(504.57117f, 801.05482f), new PointF(508.90555f, 805.26143f), new PointF(511.85713f, 810.42573f), new PointF(508.73325f, 813.98587f),
					new PointF(502.37734f, 823.35014f), new PointF(498.04955f, 816.79531f), new PointF(494.57914f, 813.95975f), new PointF(492.73279f, 806.66381f),
					new PointF(487.41682f, 807.78358f), new PointF(482.55765f, 810.13083f), new PointF(475.96485f, 813.63492f), new PointF(480.85493f, 819.58648f),
					new PointF(482.7528f, 823.71135f), new PointF(486.27353f, 830.95944f), new PointF(479.19952f, 832.07128f), new PointF(475.2306f, 833.59222f),
					new PointF(469.68497f, 836.70712f), new PointF(468.77703f, 830.32197f), new PointF(466.1358f, 826.89057f), new PointF(466.61264f, 818.27641f),
					new PointF(461.63576f, 818.56925f), new PointF(458.07069f, 819.02156f), new PointF(454.49876f, 819.41691f), new PointF(450.92969f, 819.83583f),
					new PointF(450.73884f, 825.93985f), new PointF(450.54798f, 832.04386f), new PointF(450.35713f, 838.14788f), new PointF(445.5238f, 838.34375f),
					new PointF(440.69046f, 838.53963f), new PointF(435.85713f, 838.7355f), new PointF(435.85713f, 835.58696f), new PointF(435.85713f, 832.43842f),
					new PointF(435.85713f, 829.28988f),
				});
			gp.CloseFigure();

			var m = new Matrix();
			m.Scale(0.8f, 1f);
			gp.Transform(m);

			var bounds = gp.GetBounds();
			m = new Matrix();
			m.Translate(-bounds.X, -bounds.Y);
			gp.Transform(m);

			return gp;
		}

		private static GraphicsPath CreateMovePath()
		{
			GraphicsPath gp = new GraphicsPath();
			gp.AddBeziers(new PointF[]
			{
				new PointF(90.985556f, 105.74811f),
				new PointF(90.893313f, 100.83144f), new PointF(90.801069f, 95.914777f), new PointF(90.708826f, 90.998111f),
				new PointF(60.542159f, 90.824651f), new PointF(30.375492f, 90.651191f), new PointF(0.20882478f, 90.477731f),
				new PointF(0.20882478f, 70.491318f), new PointF(0.20882478f, 50.504904f), new PointF(0.20882478f, 30.518491f),
				new PointF(30.375492f, 30.345031f), new PointF(60.542159f, 30.171571f), new PointF(90.708826f, 29.998111f),
				new PointF(91.042159f, 20.116364f), new PointF(91.375493f, 10.234618f), new PointF(91.708826f, 0.35287129f),
				new PointF(121.86204f, 20.160298f), new PointF(152.35889f, 39.459494f), new PointF(182.06471f, 59.936169f),
				new PointF(180.1838f, 64.456897f), new PointF(170.51742f, 68.317666f), new PointF(165.37398f, 72.500777f),
				new PointF(140.85375f, 88.589161f), new PointF(116.44287f, 104.85865f), new PointF(91.634656f, 120.49811f),
				new PointF(91.073952f, 115.61178f), new PointF(91.151712f, 110.6599f), new PointF(90.985556f, 105.74811f)
			});
			gp.CloseFigure();

			gp.AddBeziers(new PointF[]
			{
				new PointF(136.97201f, 88.998111f),
				new PointF(150.74577f, 79.766029f), new PointF(165.06838f, 71.254461f), new PointF(178.13792f, 61.028952f),
				new PointF(176.51483f, 55.95456f), new PointF(166.77604f, 52.673274f), new PointF(161.80061f, 48.407502f),
				new PointF(138.80148f, 33.357215f), new PointF(115.89927f, 18.146483f), new PointF(92.642766f, 3.4981113f),
				new PointF(91.898243f, 12.802028f), new PointF(92.319249f, 22.166338f), new PointF(92.208826f, 31.498111f),
				new PointF(62.208826f, 31.498111f), new PointF(32.208825f, 31.498111f), new PointF(2.2088248f, 31.498111f),
				new PointF(2.2088248f, 50.831444f), new PointF(2.2088248f, 70.164778f), new PointF(2.2088248f, 89.498111f),
				new PointF(32.208825f, 89.498111f), new PointF(62.208826f, 89.498111f), new PointF(92.208826f, 89.498111f),
				new PointF(92.434259f, 98.768627f), new PointF(91.588162f, 108.17654f), new PointF(93.060316f, 117.33144f),
				new PointF(107.78682f, 108.02825f), new PointF(122.34975f, 98.464379f), new PointF(136.97201f, 88.998111f),
			});
			gp.CloseFigure();

			return gp;
		}

		private static GraphicsPath CreatePitchPath()
		{
			GraphicsPath gp = new GraphicsPath();
			gp.AddBeziers(new PointF[]
			{
				new PointF(92.637494f, 45.644319f),
				new PointF(89.836361f, 45.747572f), new PointF(87.035227f, 45.850826f), new PointF(84.234094f, 45.954079f),
				new PointF(83.056664f, 72.994566f), new PointF(80.288415f, 100.28025f), new PointF(72.154982f, 126.19936f),
				new PointF(69.05075f, 134.22219f), new PointF(64.076871f, 141.46749f), new PointF(57.957293f, 147.49047f),
				new PointF(50.720403f, 151.3093f), new PointF(42.237345f, 149.18558f), new PointF(34.413094f, 149.68043f),
				new PointF(29.901904f, 150.65881f), new PointF(27.710166f, 146.69833f), new PointF(32.262636f, 144.3016f),
				new PointF(41.581339f, 132.57566f), new PointF(43.743937f, 117.19692f), new PointF(46.654507f, 102.94613f),
				new PointF(49.90055f, 84.132879f), new PointF(51.284381f, 65.03322f), new PointF(51.439993f, 45.954079f),
				new PointF(46.134972f, 45.08757f), new PointF(37.815334f, 47.048889f), new PointF(34.472932f, 43.696501f),
				new PointF(36.944979f, 37.056662f), new PointF(42.555046f, 32.192593f), new PointF(46.29016f, 26.262502f),
				new PointF(53.091377f, 17.236637f), new PointF(59.679324f, 8.0014404f), new PointF(67.442293f, -0.24157061f),
				new PointF(78.954749f, 12.299236f), new PointF(88.481525f, 26.479469f), new PointF(98.733548f, 40.012635f),
				new PointF(103.57105f, 46.093394f), new PointF(97.110288f, 45.350616f), new PointF(92.637494f, 45.644319f),
			});
			gp.CloseFigure();

			gp.AddBeziers(new PointF[]
			{
				new PointF(51.237593f, 22.859689f),
				new PointF(45.841123f, 30.014342f), new PointF(40.444653f, 37.168996f), new PointF(35.048183f, 44.323649f),
				new PointF(40.922053f, 44.323649f), new PointF(46.795923f, 44.323649f), new PointF(52.669793f, 44.323649f),
				new PointF(52.835981f, 72.341927f), new PointF(49.880496f, 100.50852f), new PointF(42.748509f, 127.63154f),
				new PointF(40.336311f, 135.53069f), new PointF(36.864843f, 143.66489f), new PointF(30.123883f, 148.83097f),
				new PointF(38.42277f, 148.13147f), new PointF(47.108788f, 149.2791f), new PointF(55.112762f, 146.76827f),
				new PointF(65.590255f, 140.51646f), new PointF(69.424411f, 128.00701f), new PointF(73.061263f, 117.10324f),
				new PointF(80.091431f, 93.498569f), new PointF(81.739041f, 68.77288f), new PointF(83.283394f, 44.323649f),
				new PointF(88.790928f, 44.323649f), new PointF(94.298461f, 44.323649f), new PointF(99.805995f, 44.323649f),
				new PointF(89.013082f, 30.014333f), new PointF(78.220106f, 15.705065f), new PointF(67.427193f, 1.3957494f),
				new PointF(62.030683f, 8.5504132f), new PointF(56.634103f, 15.705025f), new PointF(51.237593f, 22.859689f),
			});
			gp.CloseFigure();

			gp.AddBeziers(new PointF[]
				{
					new PointF(28.487453f, 138.88467f),
					new PointF(23.575699f, 146.76073f), new PointF(12.707761f, 140.9363f), new PointF(11.232665f, 133.36272f),
					new PointF(4.9850787f, 120.51548f), new PointF(2.5468592f, 106.16259f), new PointF(1.0057137f, 92.099676f),
					new PointF(3.5952825f, 86.096212f), new PointF(13.006677f, 89.178533f), new PointF(17.898812f, 90.085543f),
					new PointF(20.901598f, 95.651678f), new PointF(19.573598f, 102.64764f), new PointF(22.067121f, 108.59295f),
					new PointF(24.037721f, 116.9629f), new PointF(27.288111f, 124.97652f), new PointF(31.131276f, 132.62783f),
					new PointF(31.56646f, 135.04601f), new PointF(29.852111f, 137.10207f), new PointF(28.487453f, 138.88467f),
				});
			gp.CloseFigure();

			gp.AddBeziers(new PointF[]
				{
					new PointF(27.198983f, 128.45597f),
					new PointF(21.051783f, 117.37429f), new PointF(20.030363f, 104.49296f), new PointF(17.04449f, 92.444202f),
					new PointF(13.378432f, 89.056335f), new PointF(6.4484578f, 90.325661f), new PointF(2.1922269f, 91.275972f),
					new PointF(4.2946756f, 106.93357f), new PointF(7.0325838f, 123.16709f), new PointF(15.09949f, 137.03169f),
					new PointF(18.840249f, 144.12429f), new PointF(31.058067f, 139.62808f), new PointF(29.035871f, 132.20932f),
					new PointF(28.545292f, 130.9009f), new PointF(27.831945f, 129.69624f), new PointF(27.198983f, 128.45597f),
				});
			gp.CloseFigure();

			return gp;
		}

		private static GraphicsPath CreateRollPath()
		{
			GraphicsPath gp = new GraphicsPath();
			gp.AddBeziers(new PointF[]
			{
				new PointF(10.807925f, 297.74808f), new PointF(-1.9961604f, 301.04045f), new PointF(2.1508011f, 290.46635f), new PointF(5.1596062f, 282.04101f),
				new PointF(24.867047f, 210.22816f), new PointF(72.558732f, 147.46938f), new PointF(134.07262f, 106.02947f), new PointF(189.06534f, 68.051168f),
				new PointF(254.76815f, 46.937433f), new PointF(321.27411f, 42.639341f), new PointF(329.47975f, 41.379604f), new PointF(354.27532f, 43.399523f),
				new PointF(333.72507f, 36.617611f), new PointF(322.02949f, 27.764204f), new PointF(295.5855f, 30.343753f), new PointF(296.72086f, 11.70061f),
				new PointF(293.92859f, 1.002931f), new PointF(299.82907f, 3.0737994f), new PointF(306.87798f, 6.9928449f), new PointF(340.67083f, 21.458107f),
				new PointF(375.07219f, 34.581836f), new PointF(408.64422f, 49.497906f), new PointF(373.61609f, 68.486881f), new PointF(338.44047f, 87.363418f),
				new PointF(302.62394f, 104.79237f), new PointF(300.7859f, 99.961284f), new PointF(297.04733f, 86.649414f), new PointF(305.94702f, 84.826045f),
				new PointF(320.23171f, 76.786871f), new PointF(334.94037f, 69.54804f), new PointF(349.16191f, 61.393726f), new PointF(300.03751f, 61.211863f),
				new PointF(250.76437f, 69.760543f), new PointF(205.45816f, 89.09581f), new PointF(118.62501f, 124.7662f), new PointF(45.964726f, 199.44516f),
				new PointF(22.699416f, 291.57096f), new PointF(22.720315f, 299.77646f), new PointF(16.532752f, 297.71274f), new PointF(10.807925f, 297.74808f)
			});

			return gp;
		}

		private static GraphicsPath CreateTargetPath()
		{
			GraphicsPath gp = new GraphicsPath();
			gp.AddArc(new Rectangle(0, 0, 80, 80), 0, 360);
			gp.AddArc(new Rectangle(10, 10, 60, 60), 0, 360);
			gp.AddArc(new Rectangle(20, 20, 40, 40), 0, 360);
			gp.AddArc(new Rectangle(30, 30, 20, 20), 0, 360);

			gp.CloseAllFigures();

			return gp;
		}

		private static GraphicsPath CreateYawPath()
		{
			GraphicsPath gp = new GraphicsPath();
			gp.AddBeziers(new PointF[]
			{
				new PointF(21.301227f, 102.58997f),
				new PointF(14.622369f, 95.61589f), new PointF(7.4074343f, 89.024558f), new PointF(1.560607f, 81.330591f),
				new PointF(9.9877053f, 70.78469f), new PointF(20.062028f, 61.604872f), new PointF(29.521354f, 51.977802f),
				new PointF(33.853491f, 48.686307f), new PointF(38.189633f, 41.749136f), new PointF(42.898643f, 40.930205f),
				new PointF(44.095111f, 47.981002f), new PointF(43.360463f, 55.215785f), new PointF(43.560607f, 62.349121f),
				new PointF(64.688084f, 62.483606f), new PointF(85.814034f, 59.302935f), new PointF(106.21234f, 53.901244f),
				new PointF(117.56727f, 50.728373f), new PointF(128.99898f, 45.835297f), new PointF(136.63498f, 36.482283f),
				new PointF(142.60095f, 30.792949f), new PointF(139.58476f, 44.753767f), new PointF(140.21844f, 48.287646f),
				new PointF(139.86458f, 57.069191f), new PointF(140.8417f, 67.11859f), new PointF(134.07908f, 73.940343f),
				new PointF(126.02983f, 83.922774f), new PointF(113.9664f, 89.281311f), new PointF(101.86608f, 92.546907f),
				new PointF(82.908956f, 98.142052f), new PointF(63.19891f, 100.23304f), new PointF(43.560607f, 101.72328f),
				new PointF(43.369245f, 108.41393f), new PointF(44.07906f, 115.20784f), new PointF(42.890809f, 121.80994f),
				new PointF(38.017995f, 120.72083f), new PointF(33.567038f, 113.68744f), new PointF(29.065518f, 110.26208f),
				new PointF(26.459895f, 107.72252f), new PointF(23.872945f, 105.16384f), new PointF(21.301227f, 102.58997f)
			});
			gp.CloseFigure();

			gp.AddBeziers(new PointF[]
				{
					new PointF(120.50489f, 34.770121f),
					new PointF(109.71223f, 26.910168f), new PointF(96.50135f, 23.566114f), new PointF(83.560607f, 21.207321f),
					new PointF(83.560607f, 14.178891f), new PointF(83.560607f, 7.1504612f), new PointF(83.560607f, 0.12203127f),
					new PointF(98.729554f, 2.0571253f), new PointF(114.00012f, 6.2121434f), new PointF(126.90044f, 14.635636f),
					new PointF(132.95727f, 18.253911f), new PointF(134.56738f, 27.246714f), new PointF(130.32935f, 32.776644f),
					new PointF(127.70526f, 36.669395f), new PointF(124.24263f, 38.902895f), new PointF(120.50489f, 34.770121f),
				});
			gp.CloseFigure();
			return gp;
		}

		private static PointF PreparePath(GraphicsPath gp, float angle, RectangleF playArea, bool invert)
		{
			int border = 2;
			playArea.X += border;
			playArea.Y += border;
			playArea.Height -= border * 2;
			playArea.Width -= border * 2;

			var rectf = gp.GetBounds();

			var scalex = (playArea.Height) / rectf.Height;
			float translateY = playArea.Y;
			float halfPath = rectf.Width * scalex / 2;
			float maxYaw = 35;
			float offset = ((-angle / maxYaw) * playArea.Width / 2 + halfPath);
			float center = playArea.X + playArea.Width / 2;
			float translateX = center - offset;

			Matrix m;
			if (invert)
			{
				m = new Matrix();
				m.Translate(rectf.Width, 0);
				m.Scale(-1, 1);
				gp.Transform(m);
			}

			m = new Matrix();
			m.Translate(translateX, translateY);
			m.Scale(scalex, scalex);
			gp.Transform(m);

			rectf = gp.GetBounds();
			return new PointF(rectf.X + rectf.Width / 2, rectf.Top);
		}

		#endregion

		#region Private methods

		private bool CheckText(Font font)
		{
			// Stop drawing text at some point to avoid Win7 GDI+ error
			return _zoom * font.Size > 1;
		}

		private void DataChanged(int width, int height)
		{
			DataChanged(width, height, false);
		}

		private void DataChanged(int width, int height, bool force)
		{
			bool sizeChanged = _viewWidth != width || _viewHeight != height;

			_viewWidth = width;
			_viewHeight = height;

			if (sizeChanged || force)
			{
				if (AutoSize)
				{
					Size = new Size((int)Math.Ceiling(width * _zoom), (int)Math.Ceiling(height * _zoom));
				}
				else if (!ZoomToFit && AutoScroll)
				{
					AutoScrollMinSize = new Size((int)Math.Ceiling(width * _zoom), (int)Math.Ceiling(height * _zoom));
				}
				else
				{
					ZoomViewToFit();
				}
			}

			Invalidate();
		}

		private void DrawText(Graphics g, string text, Font font, Brush brush, RectangleF layoutRect, StringFormat format)
		{
			if (CheckText(font))
			{
				g.DrawString(text, font, brush, layoutRect, format);
			}
		}

		private void DrawText(Graphics g, string text, Font font, Brush brush, RectangleF layoutRect)
		{
			if (CheckText(font))
			{
				g.DrawString(text, font, brush, layoutRect);
			}
		}

		private void DrawText(Graphics g, string text, Font font, Brush brush, float x, float y)
		{
			if (CheckText(font))
			{
				g.DrawString(text, font, brush, new PointF(x, y));
			}
		}

		private void DrawText(Graphics g, string text, Font font, Brush brush, float x, float y, StringFormat format)
		{
			if (CheckText(font))
			{
				g.DrawString(text, font, brush, new PointF(x, y), format);
			}
		}

		private void DrawText(Graphics g, string text, Font font, Brush brush, PointF point)
		{
			if (CheckText(font))
			{
				g.DrawString(text, font, brush, point);
			}
		}

		private Matrix GetScrollTransformation()
		{
			int w = _viewWidth;
			int h = _viewHeight;
			float x = 0;
			float y = 0;
			if (!ZoomToFit)
			{
				if (w * _zoom > ClientSize.Width)
				{
					x += (w * _zoom - ClientSize.Width) / 2;
				}
				if (h * _zoom > ClientSize.Height)
				{
					y += (h * _zoom - ClientSize.Height) / 2;
				}
			}
			return new Matrix(1, 0, 0, 1, x, y);
		}

		private bool IsAnySet(NFaceVerificationClient.NIcaoWarnings warnings, params NFaceVerificationClient.NIcaoWarnings[] flags)
		{
			return flags != null ? flags.Any(f => (warnings & f) == f) : false;
		}

		private bool IsSet(NFaceVerificationClient.NIcaoWarnings warnings, NFaceVerificationClient.NIcaoWarnings flag)
		{
			return (warnings & flag) == flag;
		}

		private void PrepareGraphics(Graphics g)
		{
			Point pt = ZoomToFit ? new Point(0, 0) : AutoScrollPosition;
			float x = pt.X;
			float y = pt.Y;
			int w = _viewWidth; 
			int h = _viewHeight; 

			ZoomViewToFit();

			x += (ClientSize.Width - w * _zoom) / 2;
			y += (ClientSize.Height - h * _zoom) / 2;

			_centerTransform.Reset();
			_centerTransform.Translate(x, y);
			_centerTransform.Scale(_zoom, _zoom);
			g.MultiplyTransform(_centerTransform, MatrixOrder.Append);
			g.MultiplyTransform(GetScrollTransformation(), MatrixOrder.Append);
		}

		private void PaintIcaoArrows(Graphics g, NFaceVerificationClient.NIcaoWarnings warnings, Rectangle rect, float roll, float yaw)
		{
			if (warnings != NFaceVerificationClient.NIcaoWarnings.None && _showIcaoArrows)
			{
				using (var icaoBrush = new SolidBrush(_icaoArrowsColor))
				using (var icaoPen = new Pen(icaoBrush))
				{
					#region Roll
					if (IsAnySet(warnings, NFaceVerificationClient.NIcaoWarnings.RollLeft, NFaceVerificationClient.NIcaoWarnings.RollRight))
					{
						bool rollLeft = IsSet(warnings, NFaceVerificationClient.NIcaoWarnings.RollLeft);

						var gp = CreateRollPath();
						var bounds = gp.GetBounds();
						var scale = rect.Width / 5.0f / bounds.Width;

						Matrix restore = g.Transform;
						Matrix gm = g.Transform;
						gm.Translate(rect.X, rect.Y);
						gm.RotateAt(roll, new PointF(rect.Width / 2.0f, rect.Height / 2.0f));
						if (rollLeft)
						{
							gm.Translate(rect.Width / 2.0f, rect.Height / 2.0f);
							gm.Scale(-1, 1);
							gm.Translate(-rect.Width / 2.0f, -rect.Height / 2.0f);
						}
						gm.Translate(-bounds.Width / 2.0f * scale, -bounds.Height / 2.0f * scale);
						g.Transform = gm;

						Matrix m = new Matrix();
						m.Scale(scale, scale);
						gp.Transform(m);

						g.FillPath(icaoBrush, gp);
						g.Transform = restore;
					}
					#endregion
					#region Yaw
					if (IsAnySet(warnings, NFaceVerificationClient.NIcaoWarnings.YawLeft, NFaceVerificationClient.NIcaoWarnings.YawRight))
					{
						if ((IsSet(warnings, NFaceVerificationClient.NIcaoWarnings.YawLeft) && !IsSet(warnings, NFaceVerificationClient.NIcaoWarnings.TooEast)) ||
							(IsSet(warnings, NFaceVerificationClient.NIcaoWarnings.YawRight) && !IsSet(warnings, NFaceVerificationClient.NIcaoWarnings.TooWest)) || IsSet(warnings, NFaceVerificationClient.NIcaoWarnings.TooNear))
						{
							bool yawRight = IsSet(warnings, NFaceVerificationClient.NIcaoWarnings.YawRight);

							var gp = CreateYawPath();
							var bounds = gp.GetBounds();
							var scale = rect.Width / 5.0f / bounds.Width;
							var centerX = (bounds.X + bounds.Width) / 2.0f * scale;
							var centerY = (bounds.Y + bounds.Height) / 2.0f * scale;

							float offset = (rect.Width / 5.0f * yaw / 45.0f);

							Matrix restore = g.Transform;
							Matrix gm = g.Transform;
							gm.Translate(rect.X, rect.Y);
							gm.RotateAt(roll, new PointF(rect.Width / 2.0f, rect.Height / 2.0f));
							if (yawRight)
							{
								gm.Translate(rect.Width / 2.0f, rect.Height / 2.0f);
								gm.Scale(-1, 1);
								gm.Translate(-rect.Width / 2.0f, -rect.Height / 2.0f);
								offset *= -1;
							}

							gm.Translate(rect.Width - centerX - offset, rect.Height / 2.0f - centerY);

							g.Transform = gm;

							Matrix m = new Matrix();
							m.Scale(scale, scale);
							gp.Transform(m);

							g.DrawPath(icaoPen, gp);

							g.Transform = restore;
						}
					}
					#endregion
					#region Move
					if (!IsSet(warnings, NFaceVerificationClient.NIcaoWarnings.TooNear))
					{
						if (IsAnySet(warnings, NFaceVerificationClient.NIcaoWarnings.TooSouth, NFaceVerificationClient.NIcaoWarnings.TooNorth, NFaceVerificationClient.NIcaoWarnings.TooEast, NFaceVerificationClient.NIcaoWarnings.TooWest))
						{
							var gp = CreateMovePath();
							var bounds = gp.GetBounds();
							var scale = rect.Width / 5.0f / bounds.Width;
							var centerX = (bounds.X + bounds.Width) / 2.0f * scale;
							var centerY = (bounds.Y + bounds.Height) / 2.0f * scale;

							Matrix m = new Matrix();
							m.Scale(scale, scale);
							gp.Transform(m);

							Matrix restore = g.Transform;
							Matrix gm = g.Transform;
							float cx = rect.Width / 2.0f;
							float cy = rect.Height / 2.0f;

							gm.Translate(rect.X, rect.Y);
							gm.RotateAt(roll, new PointF(cx, cy));
							g.Transform = gm;

							if (IsSet(warnings, NFaceVerificationClient.NIcaoWarnings.TooEast))
							{
								float offset = IsSet(warnings, NFaceVerificationClient.NIcaoWarnings.YawLeft) ? (rect.Width / 5.0f * yaw / 45.0f) : 0;
								float dx = rect.Width + 5 - offset;
								float dy = cy - centerY;
								g.TranslateTransform(dx, dy);
								g.DrawPath(icaoPen, gp);
								g.TranslateTransform(-dx, -dy);
							}
							if (IsSet(warnings, NFaceVerificationClient.NIcaoWarnings.TooWest))
							{
								float offset = IsSet(warnings, NFaceVerificationClient.NIcaoWarnings.YawRight) ? (rect.Width / 5.0f * yaw / 45.0f) : 0;
								Matrix r = g.Transform;
								Matrix m2 = g.Transform;
								m2.Translate(cx, cy);
								m2.Scale(-1, 1);
								m2.Translate(-cx, -cy);
								m2.Translate(rect.Width + 5 + offset, rect.Height / 2.0f - centerY);
								g.Transform = m2;
								g.DrawPath(icaoPen, gp);
								g.Transform = r;
							}
							if (IsSet(warnings, NFaceVerificationClient.NIcaoWarnings.TooSouth))
							{
								Matrix r = g.Transform;
								Matrix m2 = g.Transform;
								m2.RotateAt(-90, new PointF(cx, cy));
								m2.Translate(rect.Height + 5, rect.Width / 2.0f - centerY);
								g.Transform = m2;
								g.DrawPath(icaoPen, gp);
								g.Transform = r;
							}
							if (IsSet(warnings, NFaceVerificationClient.NIcaoWarnings.TooNorth))
							{
								Matrix r = g.Transform;
								Matrix m2 = g.Transform;
								m2.Translate(cx, cy);
								m2.Scale(1, -1);
								m2.Translate(-cx, -cy);
								m2.RotateAt(-90, new PointF(cx, cy));
								m2.Translate(rect.Height + 5, rect.Width / 2.0f - centerY);
								g.Transform = m2;
								g.DrawPath(icaoPen, gp);
								g.Transform = r;
							}

							g.Transform = restore;
						}
					}
					#endregion
					#region Pitch
					if (IsAnySet(warnings, NFaceVerificationClient.NIcaoWarnings.PitchDown, NFaceVerificationClient.NIcaoWarnings.PitchUp))
					{
						if ((IsSet(warnings, NFaceVerificationClient.NIcaoWarnings.PitchDown) && !IsSet(warnings, NFaceVerificationClient.NIcaoWarnings.TooSouth)) ||
							(IsSet(warnings, NFaceVerificationClient.NIcaoWarnings.PitchUp) && !IsSet(warnings, NFaceVerificationClient.NIcaoWarnings.TooNorth)) || IsSet(warnings, NFaceVerificationClient.NIcaoWarnings.TooNear))
						{
							bool up = IsSet(warnings, NFaceVerificationClient.NIcaoWarnings.PitchUp);

							float cx = rect.Width / 2.0f;
							float cy = rect.Height / 2.0f;

							Matrix restore = g.Transform;
							Matrix gm = g.Transform;
							gm.Translate(rect.X, rect.Y);
							gm.RotateAt(roll, new PointF(cx, cy));

							var gp = CreatePitchPath();
							var bounds = gp.GetBounds();
							var scale = rect.Width / 5.0f / bounds.Width;
							var centerX = (bounds.X + bounds.Width) / 2.0f * scale;
							var centerY = (bounds.Y + bounds.Height) / 2.0f * scale;

							if (up)
							{
								gm.Translate(cx, cy);
								gm.Scale(1, -1);
								gm.Translate(-cx, -cy);
							}

							gm.Translate(cx - centerX, -centerY);

							g.Transform = gm;

							Matrix m = new Matrix();
							m.Scale(scale, scale);
							gp.Transform(m);

							g.DrawPath(icaoPen, gp);

							g.Transform = restore;
						}
					}
					#endregion
				}
			}
		}

		private Rectangle ToSystemRectangle(NFaceVerificationClient.NRectangle rect)
		{
			return new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
		}

		private void ZoomViewToFit()
		{
			if (_zoomToFit)
			{
				int w = _viewWidth;
				int h = _viewHeight;
				if (w <= 1 && h <= 1) return;
				float zoomx = w > 0 ? (float)ClientSize.Width / w : 1;
				float zoomy = h > 0 ? (float)ClientSize.Height / h : 1;
				float zoom = Math.Min(zoomx, zoomy);
				if (_zoom != zoom)
				{
					_zoom = zoom;
				}
			}
		}

		#endregion

		#region Protected overridden methods

		protected override void OnPaint(PaintEventArgs pe)
		{
			var g = pe.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;

			if (_image != null)
			{
				PrepareGraphics(g);
				g.DrawImage(_image, 0, 0, _image.Width, _image.Height);

				if (Preview != null)
				{
					using (var pen = new Pen(_faceRectangleColor, _faceRectangleWidth))
					using (var confidenceTextFont = new Font(Font.FontFamily, 10))
					using (var confidenceTextBrush = new SolidBrush(_faceRectangleColor))
					{
						Matrix oldTransform = g.Transform;
						Matrix transform = g.Transform;
						if (_rotateFaceRectangle)
						{
							transform.RotateAt(Preview.Roll,
								new PointF((Preview.BoundingRect.X * 2 + Preview.BoundingRect.Width) / 2.0f,
									(Preview.BoundingRect.Y * 2 + Preview.BoundingRect.Height) / 2.0f));
						}
						g.Transform = transform;

						Rectangle rect = ToSystemRectangle(Preview.BoundingRect);
						if (rect.IsEmpty) return;

						if (_showFaceRectangle)
						{
							var points = new List<Point>(new[]
							{
								new Point(rect.Left, rect.Top),
								new Point(rect.Right, rect.Top),
								new Point(rect.Right, rect.Bottom),
								new Point(rect.Left, rect.Bottom)
							});

							if (Preview.Yaw < 0)
								points.Insert(2, new Point((int)(rect.Right - (rect.Width / 5 * Preview.Yaw / 45)), rect.Top + rect.Height / 2));
							else
								points.Add(new Point((int)(rect.Left - (rect.Width / 5 * Preview.Yaw / 45)), rect.Top + rect.Height / 2));

							g.DrawPolygon(pen, points.ToArray());
						}

						var action = Preview.LivenessAction;
						if (action == NFaceVerificationClient.NLivenessAction.None)
						{
							var msg = string.Format("Y={0}, P={1}, R={2}", Preview.Yaw, Preview.Pitch, Preview.Roll);
							DrawText(g, msg, confidenceTextFont, confidenceTextBrush, rect.X, rect.Bottom + 4);
						}

						bool rotation = action.HasFlag(NFaceVerificationClient.NLivenessAction.RotateYaw);
						bool blink = action.HasFlag(NFaceVerificationClient.NLivenessAction.Blink);
						bool keepStill = action.HasFlag(NFaceVerificationClient.NLivenessAction.KeepStill);
						bool keepRotating = action.HasFlag(NFaceVerificationClient.NLivenessAction.KeepRotatingYaw);
						bool toCenter = action.HasFlag(NFaceVerificationClient.NLivenessAction.TurnToCenter);
						bool left = action.HasFlag(NFaceVerificationClient.NLivenessAction.TurnLeft);
						bool right = action.HasFlag(NFaceVerificationClient.NLivenessAction.TurnRight);
						bool up = action.HasFlag(NFaceVerificationClient.NLivenessAction.TurnUp);
						bool down = action.HasFlag(NFaceVerificationClient.NLivenessAction.TurnDown);
						bool showScore = keepStill;

						if (action != NFaceVerificationClient.NLivenessAction.None)
						{
							byte score = Preview.LivenessScore;
							string text = string.Empty;
							if (blink)
								text = PleaseBlink;
							else if (rotation)
								text = TurnFaceOnTarget;
							else if (keepStill)
								text = KeepStill;
							else if (keepRotating)
								text = TurnFaceSideToSide;
							else if (toCenter)
								text = TurnFaceToCenter;
							else if (left)
								text = TurnFaceLeft;
							else if (right)
								text = TurnFaceRight;
							else if (up)
								text = TurnFaceUp;
							else if (down)
								text = TurnFaceDown;

							if (showScore && score <= 100)
							{
								text += Separator + " " + string.Format(ScoreFormat, score);
							}
							using (var livenessBrush = new SolidBrush(_livenessItemColor))
							{
								var sz = g.MeasureString(text, confidenceTextFont);
								float fx = rect.Left + rect.Width / 2f - sz.Width / 2;
								float fy = rect.Bottom;
								DrawText(g, text, confidenceTextFont, livenessBrush, fx, fy);
							}
						}

						g.Transform = oldTransform;

						if (action != NFaceVerificationClient.NLivenessAction.None)
						{
							var yaw = Preview.Yaw;
							var targetYaw = Preview.LivenessTargetYaw;
							var imageWidth = _image.Width;
							var width = imageWidth / 4f;
							var heigh = width / 8f;
							var x = _image.Height - heigh * 2.5f;

							g.Transform = oldTransform;

							using (var livenessBrush = new SolidBrush(_livenessItemColor))
							using (var livenessPen = new Pen(livenessBrush))
							{
								RectangleF playArea = new RectangleF(1.5f * width, x, width, heigh);
								var text = blink ? Blink : TurnHere;
								var sz = g.MeasureString(text, confidenceTextFont);
								float fx = 0, fy = 0;
								if (rotation)
								{
									PointF center = new PointF();
									g.DrawRectangle(livenessPen, playArea.X, playArea.Y, playArea.Width, playArea.Height);
									if (rotation && !blink)
									{
										var gp = CreateArrowPath();
										PreparePath(gp, yaw, playArea, targetYaw < yaw);
										g.FillPath(livenessBrush, gp);

										gp = CreateTargetPath();
										center = PreparePath(gp, targetYaw, playArea, false);
										g.FillPath(livenessBrush, gp);
									}
									if (blink)
									{
										var gp = CreateBlinkPath();
										center = PreparePath(gp, yaw, playArea, targetYaw < yaw);
										g.FillPath(livenessBrush, gp);
									}

									fx = center.X - sz.Width / 2;
									fy = center.Y - sz.Height;
									DrawText(g, text, confidenceTextFont, livenessBrush, new PointF(fx, fy));
								}
							}
						}

						PaintIcaoArrows(g, Preview.IcaoWarnings, rect, Preview.Roll, Preview.Yaw);
					}
				}
			}

			base.OnPaint(pe);
		}

		#endregion

		#region IDisposable

		protected override void Dispose(bool disposing)
		{
			Preview = null;

			_centerTransform?.Dispose();
			_centerTransform = null;

			base.Dispose(disposing);
		}

		#endregion
	}
}
