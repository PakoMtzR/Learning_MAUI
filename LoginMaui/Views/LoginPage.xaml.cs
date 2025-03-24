using LoginMaui.ViewModels;
using Microsoft.Maui.Controls;

namespace LoginMaui.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();

		BindingContext = new LoginViewModel();
	}
}