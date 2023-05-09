using AntroStop.Domain.Base.Models;
using Microsoft.AspNetCore.Components;

namespace AntroStop.MauiUI.Components
{
    public partial class ViolationsTableComponent
    {
        [Parameter]
        public List<ViolationsInfo> Violations { get; set; }
    }
}
