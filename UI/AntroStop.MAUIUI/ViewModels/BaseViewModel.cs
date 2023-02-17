using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace AntroStop.MAUIUI.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _title;
    }
}
