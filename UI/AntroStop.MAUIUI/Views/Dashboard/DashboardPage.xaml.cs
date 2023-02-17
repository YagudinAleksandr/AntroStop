using AntroStop.MAUIUI.ViewModels.Dashboard;

namespace AntroStop.MAUIUI.Views.Dashboard;

public partial class DashboardPage : ContentPage
{
	public DashboardPage(DashboardPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}