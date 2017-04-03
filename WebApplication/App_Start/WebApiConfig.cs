using Microsoft.Owin.Hosting;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Diagnostics;
using System.Web.Http;
using Topshelf;
using WebApplication.App_Start;

namespace WebApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        internal static HttpConfiguration Register()
        {
            HttpConfiguration config = new HttpConfiguration();
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            return config;
        }
    }

    public class HostingConfiguration : ServiceControl
    {
        private IDisposable _webApplication;
        public bool Start(HostControl hostControl)
        {
            Trace.WriteLine("Starting ConsumerStateService");
            _webApplication = WebApp.Start<OwinConfiguration>("http://localhost:5001");

            
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _webApplication.Dispose();
            return true;
        }
    }
}
