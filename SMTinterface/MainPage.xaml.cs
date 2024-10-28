namespace SMTinterface;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnNavigateButtonClicked(object sender, EventArgs e)
{
    // Navega para a SecondPage.
    await Navigation.PushAsync(new PagPrincipalADM());
}

}

