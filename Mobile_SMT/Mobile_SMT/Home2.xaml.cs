using System;
using Microsoft.Maui.Controls;

namespace Mobile_SMT
{
    public partial class Home2 : ContentPage
    {
        public Home2()
        {
            InitializeComponent();
        }

        // Evento para o botão Hospital
        private async void OnHospitalButtonClicked(object sender, EventArgs e)
        {
            // Navega para a página Hospital
            await Navigation.PushAsync(new Hospital());
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

        private async void resultado(object sender, EventArgs e)
        {
            // Exibe uma mensagem informando que não há histórico
            await DisplayAlert("Aviso", "Ainda não há nenhum resultado de exame disponive.l", "OK");
        }

        private async void receita(object sender, EventArgs e)
        {
            // Exibe uma mensagem informando que não há histórico
            await DisplayAlert("Aviso", "Ainda não há nenhuma receita disponível.", "OK");
        }
        private async void OnHistoricoButtonClicked(object sender, EventArgs e)
        {
            // Exibe uma mensagem informando que não há histórico
            await DisplayAlert("Atendente indisponível", "Retorne mais tarde.", "OK");
        }

    }
}
