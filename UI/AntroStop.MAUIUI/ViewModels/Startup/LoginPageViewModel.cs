using AntroStop.Domain.Base.AuthModels;
using AntroStop.MAUIUI.Controls;
using AntroStop.MAUIUI.Views.Dashboard;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace AntroStop.MAUIUI.ViewModels.Startup
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        #region Commands
        [ICommand]
        async void Login()
        {
            if(!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
            {
                //TODO
                //Здесь происходит вызов API

                //Времянка
                var userResponseDTO = new AuthResponseDto
                {
                    ID = Email,
                    Name = "Тестовый пользователь",
                    IsAuthSuccessful = true,
                    Token = "String"
                };

                if (Preferences.ContainsKey(nameof(App.UserDetail)))
                    Preferences.Remove(nameof(App.UserDetail));

                Preferences.Set(nameof(App.UserDetail), JsonConvert.SerializeObject(userResponseDTO));

                App.UserDetail = userResponseDTO;

                AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();

                await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
            }
        }

        #endregion
    }
}
