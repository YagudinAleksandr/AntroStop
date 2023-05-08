using AntroStop.Domain.Base.Models.Users;
using AntroStop.Domain.Base.Models;
using AntroStop.Interfaces.Base.Repositories;
using AntroStop.Interfaces.WebRepositories;
using AntroStop.MauiUI.Data;
using AntroStop.MauiUI.Infrastructure.Extensions;
using AntroStop.WebAPIClients.Repositories;
using Blazored.LocalStorage;
using Darnton.Blazor.DeviceInterop.Geolocation;
using Microsoft.AspNetCore.Components.Authorization;
using Tewr.Blazor.FileReader;
using AntroStop.MauiUI.Providers;
using AntroStop.MauiUI.LocalServices;

namespace AntroStop.MauiUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            var services = builder.Services;

            //Ссылка на основной хост
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5000") });
            services.AddScoped<IGeolocationService, GeolocationService>();

            services.AddMauiBlazorWebView();
#if DEBUG
		services.AddBlazorWebViewDeveloperTools();
#endif


            //Репозитории
            services.AddApi<IWebUsersRepository<UsersInfo>, WebUsersRepository<UsersInfo>>("api/UsersRepository/");
            services.AddApi<IIntRepository<RolesInfo>, WebRolesRepository<RolesInfo>>("api/RolesRepository/");
            services.AddApi<IWebViolationsRepository<ViolationsInfo>, WebViolationsRepository<ViolationsInfo>>("api/ViolationsRepository/");
            services.AddApi<IWebElementsRepository<ElementsInfo>, WebElementsRepository<ElementsInfo>>("api/ElementsRepository/");

            services.AddFileReaderService(o => o.UseWasmSharedBuffer = true);

            //Локальное ситемное хранилище Blazor
            services.AddBlazoredLocalStorage();

            //Ядро авторизации
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            //Подключение сервиса аутентификации
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddSingleton<WeatherForecastService>();

            return builder.Build();
        }
    }
}