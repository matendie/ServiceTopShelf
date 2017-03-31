using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Topshelf;

namespace ConsoleApplication
{
    public class TimeDisplay 
    {
        readonly Timer _timer;
        public TimeDisplay()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is {0} and all is well", DateTime.Now);
        }
        public void Start() { _timer.Start(); } 
        public void Stop() { _timer.Stop(); } 
    }

     
    class Program
    {
        static void Main(string[] args)
        {

            HostFactory.Run(x =>                                 
            {
                x.Service<TimeDisplay>(s =>                        
                {
                    s.ConstructUsing(name => new TimeDisplay());   
                    s.WhenStarted(tc => tc.Start());             
                    s.WhenStopped(tc => tc.Stop());              
                });
                x.RunAsLocalSystem();                            

                x.SetDescription("Sample Topshelf Host TimeDisplay");        
                x.SetDisplayName("Time Display");                       
                x.SetServiceName("TimeDisplay");                       
            });                                                     

        }
    }
}
