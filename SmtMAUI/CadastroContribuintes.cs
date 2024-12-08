using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace SmtMAUI;

public partial class CadastroContribuintes : ContentPage
{
    public ObservableCollection<Contribuinte> Contribuintes { get; set; }
    private readonly HttpClient _httpClient;

    public CadastroContribuintes()
    {
        InitializeComponent();
        Contribuintes = new ObservableCollection<Contribuinte>();
        _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5218/api/contribuintes") }; // Ajuste a URL conforme seu servidor
        BindingContext = this;

        // Carregar os dados ao iniciar a página
        CarregarContribuintes();
    }

    

    // Método para carregar os contribuintes do backend
    private async void CarregarContribuintes()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<Contribuinte>>(""); // URL correta sem a repetição de "contribuintes"

            Console.WriteLine(response);

            if (response != null)
            {
                Contribuintes.Clear(); // Limpar lista antes de adicionar novos itens

                foreach (var contribuinte in response)
                {
                    // A propriedade GuicheConsultorio será calculada automaticamente quando necessário
                    Console.WriteLine($"Consultório/Guichê: {contribuinte.GuicheConsultorio}"); // Exibindo o valor formatado

                    // Adicionando o contribuinte à lista
                    Contribuintes.Add(contribuinte);
                }
            }
        }
        catch (Exception ex)
        {
            // Caso haja erro na requisição, exiba uma mensagem de erro
            await DisplayAlert("Erro", "Não foi possível carregar os dados. Tente novamente mais tarde.", "OK");
            Console.WriteLine(ex.Message);
        }
    }





    // Método para adicionar novo contribuinte (simulando com dados locais por enquanto)
    private async void OnAdicionarContribuinteClicked(object sender, EventArgs e)
    {
        string nome = await DisplayPromptAsync("Novo Contribuinte", "Digite o nome:");
        string ocupacao = await DisplayPromptAsync("Novo Contribuinte", "Digite a ocupação:");
        string identificador = await DisplayPromptAsync("Novo Contribuinte", "Digite o identificador:");
        string especialidade = await DisplayPromptAsync("Novo Contribuinte", "Digite a especialidade:");
        string guicheConsultorioStr = await DisplayPromptAsync("Novo Contribuinte", "Digite o número do consultório/guichê:");

        if (!string.IsNullOrWhiteSpace(nome) && !string.IsNullOrWhiteSpace(ocupacao) && double.TryParse(guicheConsultorioStr, out double guicheConsultorio))
        {
            var novoContribuinte = new Contribuinte
            {
                Nome = nome,
                Ocupacao = ocupacao,
                Identificador = identificador,
                ConsultorioOuGuiche = guicheConsultorio, // Atribuindo o valor correto
                Especialidade = especialidade
            };

            // Adicionando o novo contribuinte na coleção local
            Contribuintes.Add(novoContribuinte);

            // Aqui você pode adicionar lógica para enviar os dados para o backend via POST, se necessário.
            Console.WriteLine($"Contribuinte adicionado: {nome}, {ocupacao}");
        }
    }

}

// Classe para os dados do contribuinte (ajustada para refletir o modelo do backend)
public class Contribuinte
{
    public string Nome { get; set; }
    public string Ocupacao { get; set; }
    public string Identificador { get; set; }
    public double ConsultorioOuGuiche { get; set; } // Propriedade para armazenar o valor
    public string Especialidade { get; set; }

    // Propriedade que formata o valor de ConsultorioOuGuiche
    public string GuicheConsultorio => $"Guichê/Consultório: {ConsultorioOuGuiche:F0}";
}
