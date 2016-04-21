using RevOAuth2.Web.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ModelBinding;
using RevOAuthApp.Web.Model;
using RevOAuthApp.Common;
using System.Text;
using RevOAuth2.Web.Model;


namespace RevOAuth2.Web.Modules
{
	public class RevOAuthModule : Nancy.NancyModule
	{
		//private ILog _log;
		public RevOAuthModule(IPushHub pushub)
			: base("RevOAuth")
		{

			Post["/"] = p =>
			{
				//_log = log;
				var revoauth = this.Bind<RevOAuthInfo>();
				if (!string.IsNullOrEmpty(revoauth.ConnectionId))
				{
					pushub.ConnectionId = revoauth.ConnectionId;
				}

				RevOAuthRequest revOAuthReq = new RevOAuthRequest();
				string revAuthUrl = revOAuthReq.GetRevOAuthRequestUrl(revoauth.RevUrl.TrimEnd('/'), revoauth.ApiKey, revoauth.ApiSecret, revoauth.RedirectUri.TrimEnd('/'));
				if (revOAuthReq.Error)
				{
					pushub.SendError(revAuthUrl);
				}
				return revAuthUrl;
			};

			Get["/HostUrl"] = p =>
			{
				return RevOAuthApp.Common.Configuration.RevOAuthConfigurationSection.GetConfig().Url;
			};
			
			Post["/ConnectionID"] = p =>
			{
				var pushHubConnection = this.Bind<PushHubConnection>();
				pushub.ConnectionId = pushHubConnection.Connection;
				return Negotiate.WithStatusCode(Nancy.HttpStatusCode.OK);
			};

			Post["/RevToken"] = p =>
			{

				//_log = log;
				var revoauth = this.Bind<RevOAuthToken>();
				if (!string.IsNullOrEmpty(revoauth.ConnectionId))
				{
					pushub.ConnectionId = revoauth.ConnectionId;
				}

				RevOAuthRequest revOAuthReq = new RevOAuthRequest();

				string revAuthToken = string.Empty;
				if (!string.IsNullOrEmpty(revoauth.Code))
				{
					revAuthToken = revOAuthReq.GetRevOAuthToken(revoauth.ApiKey, revoauth.RedirectUri, revoauth.Code, revoauth.RevUrl);
				}
				else
				{
					pushub.SendError("Not able to get code!");
				}

				if (revOAuthReq.Error)
				{
					pushub.SendError(revAuthToken);
				}
				return revAuthToken;
			};
		}
                   
	}
}