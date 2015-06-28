using System;
using Nancy.Hosting.Self;
using Nancy;

namespace Customers
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var conf = new HostConfiguration()
                {
                    AllowChunkedEncoding = true,
                    UrlReservations = { CreateAutomatically = true }
                };
            StaticConfiguration.DisableErrorTraces = false;
            using (var host = new NancyHost(conf, new Uri("http://localhost:8080")))
            {
                Console.WriteLine("Started");
                host.Start();
                Console.ReadLine();
            }
        }
    
    }
}
