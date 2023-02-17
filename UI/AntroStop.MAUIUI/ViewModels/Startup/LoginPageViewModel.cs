using AntroStop.MAUIUI.Views.Dashboard;
using Microsoft.Toolkit.Mvvm.Input;

namespace AntroStop.MAUIUI.ViewModels.Startup
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        #region Commands
        [ICommand]
        async void Login()
        {
            await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
        }

        #endregion
    }
}
