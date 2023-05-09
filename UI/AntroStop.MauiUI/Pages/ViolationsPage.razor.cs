using AntroStop.Domain.Base.Models;
using AntroStop.Interfaces.WebRepositories;
using Microsoft.AspNetCore.Components;

namespace AntroStop.MauiUI.Pages
{
    public partial class ViolationsPage
    {
        [Parameter]
        public string userID { get; set; }

        public List<ViolationsInfo> ViolationsList { get; set; } = new List<ViolationsInfo>();

        [Inject]
        public IWebViolationsRepository<ViolationsInfo> ViolationsRepo { get; set; }

        protected async override Task OnInitializedAsync()
        {
            ViolationsList = (List<ViolationsInfo>)await ViolationsRepo.GetAllByID(userID);
        }
    }
}
