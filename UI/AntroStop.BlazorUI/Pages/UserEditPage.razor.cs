using AntroStop.BlazorUI.Components;
using AntroStop.Domain.Base.Models;
using AntroStop.Domain.Base.Models.Users;
using AntroStop.Interfaces.Base.Repositories;
using AntroStop.Interfaces.WebRepositories;
using Blazored.Toast;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntroStop.BlazorUI.Pages
{
    public partial class UserEditPage
    {
        [Parameter]
        public string UserId { get; set; }

        private string ButtonStatus { get; set; }

        private UsersInfo user;


        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public IIntRepository<RolesInfo> rolesRepository { get; set; }
        [Inject]
        public IWebUsersRepository<UsersInfo> usersRepository { get; set; }
        [Inject]
        public IToastService toast { get; set; }

        private IList<RolesInfo> roles;
        private ToastParameters _toastParameters = default!;

        protected override void OnInitialized()
        {
            UserId = UserId ?? string.Empty;

            if (UserId != string.Empty)
            {
                ButtonStatus = "Сохранить";
            }
            else
            {
                ButtonStatus = "Добавить";
                user = new UsersInfo();
            }

            
        }

        protected override async Task OnInitializedAsync()
        {
            roles = (await rolesRepository.GetAll()).ToList();
        }

        private async Task Save()
        {
            await usersRepository.Add(user);

            _toastParameters = new ToastParameters();
            _toastParameters.Add(nameof(MyToastComponent.Title), "Успех!");
            _toastParameters.Add(nameof(MyToastComponent.ToastParam), "Пользователь создан");

            toast.ShowToast<MyToastComponent>(_toastParameters);

            Navigation.NavigateTo("/Users");
        }
    }
}
