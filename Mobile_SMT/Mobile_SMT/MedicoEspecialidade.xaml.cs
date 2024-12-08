using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Mobile_SMT.Services;

namespace Mobile_SMT
{
    public partial class MedicoEspecialidade : ContentPage
    {
        // Propriedades para armazenar os dados dos médicos
        public ObservableCollection<Medico> Medicos { get; set; }
        private readonly HttpClient _httpClient;

        // Variável para verificar se os médicos já foram carregados
        private bool _medicosCarregados = false;

        public MedicoEspecialidade()
        {
            InitializeComponent();
            Console.WriteLine("Construtor de MedicoEspecialidade chamado");
            _httpClient = new HttpClient();
            Medicos = new ObservableCollection<Medico>();
            BindingContext = this;
        }

        // Método que será chamado quando a página aparecer
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Verificar se os médicos já foram carregados
            if (_medicosCarregados)
            {
                Console.WriteLine("Médicos já carregados, pulando chamada à API.");
                return;
            }

            Console.WriteLine("Método OnAppearing chamado");

            // Exemplo de ID de unidade, você pode passar dinamicamente
            var unidadeId = SharedData.IdUnidadeSelecionada;
            Console.WriteLine($"unidadeId: {unidadeId}");

            // Obter os médicos da API
            var medicos = await GetMedicosByUnidadeIdAsync(unidadeId);

            if (medicos != null && medicos.Any())
            {
                Console.WriteLine("Médicos encontrados, atualizando ObservableCollection");
                Medicos.Clear();

                // Filtrar duplicatas com base no CRM do médico
                var distinctMedicos = medicos.DistinctBy(m => m.CrmMedico).ToList();

                foreach (var medico in distinctMedicos)
                {
                    Medicos.Add(medico);
                    Console.WriteLine($"Médico adicionado: {medico.NomeMedico}, CRM: {medico.CrmMedico}");
                }

                // Marcar que os médicos foram carregados
                _medicosCarregados = true;
            }
            else
            {
                Console.WriteLine("Nenhum médico encontrado");
            }
        }

        // Método para buscar médicos pela unidade via API
        public async Task<List<Medico>> GetMedicosByUnidadeIdAsync(int unidadeId)
        {
            Console.WriteLine($"Método GetMedicosByUnidadeIdAsync chamado com unidadeId: {unidadeId}");
            try
            {
                // Chamada à API para obter os médicos
                var response = await _httpClient.GetFromJsonAsync<List<Medico>>($"http://localhost:5218/api/medicosespecialidades/unidade/{unidadeId}");

                if (response == null)
                {
                    Console.WriteLine("Nenhuma resposta recebida da API");
                }
                else
                {
                    Console.WriteLine($"Recebidos {response.Count} médicos da API");
                }

                return response ?? new List<Medico>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao chamar a API: {ex.Message}");
                return new List<Medico>();
            }
        }

        // Método para armazenar o CRM e navegar para outra página
        private async void OnAgendarButtonClicked(object sender, EventArgs e)
        {
            if (((Button)sender).BindingContext is Medico medicoSelecionado)
            {
                SharedData.CrmMedicoSelecionado = medicoSelecionado.CrmMedico;
                SharedData.NomeEspecialidadeSelecionada = medicoSelecionado.EspecialidadeNome;
                Console.WriteLine($"CRM Selecionado: {SharedData.CrmMedicoSelecionado}, Especialidade Selecionada {SharedData.NomeEspecialidadeSelecionada}");

                // Navegar para a próxima página
                await Navigation.PushAsync(new AgendamentoMed());
            }
            else
            {   
                Console.WriteLine(BindingContext);
                Console.WriteLine("Erro: BindingContext não é do tipo Medico.");
            }
        }

        // Método para o botão Voltar
        private async void OnBackButtonClick(object sender, EventArgs e)
        {
            Console.WriteLine("Método OnBackButtonClick chamado");
            await Navigation.PopAsync();
        }

        // Modelo de Medico para vinculação de dados
        public class Medico
        {
            public string CrmMedico { get; set; }
            public string NomeMedico { get; set; }
            public string SobrenomeMedico { get; set; }
            public string EspecialidadeNome { get; set; }
        }
    }
}
