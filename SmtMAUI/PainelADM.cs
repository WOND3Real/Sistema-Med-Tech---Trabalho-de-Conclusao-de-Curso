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
            LoadDataAsync(); // Carregar dados ao iniciar a página
        }

        private async void LoadDataAsync()
        {
            try
            {
                // Chama a API
                string apiUrl = "http://localhost:5218/api/contadorData/consultas-contribuintes";
                var response = await _httpClient.GetStringAsync(apiUrl);

                // Imprime a resposta da API
                Console.WriteLine("Resposta da API: " + response);

                // Desserializa a resposta JSON para o modelo ContadorData usando Newtonsoft.Json
                var data = JsonConvert.DeserializeObject<ContadorData>(response);

                // Imprime os dados após a deserialização
                if (data != null)
                {
                    Console.WriteLine($"Consultas: {data.TotalConsultas}, Concluídas: {data.ConsultasConcluidas}, Canceladas: {data.ConsultasCanceladas}, Contribuintes: {data.TotalContribuintes}");
                }
                else
                {
                    Console.WriteLine("Falha na deserialização dos dados.");
                }


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