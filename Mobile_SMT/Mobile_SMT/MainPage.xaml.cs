using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Mobile_SMT
{
    public partial class MainPage : ContentPage
    {
        private const string ApiUrl = "http://localhost:5218/api/Paciente/login";
        private readonly UserSessionService _userSessionService;

        // Construtor onde o UserSessionService é injetado
        public MainPage(UserSessionService userSessionService)
        {
            InitializeComponent();
            _userSessionService = userSessionService;
        }

        // Método para definir o CPF do paciente
        public void SetCpfPaciente(string cpfDoPaciente)
        {
            _userSessionService.SetCpfPaciente(cpfDoPaciente);
        }

        // Método chamado quando o botão de login é clicado
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Erro", "Por favor, preencha todos os campos!", "OK");
                return;
            }

            try
            {
                var clienteHttp = new HttpClient();
                var payload = new
                {
                    EmailPaci = email,
                    Senhapaci = password
                };

                var jsonPayload = JsonSerializer.Serialize(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await clienteHttp.PostAsync(ApiUrl, content);

                Console.WriteLine($"Resposta HTTP: {response}");

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Dados da Resposta: {responseData}");

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true // Ignorar diferença entre maiúsculas/minúsculas
                    };

                    var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseData, options);

                    if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.CpfPaciente))
                    {
                        Console.WriteLine($"CPF Retornado: {loginResponse.CpfPaciente}");

                        // Verifica se o CPF contém caracteres inválidos
                        if (loginResponse.CpfPaciente.Any(c => char.IsLetter(c)))
                        {
                            await DisplayAlert("Erro", "CPF inválido, contém letras!", "OK");
                            return;
                        }

                        // Armazena o CPF do paciente na sessão
                        _userSessionService.SetCpfPaciente(loginResponse.CpfPaciente);

                        // Notifica o sucesso do login
                        await DisplayAlert("Sucesso", "Login realizado com sucesso!", "OK");
                        Console.WriteLine(SharedData.CpfPacienteSelecionado);

                        // Navega para a tela inicial
                        await Navigation.PushAsync(new Home());
                    }
                    else
                    {
                        // Caso o CPF não seja retornado, exibe erro
                        await DisplayAlert("Erro", "Login inválido, CPF não encontrado!", "OK");
                    }
                }
                else
                {
                    // Login inválido
                    await DisplayAlert("Erro", "Usuário ou senha inválidos!", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
            }
        }

        // Método para navegar para a tela de cadastro
        private void OnRegisterTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Cadastro());
        }
    }

    public class UserSessionService
    {
        private string _cpfPaciente;

        public void SetCpfPaciente(string cpf)
        {
            _cpfPaciente = cpf;
            SharedData.CpfPacienteSelecionado = cpf;
        }

        public string GetCpfPaciente()
        {
            return _cpfPaciente;
        }
    }

    // Classe para deserializar a resposta do login
    public class LoginResponse
    {
        [JsonPropertyName("cpfPaciente")] // Mapeia para o campo no JSON
        public string CpfPaciente { get; set; }
    }
}
