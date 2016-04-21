using Microsoft.Owin.Hosting;
using RevOAuth2App.Runtime.Host;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace RevOAuth2App.Runtime
{
	class Program
	{
		private static string _webURL = string.Empty;
		private static string _signalRUrl = string.Empty;
		static void Main(string[] args)
		{
			RevOAuthApp.Common.Configuration.RevOAuthConfigurationSection _config = RevOAuthApp.Common.Configuration.RevOAuthConfigurationSection.GetConfig();
			_webURL = _config.Url;
			
			if (args == null || args.Length == 0)
			{
				RunWindowsService();
			}
			else if (args[0] == "/console")
			{
				RunConsole();
			}
			else if (args[0] == "/deploy" && args.Length == 2 && args[1] != "")
			{
				//Deploy(args[1]);
			}
			else if (args[0] == "/install")
			{
				InstallService();
			}
			else if (args[0] == "/uninstall")
			{
				UninstallService();
			}
			else
			{
				Console.WriteLine("Unrecognized argument(s)");
			}
			Console.ReadLine();

		}


		private static void InstallService()
		{
			var installer = new AssemblyInstaller(typeof(Program).Assembly, null);
			var savedState = new Hashtable();
			installer.Install(savedState);
			installer.Commit(savedState);
		}

		private static void UninstallService()
		{
			// Should the saved state come from a file saved during the install?

			var installer = new AssemblyInstaller(typeof(Program).Assembly, null);
			var savedState = new Hashtable();

			installer.Uninstall(savedState);
		}

		private static void RunWindowsService()
		{
			Directory.SetCurrentDirectory(Assembly.GetExecutingAssembly().GetDirectory());

			ServiceBase.Run(new RuntimeHostService());
		}


		private static void RunConsole()
		{

			//var config = new HostConfiguration()
			//{
			//    UrlReservations = new UrlReservations() { CreateAutomatically = true }
			//};

			//using (var webHost = new Nancy.Hosting.Self.NancyHost(
			//    config,
			//    new Uri(_webURL)))
			//{
			using (WebApp.Start<Startup>(_webURL))
			{
				//webHost.Start();
				Console.WriteLine("Application running on {0}", _webURL);
				Console.Write("Press any key to stop!");
				Console.ReadKey();
				//webHost.Stop();
			}
			//}

		}
	}
}
