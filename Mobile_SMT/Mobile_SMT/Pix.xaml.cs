using System;
using Microsoft.Maui.Controls;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace Mobile_SMT
{
    public partial class Pix : ContentPage
    {
        private readonly HttpClient _httpClient;

        public Pix()
        {
            InitializeComponent();
            _httpClient = new HttpClient(); // Inicializa o HttpClient
            Console.WriteLine("Construtor Pix iniciado.");
        }

        // Evento para o botão de voltar
        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Botão Voltar clicado.");

            // Voltar para a página anterior
            await Navigation.PopAsync();
            Console.WriteLine("Navegação de volta concluída.");
        }

        // Evento para o botão de copiar código Pix
        private async void copiar(object sender, EventArgs e)
        {
            Console.WriteLine("Botão Copiar clicado.");

            // Código Pix que será copiado
            string pixCode = "123e4567-e89b-12d3-a456-426614174000";

            // Copiar para a área de transferência
            await Clipboard.SetTextAsync(pixCode);
            Console.WriteLine("Código Pix copiado para a área de transferência.");

            // Exibir mensagem de confirmação
            await DisplayAlert("Sucesso", "Código Pix copiado!", "OK");
            Console.WriteLine("Mensagem de sucesso exibida.");
        }

        // Evento para o botão "Concluído"
        private async void prximaPag(object sender, EventArgs e)
        {
            Console.WriteLine("Botão 'Concluído' clicado.");

            try
            {
                // Nome da especialidade que você quer buscar (por exemplo, vindo de SharedData ou da interface)
                string nomeEspecialidade = SharedData.NomeEspecialidadeSelecionada;
                Console.WriteLine($"Buscando ID da especialidade com o nome: {nomeEspecialidade}");

                // Chamada ao backend para buscar o ID da especialidade pelo nome
                var response = await _httpClient.GetStringAsync($"http://localhost:5218/api/Especialidade/{nomeEspecialidade}");
                Console.WriteLine("Resposta da API recebida.");

                // Desserializa a resposta usando System.Text.Json
                var result = JsonSerializer.Deserialize<JsonElement>(response);
                Console.WriteLine("Resposta desserializada.");

                // Modificação: Acesso correto ao valor "idEspecialidade"
                if (result.TryGetProperty("idEspecialidade", out var idEspecialidade))
                {
                    // Associa o ID da especialidade à SharedData
                    SharedData.EspecialidadeSelecionada = idEspecialidade.GetInt32();
                    Console.WriteLine($"ID da especialidade encontrado: {SharedData.EspecialidadeSelecionada}");

                    // Exibir mensagem de confirmação
                    await DisplayAlert("Transação Concluída", "A transação foi concluída com sucesso.", "OK");
                    Console.WriteLine("Mensagem de transação concluída exibida.");

                    // Navegar para a página "Fim"
                    await Navigation.PushAsync(new Fim()); // Substitua "Fim" pelo nome da classe da página de destino.
                    Console.WriteLine("Navegação para a página Fim.");
                }
                else
                {
                    await DisplayAlert("Erro", "Especialidade não encontrada.", "OK");
                    Console.WriteLine("Especialidade não encontrada.");
                }
            }
            catch (Exception ex)
            {
                // Tratar erro
                await DisplayAlert("Erro", $"Erro ao buscar especialidade: {ex.Message}", "OK");
                Console.WriteLine($"Erro ao buscar especialidade: {ex.Message}");
            }
        }
    }
}
