using System.Net.Http;
using System.Text.Json;
using Microsoft.Maui.Controls;

namespace SmtMAUI
{
    public partial class Perfil : ContentPage
    {
        private readonly HttpClient _httpClient;

        public Perfil()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

		// Chame este método no evento OnAppearing ou no seu fluxo desejado
    	protected override async void OnAppearing()
        {
            base.OnAppearing();
            await BuscarAdministrador(GerarIdConsultaAleatorio()); // Passar o ID de exemplo ou um ID real
        }

		private int GerarIdConsultaAleatorio(int min = 1, int max = 1)
		{
			Random random = new Random();
			return random.Next(min, max);
		}

        // Método para buscar o administrador
        private async Task BuscarAdministrador(int id)
		{
			try
			{
				var response = await _httpClient.GetAsync($"http://localhost:5218/api/Administrador/{id}");
				Console.WriteLine(response);
				
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					Console.WriteLine(content);

					// Criando JsonSerializerOptions com PropertyNameCaseInsensitive
					var options = new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true,  // Não se importa com maiúsculas/minúsculas
						DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull // Ignora propriedades nulas
					};

					// Desserializando o conteúdo com as opções
					var administrador = JsonSerializer.Deserialize<Administrador>(content, options);
					Console.WriteLine(administrador);
					PreencherCampos(administrador);
				}
				else
				{
					await DisplayAlert("Erro", "Não foi possível obter os dados", "OK");
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert("Erro", $"Erro ao buscar dados: {ex.Message}", "OK");
			}
		}


        // Preencher os campos da interface com os dados do administrador
        private void PreencherCampos(Administrador administrador)
        {
    		NomeAdm.Text = administrador.NomeAdm;
    		IdAdministrador.Text = administrador.IdAdministrador.ToString();
		}

		public class Administrador
		{
			public int IdAdministrador { get; set; }
			public int UnidadeIdUnidade { get; set; }
			public string NomeAdm { get; set; } = string.Empty;
			public string SenhaAdm { get; set; } = string.Empty;
		}
	}
}

