using Microsoft.Maui.Controls;

namespace SmtMAUI.Controls
{
    public partial class Header : ContentView
    {
        public Header()
        {
            InitializeComponent();
        }

        private async void OnNavigateToListaConsultasClicked(object sender, EventArgs e)
        {
            
                // Navegar para a página de consultas
                await Navigation.PushAsync(new ListaConsulta());

            
        }

        private async void OnNavigateToCadastroContribuintesClicked(object sender, EventArgs e)
        {
            
                // Navegar para a página de contribuintes
                await Navigation.PushAsync(new CadastroContribuintes());

            
        }

        private async void OnNavigateToPainelMedicoClicked(object sender, EventArgs e)
        {
            
                // Navegar para a página de contribuintes
                await Navigation.PushAsync(new PainelMedico());

            
        }
        private async void OnNavigateToPainelADMClicked(object sender, EventArgs e)
        {            
            
                // Navegar para a página PainelADM
                await Navigation.PushAsync(new PainelADM());

            
            
        }
    }
}
