using AntroStop.BlazorUI.Components;
using AntroStop.Domain.Base.AuthModels;
using AntroStop.Interfaces.WebRepositories;
using Blazored.Toast;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AntroStop.BlazorUI.Pages
{
    public partial class LoginPage
    {
        private UserForAuthenticationDto _userForAuthentication = new UserForAuthenticationDto();

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IToastService toast { get; set; }
        private ToastParameters _toastParameters = default!;

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

                _toastParameters = new ToastParameters();
                _toastParameters.Add(nameof(MyToastComponent.Title), "Ошибка");
                _toastParameters.Add(nameof(MyToastComponent.ToastParam), Error);
                _toastParameters.Add(nameof(MyToastComponent.Type), "danger");

                toast.ShowToast<MyToastComponent>(_toastParameters);
            }
            else
            {
                _toastParameters = new ToastParameters();
                _toastParameters.Add(nameof(MyToastComponent.Title), "Успех");
                _toastParameters.Add(nameof(MyToastComponent.ToastParam), "Вы успешно авторизировались");
                _toastParameters.Add(nameof(MyToastComponent.Type), "success");

                toast.ShowToast<MyToastComponent>(_toastParameters);

                NavigationManager.NavigateTo("/", true);
            }
        }
    }
}
