using AntroStop.Domain.Base.Models;
using AntroStop.Interfaces.WebRepositories;
using Blazored.Toast.Services;
using Blazored.Toast;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace AntroStop.BlazorUI.Components.ViolationsTable
{
    public partial class ViolationsTable
    {
        [Parameter]
        public List<ViolationsInfo> Violations { get; set; }

        [Inject]
        public IWebViolationsRepository<ViolationsInfo> ViolationsRepository { get; set; }

        [Inject]
        public IToastService toast { get; set; }
        private ToastParameters _toastParameters = default!;


    }
}
