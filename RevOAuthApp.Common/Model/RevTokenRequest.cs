using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevOAuthApp.Common.Model
{
	public class RevTokenRequest
	{
		public string AuthCode { get; set; }
		public string GrantType { get; set; }
		public string ApiKey { get; set; }
		public string RedirectUri { get; set; }
	}
}
