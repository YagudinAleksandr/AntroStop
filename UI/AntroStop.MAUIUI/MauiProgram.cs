using AntroStop.Interfaces.WebRepositories;
using AntroStop.MAUIUI.Services;

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

            //Добавление моделей


            return builder.Build();
        }
    }
}