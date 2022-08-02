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
			Console.WriteLine("\t{0} -k [apiKey] -t [nTemplate] -o [outTemplate]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\t-o [outTemplate]  - (required) filename to store created face verification template.");
			Console.WriteLine("\t-k [apiKey] - (optional) authentication token from Neurotechnology cloud service.");
			Console.WriteLine("\t-t [nTemplate] - (optional) filename of NTemplate (if not specified, then template will be created from camera).");
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
			string outTemplatePath;
			string templatePath;

			try
			{
				ParseArgs(args, out outTemplatePath, out templatePath, out apiKey);
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
					NOperationResult result;
					if (templatePath != null)
					{
						Console.WriteLine("Starting template import...");

						var nTemplate = File.ReadAllBytes(templatePath);
						var registrationKey = nfvc.StartImportNTemplate(nTemplate);

						// Validate and get operation result
						result = Validate(nfvc, api, registrationKey);

						Console.WriteLine("Template imported");
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

						// Start capture and template creation
						var registrationKey = nfvc.StartCreateTemplate();
						Console.WriteLine("Face captured");

						// Validate and get operation result
						result = Validate(nfvc, api, registrationKey);
						Console.WriteLine("Template created \nQuality is {0}", result.Quality);
					}

					// Save template to file
					File.WriteAllBytes(outTemplatePath, result.Template);
					Console.WriteLine("Template saved successfully");
				}
				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
		}

		private static void ParseArgs(string[] args, out string outTemplatePath, out string templatePath, out string apiKey)
		{
			outTemplatePath = templatePath = apiKey = null;
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
					case 'o':
						i++;
						outTemplatePath = optarg;
						break;
					case 't':
						i++;
						templatePath = optarg;
						break;
					case 'k':
						i++;
						apiKey = optarg;
						break;
					default:
						throw new ApplicationException("Wrong parameter found!");
				}
			}

			if (outTemplatePath == string.Empty)
				throw new ApplicationException("outTemplatePath - required parameter not specified");
		}

		private static NOperationResult Validate(NFaceVerificationClient nfvc, OperationApi api, byte[] registrationKey)
		{
			// Register key on the server
			var serverKey = api.Validate(registrationKey);

			// Receive OperationResult
			var result = nfvc.FinishOperation(serverKey);

			// Check if status is success
			if (result.Status != NFaceVerificationClient.NStatus.Success)
				throw new Exception(string.Format("Task failed: {0}", result.Status));

			return result;
		}
	}
}
