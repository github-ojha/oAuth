using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;

namespace RevOAuth2App.Runtime.Host
{
	/// <summary>
	/// A service that hosts the VBrick runtime
	/// </summary>
	internal sealed class RuntimeHostService : ServiceBase
	{
		private RuntimeHost _host;

		public RuntimeHostService()
		{
			ServiceName = "VBrick Rev OAuth2 Runtime Host";

			AutoLog = true;
			CanHandlePowerEvent = false;
			CanHandleSessionChangeEvent = false;
			CanPauseAndContinue = false;
			CanShutdown = false;
			CanStop = true;
		}

		protected override void OnStart(string[] args)
		{
			_host = new RuntimeHost();
			_host.Start();
			base.OnStart(args);
		}

		protected override void OnStop()
		{
			_host.Stop();
			_host = null;
			base.OnStop();
		}
	}
}