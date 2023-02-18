using AntroStop.MAUIUI.Views.Startup;
using Microsoft.Toolkit.Mvvm.Input;

namespace AntroStop.MAUIUI.ViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {
        [ICommand]
        async void SignOut()
        {
            if (Preferences.ContainsKey(nameof(App.UserDetail)))
                Preferences.Remove(nameof(App.UserDetail));

            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
