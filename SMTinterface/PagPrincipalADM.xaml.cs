using System;
using Microsoft.Maui.Controls;

namespace SMTinterface;



public partial class PagPrincipalADM : ContentPage
{
	public PagPrincipalADM()
	{
		InitializeComponent();
	}

	// Este método será chamado quando o botão "Voltar" for clicado.
        private void OnBackButtonClicked(object sender, EventArgs e)
        {
            // Navega para a página anterior na pilha de navegação.
            Navigation.PopAsync();
	    }
}