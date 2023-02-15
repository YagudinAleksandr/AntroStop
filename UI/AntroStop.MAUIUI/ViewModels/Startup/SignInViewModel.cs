using AntroStop.MAUIUI.Views.Dashboard;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntroStop.MAUIUI.ViewModels.Startup
{
    public partial class SignInViewModel : BaseViewModel
    {
        #region Commands
        [ICommand]
        async void Login()
        {
            await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
        }
        #endregion
    }
}
