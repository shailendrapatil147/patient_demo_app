using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Demo.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                           .UseKestrel()
                           .UseContentRoot(Directory.GetCurrentDirectory())
                           .ConfigureAppConfiguration((hostingContext, config) =>
                           {
                               var env = hostingContext.HostingEnvironment;
                               config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                     .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);
                               config.AddEnvironmentVariables();
                               if (args != null)
                               {
                                   config.AddCommandLine(args);
                               }
                           })
                           .UseStartup<Startup>()
                           .CaptureStartupErrors(true)
                           .UseIISIntegration();
        }
    }
}
