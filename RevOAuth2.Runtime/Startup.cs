using System;
using Microsoft.Owin;
using System.Web;
using Owin;
using Nancy.Owin;
using RevOAuth2App.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;

namespace RevOAuth2App.Runtime.Host
{    
    public class Startup
    {
        /// <summary>
        /// Nancy Startup Class
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            //Register web application
            //app.MapSignalR();
            app.MapSignalR().UseNancy(new NancyOptions() { Bootstrapper = new RevOAuth2AppBootStrapper() });
            app.UseCors(CorsOptions.AllowAll);
            //app.MapSignalR().UseNancy();
            Console.WriteLine("Register web application..");
           
        }

    }
}
