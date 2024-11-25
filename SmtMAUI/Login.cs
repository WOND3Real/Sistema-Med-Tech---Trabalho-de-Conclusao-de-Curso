using System;
using Microsoft.Maui.Controls;

namespace SmtMAUI;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

	 private async void OnLoginClicked(object sender, EventArgs e)
        {

			 // Lógica de autenticação
            await DisplayAlert("Login", "Bem-vindo ao sistema!", "OK");
			await Navigation.PushAsync(new PainelADM());

        }
    
		
      




}

