using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AntroStop.Interfaces.Base.Repositories;
using AntroStop.Domain.Base.Models;
using AntroStop.WebAPIClients.Repositories;
using AntroStop.BlazorUI.Infrastructure.Extensions;
using AntroStop.Domain.Base.Models.Users;
using AntroStop.Interfaces.WebRepositories;
using Blazored.Toast;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using AntroStop.BlazorUI.Providers;
using AntroStop.BlazorUI.LocalServices;
using Darnton.Blazor.DeviceInterop.Geolocation;
using Tewr.Blazor.FileReader;

namespace AntroStop.BlazorUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var services = builder.Services;

            //Ссылка на основной хост
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            services.AddScoped<IGeolocationService, GeolocationService>();

            //Репозитории
            services.AddApi<IWebUsersRepository<UsersInfo>, WebUsersRepository<UsersInfo>>("api/UsersRepository/");
            services.AddApi<IIntRepository<RolesInfo>, WebRolesRepository<RolesInfo>>("api/RolesRepository/");
            services.AddApi<IWebViolationsRepository<ViolationsInfo>, WebViolationsRepository<ViolationsInfo>>("api/ViolationsRepository/");

            services.AddFileReaderService(o => o.UseWasmSharedBuffer = true);

            //Локальное ситемное хранилище Blazor
            services.AddBlazoredLocalStorage();

            //Ядро авторизации
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            //Подключение сервиса аутентификации
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            //Подключение сервиса уведомлений
            services.AddBlazoredToast();

            await builder.Build().RunAsync();
        }
    }
}
