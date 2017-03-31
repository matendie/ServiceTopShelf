using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace TestServiceApp
{
    class Program
    {
        

        //static void Main(string[] args)
        //{
        //    ////---data to send to the server---
        //    //string textToSend = DateTime.Now.ToString();
        //    ////---create a TCPClient object at the IP and port no.---
        //    //client = new TcpClient(SERVER_IP, PORT_NO);
        //    //nwStream = client.GetStream();
        //    //byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);
        //    ////---send the text---
        //    //Console.WriteLine("Sending : " + textToSend);
        //    //nwStream.Write(bytesToSend, 0, bytesToSend.Length);
        //    ////---read back the text---
        //    //byte[] bytesToRead = new byte[client.ReceiveBufferSize];
        //    //int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
        //    //Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
        //    //Console.ReadLine();
        //    //client.Close();
        //}

        static void Main(string[] args)
        {

            HostFactory.Run(x =>
            {
                x.Service<TestService>(s =>
                {
                    s.ConstructUsing(name => new TestService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Sample Topshelf Host Server TestService");
                x.SetDisplayName("Test Service");
                x.SetServiceName("TestService");
            });
        }
    }
    public class TestService
    {
        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1";
        static TcpClient client;
        static NetworkStream nwStream;



        public void SendMessage(string message)
        {
            //---data to send to the server---
            //string textToSend = DateTime.Now.ToString();
            string textToSend = message;

            //---create a TCPClient object at the IP and port no.---
            client = new TcpClient(SERVER_IP, PORT_NO);
            nwStream = client.GetStream();
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

            //---send the text---
            Console.WriteLine("Sending : " + textToSend);
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
        }

        public void ReceivedMessage()
        {
            //---read back the text---
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
            //Console.ReadLine();
            client.Close();

            //return "some message";
        }

        public void Start()
        {
            while(true)
            {
                string input = Console.ReadLine();
                SendMessage(input);
                ReceivedMessage();
            }
        }

        public void Stop()
        {
            Environment.Exit(0);
        }
    }
}
