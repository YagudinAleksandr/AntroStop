using AntroStop.Interfaces.WebRepositories;
using Microsoft.AspNetCore.Components;

namespace AntroStop.MauiUI.Pages
{
    public partial class LogoutPage
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await AuthenticationService.Logout();
            NavigationManager.NavigateTo("/");
        }
    }
}
