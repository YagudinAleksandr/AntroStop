using AntroStop.Domain.Base.Models;
using AntroStop.Interfaces.WebRepositories;
using Blazored.Toast.Services;
using Blazored.Toast;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

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

        private async Task Delete(string id)
        {

            await ViolationsRepository.Delete(id.ToString());

            Violations.Remove(Violations.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault());

            _toastParameters = new ToastParameters();
            _toastParameters.Add(nameof(MyToastComponent.Title), "Успех!");
            _toastParameters.Add(nameof(MyToastComponent.ToastParam), $"Заявка удален успешно!");
            _toastParameters.Add(nameof(MyToastComponent.Type), "success");

            toast.ShowToast<MyToastComponent>(_toastParameters);
        }
    }
}
