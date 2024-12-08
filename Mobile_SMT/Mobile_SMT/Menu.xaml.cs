namespace Mobile_SMT;

public partial class Menu : ContentPage
{
	public Menu()
    {
        InitializeComponent();
    }


    // Evento para navegar para a página Home (menu inferior)

    private async void home(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Home());
    }

    // Evento para navegar para a página Perfil (menu inferior)
    private async void OnProfileClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Perfil());
    }

    // Evento para navegar para a página Menu (menu inferior)
    private async void OnmenuClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Menu());
    }
}

