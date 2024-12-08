using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmtMAUI
{
    public partial class PainelADM : ContentPage
    {
        private readonly HttpClient _httpClient;

        // Injeção de dependência
        public PainelADM(HttpClient httpClient)
        {
            InitializeComponent();
            _httpClient = httpClient;
            OnAppearing(); // Carregar dados ao iniciar a página
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            try
            {
                // Chama a API
                string apiUrl = "http://localhost:5218/api/contadorData/consultas-contribuintes"; // Substitua pela URL correta da API
                var response = await _httpClient.GetStringAsync(apiUrl);

                // Desserializa a resposta JSON para o modelo ContadorData usando Newtonsoft.Json
                var data = JsonConvert.DeserializeObject<ContadorData>(response);
                
                Console.WriteLine(response);

                // Verifica se a resposta é válida
                if (data != null)
                {
                    // Atribui os valores aos labels
                    NumConsultas.Text = data.TotalConsultas.ToString();
                    NumContribuentes.Text = data.TotalContribuintes.ToString();
                    NumConcluidas.Text = data.ConsultasConcluidas.ToString();
                    NumCanceldas.Text = data.ConsultasCanceladas.ToString();
                }
                else
                {
                    Console.WriteLine("Resposta da API inválida.");
                }
            }
            catch (Exception ex)
            {
                // Se houver erro, exibe a mensagem de erro
                Console.WriteLine($"Erro ao carregar dados: {ex.Message}");
            }
        }
    }
}

public class ContadorData
{
    public int TotalConsultas { get; set; }
    public int TotalContribuintes { get; set; }
    public int ConsultasConcluidas { get; set; }
    public int ConsultasCanceladas { get; set; }
}