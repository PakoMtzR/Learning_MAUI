namespace MauiApp2
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Me tocaste {count} vez";
            else
                CounterBtn.Text = $"Me tocaste {count} veces";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private void ResetBtn_Clicked(object sender, EventArgs e)
        {
            count = 0;
            CounterBtn.Text = $"Tocame";
            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
