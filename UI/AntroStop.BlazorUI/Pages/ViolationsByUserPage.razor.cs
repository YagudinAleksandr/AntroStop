using AntroStop.Domain.Base.Models;
using AntroStop.Interfaces.WebRepositories;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AntroStop.BlazorUI.Pages
{
    public partial class ViolationsByUserPage
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
