using AntroStop.Domain.Base.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace AntroStop.BlazorUI.Components.ViolationsTable
{
    public partial class ViolationsTable
    {
        [Parameter]
        public List<ViolationsInfo> Violations { get; set; }
    }
}
