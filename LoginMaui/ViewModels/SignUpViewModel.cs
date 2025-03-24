 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LoginMaui.Models;

namespace LoginMaui.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        private string _username; 
        private string _email;
        private string _password;
        private string _confirmPassword;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        public ICommand SignUpCommand { get; }
        public ICommand NavigateToLoginCommand { get; }

        public SignUpViewModel()
        {
            SignUpCommand = new Command(OnSignUp);
            NavigateToLoginCommand = new Command(OnNavigateToLogin);
        }

        private void OnSignUp()
        {
            if (Password != ConfirmPassword)
            {
                App.Current.MainPage.DisplayAlert("Error", "Las contraseñas no coinciden", "OK");
                return;
            }

            // Aquí puedes guardar el usuario en la base de datos SQLite
            var nuevoUsuario = new User
            {
                Username = Username,
                Email = Email,
                Password = Password
            };

            // Guardar en SQLite (implementar la lógica de guardado)
            // ...

            // Navegar de vuelta al login
            Shell.Current.GoToAsync("//LoginPage");
        }

        private void OnNavigateToLogin()
        {
            // Navegar de vuelta al login
            Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
