using AntroStop.Domain.Base.AuthModels;
using AntroStop.Interfaces.WebRepositories;
using Microsoft.AspNetCore.Components;

namespace AntroStop.MauiUI.Pages
{
    public partial class Index
    {
        //Блок авторизации пользователя
        private UserForAuthenticationDto _userForAuthentication = new UserForAuthenticationDto();

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public bool ShowAuthError { get; set; }
        public string Error { get; set; }

        public async Task ExecuteLogin()
        {
            ShowAuthError = false;

            var result = await AuthenticationService.Login(_userForAuthentication);
            if (!result.IsAuthSuccessful)
            {
                Error = result.ErrorMessage;
                ShowAuthError = true;

                StateHasChanged();
            }
            else
            {
                NavigationManager.NavigateTo("/", true);
            }
        }
    }
}
