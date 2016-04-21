using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;

using Owin;
using RevOAuth2App.Runtime.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace RevOAuth2App.Runtime
{


    public class RuntimeHost
    {
        IDisposable _webApp;
        //NancyHost _host;          
        public bool Start(bool restart = false)
        {            
            onStart();
            return true;
        }
        private void onStart()
        {
          
            //var config = new HostConfiguration()
            //{
            //    UrlReservations = new UrlReservations() { CreateAutomatically = true }
            //};

            //_host = new Nancy.Hosting.Self.NancyHost(config, new Uri(_config.Url));
            //_host.Start();
			RevOAuthApp.Common.Configuration.RevOAuthConfigurationSection _config = RevOAuthApp.Common.Configuration.RevOAuthConfigurationSection.GetConfig();
			var _webURL = _config.Url;
            _webApp = WebApp.Start<Startup>(_webURL);
        }
        public bool Restart()
        {
            onStart();
            return true;
        }

        public bool Stop()
        {
            if (_webApp == null)
            {
                return false;
            }

            //_host.Stop();
            _webApp = null;
            return true;
        }

    }

}
