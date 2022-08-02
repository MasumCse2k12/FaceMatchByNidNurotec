using System;
using System.Diagnostics;
using System.Reflection;

namespace Neurotec.Tutorials
{
	public static class TutorialUtils
	{
		public static void PrintTutorialHeader(string [] args)
		{
			string description = ((AssemblyDescriptionAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0]).Description;
			string version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
			string copyright = ((AssemblyCopyrightAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright;
			Console.WriteLine(GetAssemblyName());
			Console.WriteLine("");
			Console.WriteLine(@"{0} (Version: {1})", description, version);
			Console.WriteLine(copyright.Replace("?", "(C)"));
			Console.WriteLine();
			if (args != null && args.Length > 0)
			{
				Console.WriteLine("Arguments:");
				foreach (string item in args)
				{
					Console.WriteLine("\t{0}", item);
				}
				Console.WriteLine();
			}
		}

		public static string GetAssemblyName()
		{
			return Assembly.GetExecutingAssembly().GetName().Name;
		}

		public static int PrintException(Exception ex)
		{
			Console.WriteLine(ex);

			while ((ex is AggregateException) && (ex.InnerException != null))
				ex = ex.InnerException;

			if(ex != null)
				Console.WriteLine(ex);

			return -1;
		}
	}
}
