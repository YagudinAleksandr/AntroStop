using AntroStop.MAUIUI.ViewModels.Startup;

namespace AntroStop.MAUIUI;

public partial class SignIn : ContentPage
{
	public SignIn(SignInViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}

    private async void TapGestureRecognizer_Tapped_For_SignUp(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("//SignUp");
    }
}