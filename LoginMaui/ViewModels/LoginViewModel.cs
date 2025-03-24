using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using LoginMaui.Models;


namespace LoginMaui.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand NavigateToSignUpCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLogin);
            NavigateToSignUpCommand = new Command(OnNavigateToSignUp);

        }

        private void OnLogin()
        {
            // Aquí puedes agregar la lógica para validar el usuario
            // Por ejemplo, verificar en la base de datos SQLite
            if (Username == "admin" && Password == "1234")
            {
                // Navegar a la pantalla de bienvenida
                Shell.Current.GoToAsync("//WelcomePage");
            }
            else
            {
                // Mostrar un mensaje de error
                App.Current.MainPage.DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
            }
        }

        private void OnNavigateToSignUp()
        {
            Shell.Current.GoToAsync("//SignUpPage");
        }
    }
}
