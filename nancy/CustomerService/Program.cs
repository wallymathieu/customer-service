using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Customers
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();
    
    }
}
