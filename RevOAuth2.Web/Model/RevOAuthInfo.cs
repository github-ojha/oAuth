using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevOAuthApp.Web.Model
{
	public class RevOAuthInfo
	{
		public string RevUrl { get; set; }
		public string ApiKey { get; set; }
		public string ApiSecret { get; set; }
		public string ConnectionId { get; set; }
		public string RedirectUri { get; set; }
	}
}
