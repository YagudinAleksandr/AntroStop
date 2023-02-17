using AntroStop.MAUIUI.ViewModels;
using AntroStop.MAUIUI.Views.Dashboard;

namespace AntroStop.MAUIUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            this.BindingContext = new AppShellViewModel();

            Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
        }
    }
}