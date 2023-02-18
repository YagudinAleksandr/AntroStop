using AntroStop.MAUIUI.ViewModels;
using AntroStop.MAUIUI.Views.Dashboard;
using AntroStop.MAUIUI.Views.Startup;

namespace AntroStop.MAUIUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            this.BindingContext = new AppShellViewModel();

            Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        }
    }
}