using System;
using Microsoft.Maui.Controls;

namespace Mobile_SMT
{
    public partial class PagamentoEscolha : ContentPage
    {
        public PagamentoEscolha()
        {
            InitializeComponent();
        }

        private async void OnPixButtonClicked(object sender, EventArgs e)
        {
            // Exibe uma mensagem de confirma��o
            bool isConfirmed = await DisplayAlert(
                "Confirma��o",
                "Deseja confirmar o pagamento via Pix?",
                "Sim",
                "N�o"
            );

            // Caso o usu�rio confirme, navega para a pr�xima p�gina
            if (isConfirmed)
            {
                await Navigation.PushAsync(new Pix()); // Substitua 'ProximaPagina' pela classe da pr�xima p�gina.
            }
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
