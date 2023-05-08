using AntroStop.Domain.Base.Models;
using AntroStop.Interfaces.WebRepositories;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AntroStop.BlazorUI.Components
{
    public partial class SideBarComponent
    {
        private int inboxViolations;
        [Inject]
        private IWebViolationsRepository<ViolationsInfo> ViolationsRepository { get; set; }

        protected override async Task OnInitializedAsync()
        {
            inboxViolations = await ViolationsRepository.GetCountByStatus("Принята");
        }
    }
}
