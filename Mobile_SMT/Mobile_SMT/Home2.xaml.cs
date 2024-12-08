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

        // Evento para o bot�o Hospital
        private async void OnHospitalButtonClicked(object sender, EventArgs e)
        {
            // Navega para a p�gina Hospital
            await Navigation.PushAsync(new Hospital());
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

        private async void resultado(object sender, EventArgs e)
        {
            // Exibe uma mensagem informando que n�o h� hist�rico
            await DisplayAlert("Aviso", "Ainda n�o h� nenhum resultado de exame disponive.l", "OK");
        }

        private async void receita(object sender, EventArgs e)
        {
            // Exibe uma mensagem informando que n�o h� hist�rico
            await DisplayAlert("Aviso", "Ainda n�o h� nenhuma receita dispon�vel.", "OK");
        }
        private async void OnHistoricoButtonClicked(object sender, EventArgs e)
        {
            // Exibe uma mensagem informando que n�o h� hist�rico
            await DisplayAlert("Atendente indispon�vel", "Retorne mais tarde.", "OK");
        }

    }
}
