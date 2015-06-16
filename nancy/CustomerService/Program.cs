using System;
using Nancy.Hosting.Self;

namespace CustomerService
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
            using (var host = new NancyHost(conf, new Uri("http://localhost:8080")))
            {
                Console.WriteLine("Started");
                host.Start();
                Console.ReadLine();
            }
        }
    
    }
}
