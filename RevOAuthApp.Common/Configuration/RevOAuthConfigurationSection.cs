using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevOAuthApp.Common.Configuration
{
	public class RevOAuthConfigurationSection : ConfigurationSection
	{
		[ConfigurationProperty("utilityName", IsRequired = true)]
		public string UtilityName
		{
			get { return (string)this["utilityName"]; }
			set { this["utilityName"] = value; }
		}

	
		[ConfigurationProperty("logLevel", IsRequired = true)]
		public string LogLevel
		{
			get { return (string)this["logLevel"]; }
			set { this["logLevel"] = value; }
		}

		

		[ConfigurationProperty("url", IsRequired = true)]
		public string Url
		{
			get { return (string)this["url"]; }
			set { this["url"] = value; }
		}

		[ConfigurationProperty("logConfigurationFile", IsRequired = true)]
		public string LogConfigurationFile
		{
			get { return (string)this["logConfigurationFile"]; }
			set { this["logConfigurationFile"] = value; }
		}
		

		
		public static RevOAuthConfigurationSection GetConfig()
		{
			return ConfigurationManager.GetSection("revOAuthConfigurationSettings") as RevOAuthConfigurationSection;
		}
	}
	
}
