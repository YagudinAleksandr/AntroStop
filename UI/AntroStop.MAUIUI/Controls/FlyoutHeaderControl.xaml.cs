namespace AntroStop.MAUIUI.Controls;

public partial class FlyoutHeaderControl : StackLayout
{
	public FlyoutHeaderControl()
	{
		InitializeComponent();

		if(App.UserDetail!=null)
		{
			lblUserEmail.Text = App.UserDetail.ID;
			lblUserName.Text = App.UserDetail.Name;
		}
	}
}