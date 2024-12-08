namespace Mobile_SMT;

public partial class Menu : ContentPage
{
	public Menu()
    {
        InitializeComponent();
    }


    // Evento para navegar para a p�gina Home (menu inferior)

    private async void home(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Home());
    }

    // Evento para navegar para a p�gina Perfil (menu inferior)
    private async void OnProfileClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Perfil());
    }

    // Evento para navegar para a p�gina Menu (menu inferior)
    private async void OnmenuClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Menu());
    }
}

