using AntroStop.Interfaces.WebRepositories;
using AntroStop.MAUIUI.Services;
using AntroStop.MAUIUI.ViewModels.Dashboard;
using AntroStop.MAUIUI.ViewModels.Startup;
using AntroStop.MAUIUI.Views.Dashboard;

namespace AntroStop.MAUIUI
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Добавление сервисов
            builder.Services.AddSingleton<IAuthenticationService, LoginService>();

            //Добавление страниц
            builder.Services.AddSingleton<SignIn>();
            builder.Services.AddSingleton<DashboardPage>();

            //Добавление моделей
            builder.Services.AddSingleton<SignInViewModel>();
            builder.Services.AddSingleton<DashboardViewModel>();

            return builder.Build();
        }
    }
}