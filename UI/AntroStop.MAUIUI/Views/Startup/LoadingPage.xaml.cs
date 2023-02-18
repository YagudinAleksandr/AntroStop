using AntroStop.MAUIUI.ViewModels.Startup;

namespace AntroStop.MAUIUI.Views.Startup;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoadingPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}