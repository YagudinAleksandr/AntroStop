using AntroStop.MAUIUI.Views.Dashboard;
using AntroStop.MAUIUI.Views.Startup;

namespace AntroStop.MAUIUI.ViewModels.Startup
{
    public class LoadingPageViewModel
    {
        public LoadingPageViewModel()
        {
            CheckUser();
        }

        private async void CheckUser()
        {
            string userDetailsStr = Preferences.Get(nameof(App.UserDetail), "");

            if (string.IsNullOrWhiteSpace(userDetailsStr))
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            else
                await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
        }
    }
}
