using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Topshelf;

namespace WebApplication
{
    public class Program
    {
        public static int Main()
        {
            var exitCode = HostFactory.Run(x =>
                {
                    x.SetStartTimeout(TimeSpan.FromSeconds(120));
                    x.SetStopTimeout(TimeSpan.FromSeconds(120));
                    x.Service<HostingConfiguration>();
                    x.RunAsLocalSystem();
                    x.StartAutomatically();
                    x.SetDisplayName("WebApplication");
                    x.SetServiceName("WebApplication");
                });
            return (int)exitCode;
        }
    }
}