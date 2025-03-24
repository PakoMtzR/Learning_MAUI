using LoginMaui.ViewModels;
using Microsoft.Maui.Controls;

namespace LoginMaui.Views;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();

		BindingContext = new SignUpViewModel();
	}
}