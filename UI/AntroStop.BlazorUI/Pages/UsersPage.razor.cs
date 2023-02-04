using AntroStop.Domain.Base.Models.Users;
using AntroStop.Domain.Pagination.RequestFeatures;
using AntroStop.Interfaces.WebRepositories;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AntroStop.BlazorUI.Pages
{
    public partial class UsersPage
    {
        public List<UsersInfo> UsersList { get; set; } = new List<UsersInfo>();

        public MetaData MetaData { get; set; } = new MetaData();

        private PageParametrs _productParameters = new PageParametrs();

        [Inject]
        public IWebUsersRepository<UsersInfo> UsersRepo { get; set; }


        protected async override Task OnInitializedAsync()
        {
            await GetProducts();
        }

        private async Task SelectedPage(int page)
        {
            _productParameters.PageNumber = page;
            await GetProducts();
        }
        private async Task GetProducts()
        {
            var pagingResponse = await UsersRepo.GetPage(_productParameters);
            UsersList = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }
    }
}
