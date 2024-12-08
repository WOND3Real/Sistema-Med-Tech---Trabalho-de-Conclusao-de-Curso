namespace Mobile_SMT;

public partial class Fim : ContentPage
{
    public Fim()
    {
        InitializeComponent();
    }

    private async void proximaPag(object sender, EventArgs e)
    {
        // Navega para a p gina Hospital
        await Navigation.PushAsync(new Home2());
    }
}