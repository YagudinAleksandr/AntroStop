using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using AntroStop.Domain.Base.Models.Users;
using AntroStop.Interfaces.WebRepositories;
using Blazored.Toast.Services;
using Blazored.Toast;
using System.Linq;

namespace AntroStop.BlazorUI.Components.UsersTable
{
    public partial class UsersTable
    {
        [Parameter]
        public List<UsersInfo> Users { get; set; }

        [Inject]
        public IWebUsersRepository<UsersInfo> UsersRepo { get; set; }
        [Inject]
        public IToastService toast { get; set; }
        private ToastParameters _toastParameters = default!;


        private async Task Delete(string id)
        {
            if (id == string.Empty) return;

            await UsersRepo.Delete(id);

            Users.Remove(Users.Where(x => x.ID == id).FirstOrDefault());

            _toastParameters = new ToastParameters();
            _toastParameters.Add(nameof(MyToastComponent.Title), "Успех!");
            _toastParameters.Add(nameof(MyToastComponent.ToastParam), "Пользователь удален успешно!");

            toast.ShowToast<MyToastComponent>(_toastParameters);

        }
    }
}
