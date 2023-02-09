using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AntroStop.Interfaces.Base.Repositories;
using AntroStop.Domain.Base.Models;
using AntroStop.WebAPIClients.Repositories;
using AntroStop.BlazorUI.Infrastructure.Extensions;
using AntroStop.Interfaces.Repositories;
using AntroStop.Domain.Base.Models.Users;
using AntroStop.Interfaces.WebRepositories;
using Blazored.Toast;

namespace AntroStop.BlazorUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var services = builder.Services;

            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            //services.AddHttpClient<IGuidRepository<ViolationsInfo>, WebViolationsRepository<ViolationsInfo>>(
            //(host, client) => client.BaseAddress = new(host.GetRequiredService<IWebAssemblyHostEnvironment>().BaseAddress+"api/ViolationsRepository"));

            
            services.AddApi<IWebUsersRepository<UsersInfo>, WebUsersRepository<UsersInfo>>("api/UsersRepository/");
            services.AddApi<IIntRepository<RolesInfo>, WebRolesRepository<RolesInfo>>("api/RolesRepository/");

            services.AddBlazoredToast();

            await builder.Build().RunAsync();
        }
    }
}
