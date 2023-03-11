using AntroStop.Domain.Base.Models;
using AntroStop.Domain.Pagination.RequestFeatures;
using AntroStop.Interfaces.WebRepositories;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AntroStop.BlazorUI.Pages
{
    public partial class ViolationsPage
    {
        public List<ViolationsInfo> ViolationsList { get; set; } = new List<ViolationsInfo>();

        public MetaData MetaData { get; set; } = new MetaData();

        private PageParametrs _productParameters = new PageParametrs();

        [Inject]
        public IWebViolationsRepository<ViolationsInfo> ViolationsRepo { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await GetItems();
        }

        private async Task SelectedPage(int page)
        {
            _productParameters.PageNumber = page;
            await GetItems();
        }

        private async Task GetItems()
        {
            var pagingResponse = await ViolationsRepo.GetPage(_productParameters);
            ViolationsList = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }
    }
}
