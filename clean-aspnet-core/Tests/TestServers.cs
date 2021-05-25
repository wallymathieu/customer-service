using Customers;
using System.IO;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace Tests
{
    public static class TestServers
    {
        public static TestServer Create<T>() where T : Startup
        {
            var _config = new ConfigurationBuilder()
                //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            var host = new WebHostBuilder()
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseConfiguration(_config)
                    .UseStartup<T>()
                ;

            return new TestServer(host) { AllowSynchronousIO=true };
        }
    }
}

