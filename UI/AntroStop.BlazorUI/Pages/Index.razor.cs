using System.Threading.Tasks;
using AntroStop.Domain.Base.Models.Users;
using AntroStop.Interfaces.WebRepositories;
using Microsoft.AspNetCore.Components;

namespace AntroStop.BlazorUI.Pages
{
    public partial class Index
    {
        private int usersCount;
        [Inject]
        private IWebUsersRepository<UsersInfo> Users { get; set; }


        protected override async Task OnInitializedAsync()
        {
            usersCount = await Users.Count();


           
        }
    }
}
