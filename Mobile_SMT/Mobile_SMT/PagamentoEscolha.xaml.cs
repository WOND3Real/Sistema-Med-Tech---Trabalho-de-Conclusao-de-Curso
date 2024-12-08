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
            // Exibe uma mensagem de confirmação
            bool isConfirmed = await DisplayAlert(
                "Confirmação",
                "Deseja confirmar o pagamento via Pix?",
                "Sim",
                "Não"
            );

            // Caso o usuário confirme, navega para a próxima página
            if (isConfirmed)
            {
                await Navigation.PushAsync(new Pix()); // Substitua 'ProximaPagina' pela classe da próxima página.
            }
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
