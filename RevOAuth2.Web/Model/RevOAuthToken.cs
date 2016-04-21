using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RevOAuth2.Web.Model
{
	public class RevOAuthToken
	{
		public string Code { get; set; }
		public string ConnectionId { get; set; }
		public string RevUrl { get; set; }
		public string ApiKey { get; set; }
		public string RedirectUri { get; set; }
	}
}