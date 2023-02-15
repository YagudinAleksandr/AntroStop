using Microsoft.Toolkit.Mvvm.Input;

namespace AntroStop.MAUIUI.ViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {
        [ICommand]
        async void SignOut()
        {
            await Shell.Current.GoToAsync($"//{nameof(SignIn)}");
        }
    }
}
