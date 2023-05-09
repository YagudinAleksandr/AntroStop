using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Net;
using System.Threading.Tasks;

namespace AntroStop.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog((host,log)=>log.ReadFrom.Configuration(host.Configuration))
            .ConfigureWebHostDefaults(host =>
                {
                    host.UseStartup<Startup>();
                    host.UseUrls("http://10.3.3.18:5002", "https://10.3.3.18:5004");
                });    
    }
}
