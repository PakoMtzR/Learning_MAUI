
namespace MauiApp3_Calculator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Views.CalculatorView();
        }
    }
}
