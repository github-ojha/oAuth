using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;

namespace RevOAuth2App.Runtime.Host
{
	/// <summary>
	/// Installs a service which hosts the VBrick runtime
	/// </summary>
	[RunInstaller(true)]
	public sealed class RuntimeHostServiceInstaller : Installer
	{
		public RuntimeHostServiceInstaller()
		{
			Installers.AddRange(new Installer[]
			{
				new ServiceProcessInstaller
				{
					Account = ServiceAccount.LocalSystem,
					Password = null,
					Username = null
				},
				new System.ServiceProcess.ServiceInstaller
				{
					ServiceName = "VBrick RevOAuth2App Runtime Host",
					StartType = ServiceStartMode.Automatic
				}
			});
		}
	}
}