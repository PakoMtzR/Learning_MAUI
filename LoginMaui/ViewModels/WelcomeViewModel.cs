using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LoginMaui.ViewModels
{
    public class WelcomeViewModel : BaseViewModel
    {
        public ICommand LogOutCommand { get; }

        public WelcomeViewModel()
        {
            LogOutCommand = new Command(OnLogOut);
        }

        private void OnLogOut()
        {
            Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
