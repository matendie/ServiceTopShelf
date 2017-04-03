using NSwag.AspNet.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.App_Start
{
    public class OwinConfiguration
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWebApi(WebApiConfig.Register());

            var swagSettings = new SwaggerUiOwinSettings();
            swagSettings.DefaultPropertyNameHandling = NJsonSchema.PropertyNameHandling.Default;
            app.UseSwaggerUi(typeof(Startup).Assembly, swagSettings);

        }
    }
}