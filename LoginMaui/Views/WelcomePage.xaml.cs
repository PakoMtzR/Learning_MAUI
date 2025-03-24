using LoginMaui.ViewModels;
using Microsoft.Maui.Controls;

namespace LoginMaui.Views;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();

		BindingContext = new WelcomeViewModel();
	}
}