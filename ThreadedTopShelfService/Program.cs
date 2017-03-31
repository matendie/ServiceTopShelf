using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Runtime;

namespace ThreadedTopShelfService
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.StartAutomatically(); // Start the service automatically

                x.EnableServiceRecovery(rc =>
                {
                    rc.RestartService(1); // restart the service after 1 minute
                });


                x.Service<MyService>(s =>
                {
                    s.ConstructUsing(hostSettings => new MyService(hostSettings));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("MyDescription");
                x.SetDisplayName("MyDisplayName");
                x.SetServiceName("MyServiceName");

            });
        }
    }

    public class MyService
    {
        public MyService(HostSettings settings)
        {
        }

        private SemaphoreSlim _semaphoreToRequestStop;
        private Thread _thread;

        public void Start()
        {
            _semaphoreToRequestStop = new SemaphoreSlim(0);
            _thread = new Thread(DoWork);
            _thread.Start();
        }

        public void Stop()
        {
            _semaphoreToRequestStop.Release();
            _thread.Join();
        }

        private void DoWork()
        {
            while (true)
            {
                Console.WriteLine("doing work..");
                if (_semaphoreToRequestStop.Wait(500))
                {
                    Console.WriteLine("Stopped");
                    break;
                }
            }
        }
    }
}
