using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text; 
using Topshelf;

namespace SimpleService
{

    public class SimpleService
    {
        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1";
        TcpListener listener;
        TcpClient client;

        
        public void Start()
        {
            IPAddress localAdd = IPAddress.Parse(SERVER_IP);
            listener = new TcpListener(localAdd, PORT_NO);
            listener.Start();
            Console.WriteLine("Listening...");

            while (true)
            {                
                ReadMessage();
            }
        }
        public void Stop()
        {
            client.Close();
            listener.Stop();
            Console.WriteLine("Service stopped");
            //Console.ReadLine();
            Environment.Exit(0);
            
        }

        public void ReadMessage()
        {
            client = listener.AcceptTcpClient();
            NetworkStream nwStream = client.GetStream();
            byte[] buffer = new byte[client.ReceiveBufferSize];

            //---read incoming stream---
            int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

            //---convert the data received into a string---
            string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received : " + dataReceived);

            //---write back the text to the client---
            Console.WriteLine("Sending back : " + dataReceived);
            nwStream.Write(buffer, 0, bytesRead);
        }
    }

    class Program
    {
        

        static void Main(string[] args)
        {

            HostFactory.Run(x =>                                 
            {
                x.Service<SimpleService>(s =>                        
                {
                    s.ConstructUsing(name => new SimpleService());   
                    s.WhenStarted(tc => tc.Start());             
                    s.WhenStopped(tc => tc.Stop());              
                });
                x.RunAsLocalSystem();                            

                x.SetDescription("Sample Topshelf Host Server SimpleService");        
                x.SetDisplayName("Simple Service");                       
                x.SetServiceName("SimpleService");                       
            });                                                     
        }
    }
}
