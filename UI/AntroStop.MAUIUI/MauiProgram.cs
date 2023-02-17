using AntroStop.MAUIUI.ViewModels.Dashboard;
using AntroStop.MAUIUI.ViewModels.Startup;
using AntroStop.MAUIUI.Views.Dashboard;
using AntroStop.MAUIUI.Views.Startup;

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

            //Views
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<DashboardPage>();
            

            //Models
            builder.Services.AddSingleton<LoginPageViewModel>();
            builder.Services.AddSingleton<DashboardPageViewModel>();

            return builder.Build();
        }
    }
}