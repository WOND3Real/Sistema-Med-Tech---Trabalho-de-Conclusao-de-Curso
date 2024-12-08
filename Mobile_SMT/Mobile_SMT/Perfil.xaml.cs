namespace Mobile_SMT;

public partial class Perfil : ContentPage
{
	public Perfil()
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
    private async void OnHistoricoButtonClicked(object sender, EventArgs e)
    {
        // Exibe uma mensagem informando que n�o h� hist�rico
        await DisplayAlert("Aviso", "Ainda n�o h� nenhum hist�rico dispon�vel.", "OK");
    }
}


