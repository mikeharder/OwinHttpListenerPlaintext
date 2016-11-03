using Microsoft.Owin.Hosting;
using System;

namespace OwinHttpListenerPlaintext
{
    class Program
    {
        private const string _url = "http://*:8080";

        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>(_url))
            {
                Console.WriteLine($"Listening on {_url}");
                Console.ReadLine();
            }
        }
    }
}
