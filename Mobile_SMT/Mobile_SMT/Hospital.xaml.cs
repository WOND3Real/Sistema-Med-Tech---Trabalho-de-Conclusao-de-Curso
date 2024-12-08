using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace Mobile_SMT
{
    public partial class Hospital : ContentPage
    {
        private HttpClient _httpClient;
        public ObservableCollection<Unidade> Unidades { get; set; }

        public Hospital()
        {
            InitializeComponent();
            Console.WriteLine("Inicializando Hospital...");

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5218/api/Unidade") // Atualize com a URL do seu backend
            };
            Unidades = new ObservableCollection<Unidade>();
            BindingContext = this;

            Console.WriteLine("Carregando unidades...");
            LoadUnidadesAsync();
        }

        private async void LoadUnidadesAsync()
        {
            try
            {
                Console.WriteLine("Tentando carregar unidades...");

                var unidades = await _httpClient.GetFromJsonAsync<List<Unidade>>("Unidade");
                if (unidades != null)
                {
                    Console.WriteLine($"Unidades encontradas: {unidades.Count}");
                    foreach (var unidade in unidades)
                    {
                        Unidades.Add(unidade);
                        Console.WriteLine($"Unidade adicionada: {unidade.NomeUnidade}");
                    }
                }
                else
                {
                    Console.WriteLine("Nenhuma unidade foi retornada.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar unidades: {ex.Message}");
                await DisplayAlert("Erro", $"Erro ao carregar unidades: {ex.Message}", "OK");
            }
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Botão de voltar clicado.");
            await Navigation.PopAsync();
        }

        private async void OnScheduleButtonClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Botão de agendar clicado.");
            
            // Você ainda pode acessar o item selecionado no CollectionView através da fonte de dados
            if (sender is Button button)
            {
                // Encontrar o contexto de dados (Unidade) associado ao botão
                var unidadeSelecionada = button.BindingContext as Unidade;

                if (unidadeSelecionada != null)
                {
                    // Armazenando o ID da unidade selecionada em SharedData
                    SharedData.IdUnidadeSelecionada = unidadeSelecionada.IdUnidade;
                    Console.WriteLine($"Unidade selecionada: {unidadeSelecionada.NomeUnidade}, Id: {SharedData.IdUnidadeSelecionada}");
                    
                    // Navegação para a próxima tela
                    await Navigation.PushAsync(new MedicoEspecialidade());
                }
            }
        }

    }

    public class Unidade
    {
        public int IdUnidade { get; set; }
        public string NomeUnidade { get; set; } = string.Empty;
        public string CepUni { get; set; } = string.Empty;
        public string LogradouroUni { get; set; } = string.Empty;
        public string BairroUni { get; set; } = string.Empty;
        public string CidadeUni { get; set; } = string.Empty;
        public string EstadoUni { get; set; } = string.Empty;
        public string PaisUni { get; set; } = string.Empty;
    }
}
