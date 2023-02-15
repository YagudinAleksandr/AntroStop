using AntroStop.MAUIUI.ViewModels.Dashboard;

namespace AntroStop.MAUIUI.Views.Dashboard;

public partial class DashboardPage : ContentPage
{
	public DashboardPage(DashboardViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}