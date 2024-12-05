using System;
using Microsoft.Maui.Controls;
using System.Net.Http;

namespace SmtMAUI
{
    public partial class Login : ContentPage
    {
        private readonly HttpClient _httpClient;

        public Login()
        {
            InitializeComponent();
            _httpClient = new HttpClient();  // Criando o HttpClient
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Lógica de autenticação
            await DisplayAlert("Login", "Bem-vindo ao sistema!", "OK");

            // Navegar para o PainelADM passando o HttpClient
            await Navigation.PushAsync(new PainelADM(_httpClient));
        }
    }
}
 