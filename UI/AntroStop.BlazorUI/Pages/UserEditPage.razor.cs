using AntroStop.Domain.Base.Models;
using AntroStop.Domain.Base.Models.Users;
using AntroStop.Interfaces.Base.Repositories;
using AntroStop.Interfaces.WebRepositories;
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
        public IIntRepository<RolesInfo> rolesRepository { get; set; }
        [Inject]
        public IWebUsersRepository<UsersInfo> usersRepository { get; set; }

        private IList<RolesInfo> roles;

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
        }
    }
}
