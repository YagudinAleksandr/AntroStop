using AntroStop.Domain.Base.AuthModels;
using AntroStop.Interfaces.WebRepositories;
using Blazored.Toast.Services;
using Blazored.Toast;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Collections.Generic;
using AntroStop.BlazorUI.Components;

namespace AntroStop.BlazorUI.Pages
{
    public partial class RegistrationPage
    {
        private UserForRegistrationDto userForRegistration = new UserForRegistrationDto();

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IToastService toast { get; set; }
        private ToastParameters toastParameters = default!;

        public bool ShowRegistrationError { get; set; }
        public List<string> Error { get; set; }

        public async Task Registration()
        {
            ShowRegistrationError = false;

            var result = await AuthenticationService.RegisterUser(userForRegistration);

            if(!result.IsSuccessfulRegistration)
            {
                Error = (List<string>)result.Errors;
                ShowRegistrationError = true;

                toastParameters = new ToastParameters();
                toastParameters.Add(nameof(MyToastComponent.Title), "Ошибка");
                toastParameters.Add(nameof(MyToastComponent.ToastParam), Error);
                toastParameters.Add(nameof(MyToastComponent.Type), "danger");

                toast.ShowToast<MyToastComponent>(toastParameters);
            }
            else
            {
                toastParameters = new ToastParameters();
                toastParameters.Add(nameof(MyToastComponent.Title), "Успех");
                toastParameters.Add(nameof(MyToastComponent.ToastParam), "Вы успешно зарегистрировались");
                toastParameters.Add(nameof(MyToastComponent.Type), "success");

                toast.ShowToast<MyToastComponent>(toastParameters);

                NavigationManager.NavigateTo("/Login");
            }
        }
    }
}
