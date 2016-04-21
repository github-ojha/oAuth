using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RevOAuthApp.Common.Configuration
{
	//public class Logger : ILog
	//{
	//	public LogLevel Level { get; private set; }

	//	private readonly ILog _log;

	//	public Logger()
	//	{
	//		if (Environment.UserInteractive)
	//		{
	//			Console.Title = "SelfRegistrationPortal PushHubs";
	//		}

	//		AmbientLog.ConfigureEffective(new FileInfo(RevOAuthConfigurationSection.GetConfig().LogConfigurationFile), false);
	//		_log = AmbientLog.ResolveDedicated(this);
	//	}

	//	public void Write(LogMessage message)
	//	{
	//		_log.Write(message);

	//		if (Environment.UserInteractive)
	//		{
	//			Console.WriteLine(message.Title);
	//		}
	//	}
	//}
}
