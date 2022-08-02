using System;
using System.IO;

using Neurotec.FaceVerificationClient;
using Neurotec.FaceVerificationServer.Rest.Api;

namespace Neurotec.Tutorials
{
	class Program
	{
		const string DefaultApiKey = "9tlitadjedrg1emf9e27d0dlkt";
		const string ApiUrl = "http://faceverification.neurotechnology.com/rs/";

		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} -k [apiKey] -t [fvTemplate] -n [ntemplate]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\t-t [fvTemplate] - (required) filename of stored face verification template.");
			Console.WriteLine("\t-k [apiKey] - (optional) authentication token from Neurotechnology cloud service.");
			Console.WriteLine("\t-n [nTemplate] - (optional) filename of NTemplate (if not specified, then verification will done from camera).");
			Console.WriteLine();

			return 1;
		}

		static int Main(string[] args)
		{
			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 1)
			{
				return Usage();
			}

			string apiKey;
			string fvTemplatePath;
			string nTemplatePath;

			try
			{
				ParseArgs(args, out fvTemplatePath, out nTemplatePath, out apiKey);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: {0}", ex);
				Usage();
				return -1;
			}

			try
			{
				// Create new operation api instance and configure authentication
				var api = new OperationApi(ApiUrl);
				api.Configuration.AddApiKey("X-Auth-token", apiKey ?? DefaultApiKey);

				// Create new NFaceVerificationClient instance
				// When using Neurotechnology Cloud service, application id must be set to 1
				using (var nfvc = new NFaceVerificationClient(1))
				{
					var fvTemplate = File.ReadAllBytes(fvTemplatePath);

					NOperationResult result;
					if (nTemplatePath != null)
					{
						var nTemplate = File.ReadAllBytes(nTemplatePath);

						Console.WriteLine("Performing verfication using NTemplate");
						result = nfvc.Verify(fvTemplate, nTemplate);
					}
					else
					{
						if (nfvc.AvailableCameras.Length == 0)
						{
							Console.WriteLine("No cameras available. Exiting...");
							return -1;
						}

						if (nfvc.AvailableCameras.Length == 1)
						{
							// Set camera
							nfvc.CurrentCamera = nfvc.AvailableCameras[0];
						}
						else
						{
							Console.WriteLine("All available cameras:");
							int i = 1;
							foreach (var camera in nfvc.AvailableCameras)
							{
								Console.WriteLine("{0}) {1}", i++, camera);
							}

							Console.Write("Input camera number you want to use: ");
							i = int.Parse(Console.ReadLine());

							// Set camera
							nfvc.CurrentCamera = nfvc.AvailableCameras[i - 1];
						}

						Console.WriteLine("Camera selected: {0}", nfvc.CurrentCamera);
						Console.WriteLine("Starting capture... \nPlease face the camera");

						// Start capture and verification
						result = nfvc.Verify(fvTemplate);
						Console.WriteLine("Face captured \nQuality is {0}", result.Quality);
					}

					if(result.Status != NFaceVerificationClient.NStatus.Success)
					{
						Console.WriteLine("Verification failed with status: {0}", result.Status);
						return -1;
					}
					Console.WriteLine("Verification was successful");
				}
				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
		}

		private static void ParseArgs(string[] args, out string fvTemplatePath, out string ntemplatePath, out string apiKey)
		{
			fvTemplatePath = ntemplatePath = apiKey = null;
			for (int i = 0; i < args.Length; i++)
			{
				string optarg = string.Empty;

				if (args[i].Length != 2 || args[i][0] != '-')
				{
					throw new ApplicationException("Parameter parse error");
				}

				if (args.Length > i + 1 && args[i + 1][0] != '-')
				{
					optarg = args[i + 1]; // we have a parameter for given flag
				}

				if (optarg == string.Empty)
				{
					throw new ApplicationException("Parameter parse error");
				}

				switch (args[i][1])
				{
					case 't':
						i++;
						fvTemplatePath = optarg;
						break;
					case 'n':
						i++;
						ntemplatePath = optarg;
						break;
					case 'k':
						i++;
						apiKey = optarg;
						break;
					default:
						throw new ApplicationException("Wrong parameter found!");
				}
			}

			if (fvTemplatePath == string.Empty)
				throw new ApplicationException("outTemplatePath - required parameter not specified");
		}
	}
}
