using AntroStop.MAUIUI.ViewModels.Startup;

namespace AntroStop.MAUIUI.Views.Startup;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}