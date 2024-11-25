using Microsoft.Maui.Controls;

namespace SmtMAUI;

public partial class PainelADM : ContentPage
{

	public PainelADM()
	{
		InitializeComponent();
	}

	 // Evento de mudança de estado do RadioButton
        private async void OnLoginClicked(object sender, CheckedChangedEventArgs e)
        {
            // Verifica se o RadioButton foi marcado
            if (e.Value)
            {
                // Navegar para a página CadastroContribuintes
                await Navigation.PushAsync(new CadastroContribuintes());
            }
        }

}

