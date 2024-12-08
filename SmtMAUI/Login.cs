using System;
using System.Net.Http;
using Microsoft.Maui.Controls;

namespace SmtMAUI;

public partial class Login : ContentPage
{
	private readonly HttpClient _httpClient = new HttpClient();
	public Login()
	{
		InitializeComponent();
	}

	 private async void OnLoginClicked(object sender, EventArgs e)
    {

		 // Lógica de autenticação
        await DisplayAlert("Login", "Bem-vindo ao sistema!", "OK");
		await Navigation.PushAsync(new PainelADM(_httpClient));

    }
}

