using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Bootstrapper;
using RevOAuth2.Web.Hub;

using RevOAuthApp.Common.Configuration;

namespace RevOAuth2App.Web
{
	public class RevOAuth2AppBootStrapper : DefaultNancyBootstrapper
	{
		/// <summary>
		///  configure Application Container
		/// </summary>
		/// <param name="container"></param>
		protected override void ConfigureApplicationContainer(Nancy.TinyIoc.TinyIoCContainer container)
		{

			//container.Register<ILog, Logger>().AsSingleton();
			//container.Register<INetworkApi, NetworkAPI>();
			container.Register<IPushHub, PushHub>().AsSingleton();
			base.ConfigureApplicationContainer(container);
		}
		/// <summary>
		/// Application Startup event
		/// </summary>
		/// <param name="container"></param>
		/// <param name="pipelines"></param>
		protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
		{
			base.ApplicationStartup(container, pipelines);
		}

		protected override Nancy.Diagnostics.DiagnosticsConfiguration DiagnosticsConfiguration
		{
			get
			{
				return base.DiagnosticsConfiguration;

			}
		}

		/// <summary>
		/// View/Model Configuration
		/// </summary>
		/// <param name="nancyConventions"></param>
		protected override void ConfigureConventions(Nancy.Conventions.NancyConventions nancyConventions)
		{
			nancyConventions.StaticContentsConventions.Add(Nancy.Conventions.StaticContentConventionBuilder.AddDirectory("Scripts", @"/Content"));
			nancyConventions.ViewLocationConventions.Insert(0, (viewName, model, context) =>
			{
				if (string.IsNullOrWhiteSpace(context.ModulePath))
					return null;
				return string.Concat("views/", context.ModuleName, "/", viewName);
			});

			base.ConfigureConventions(nancyConventions);

		}
		/// <summary>
		/// Application Startup event
		/// </summary>
		/// <param name="container"></param>
		/// <param name="pipelines"></param>
		/// <param name="context"></param>
		protected override void RequestStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines, NancyContext context)
		{
			base.RequestStartup(container, pipelines, context);

			pipelines.BeforeRequest += (ctx) =>
			{
				return null;
			};

			pipelines.AfterRequest += (ctx) =>
			{
			};

			//Handling Error here
			pipelines.OnError += (ctx, err) =>
			{
				return ctx.Response;
			};
		}
	}
}