using AntroStop.Domain.Base.AuthModels;
using AntroStop.MAUIUI.Controls;
using AntroStop.MAUIUI.Views.Dashboard;
using AntroStop.MAUIUI.Views.Startup;
using Newtonsoft.Json;

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
            {
                var userInfo = JsonConvert.DeserializeObject<AuthResponseDto>(userDetailsStr);

                App.UserDetail = userInfo;

                AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();

                await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
            }
                
        }
    }
}
