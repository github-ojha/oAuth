using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using Nancy.ModelBinding;
using System.Threading.Tasks;


namespace RevOAuth2App.Web.Modules
{
    /// <summary>
    /// Home Module
    /// To get initial request
    /// </summary>
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
			Get["/"] = p => View["Home.html"];

			Get["/oauth"] = p => View["Home.html"]; 
        }
       
    }
}