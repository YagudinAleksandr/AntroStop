using AntroStop.Domain.Base.AuthModels;
using AntroStop.Interfaces.WebRepositories;
using Microsoft.AspNetCore.Components;

namespace AntroStop.MauiUI.Pages
{
    public partial class RegistrationPage
    {
        
        public UserForRegistrationDto userForRegistration = new UserForRegistrationDto();

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public bool ShowRegistrationError { get; set; } = false;
        public List<string> Error { get; set; }

        

        public async Task Registration()
        {
            ShowRegistrationError = false;

            var result = await AuthenticationService.RegisterUser(userForRegistration);

            if (!result.IsSuccessfulRegistration)
            {
                Error = (List<string>)result.Errors;
                ShowRegistrationError = true;

                StateHasChanged();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Регистрация успешна!", "Регистрация", "OK");
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
