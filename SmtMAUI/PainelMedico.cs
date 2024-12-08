using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace SmtMAUI;

public partial class PainelMedico : ContentPage
{
    private readonly HttpClient _httpClient;
    private ConsultaPaciente? _consulta;

    public PainelMedico()
    {
        InitializeComponent();
        _httpClient = new HttpClient();
        _ = CarregarDadosConsulta(GerarIdConsultaAleatorio()); 
    }

    private int GerarIdConsultaAleatorio(int min = 1, int max = 5)
    {
        Random random = new Random();
        return random.Next(min, max);
    }

    private async Task CarregarDadosConsulta(int idConsulta)
    {
        try
        {
            string url = $"http://localhost:5218/api/consultapaciente/{idConsulta}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"JSON recebido: {json}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters =
                    {
                        new DateOnlyJsonConverter(),
                        new TimeOnlyJsonConverter()
                    }
                };

                _consulta = JsonSerializer.Deserialize<ConsultaPaciente>(json, options);

                if (_consulta != null)
                {
                    AtualizarInterface();
                }
                else
                {
                    await DisplayAlert("Erro", "Não foi possível processar os dados da consulta.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Erro", $"Erro na API: {response.StatusCode}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Erro ao acessar a API: {ex.Message}", "OK");
        }
    }

    private void AtualizarInterface()
    {
        if (_consulta != null)
        {
            // Atualize os elementos da interface com os dados da consulta
            ConsultaLabel.Text = $"Consulta: {_consulta.IdConsulta}";
            DataConsultaFormatada.Text = $"Data: {_consulta.DataConsul:dd/MM/yyyy}";
            HoraConsultaFormatada.Text = $"Hora: {_consulta.HoraConsul}";
            NomePacienteLabel.Text = $"Nome: {_consulta.NomePaciente}";
            NascimentoPacienteLabel.Text = $"Nascimento: {_consulta.DataNascimento:dd/MM/yyyy}";
            CpfPacienteLabel.Text = $"CPF: {_consulta.CpfPaciente}";
            SexoPacienteLabel.Text = $"Sexo: {_consulta.SexoPaciente}";
        }
    }

	public class ConsultaPaciente
    {
        public int IdConsulta { get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))] // Conversor para DateOnly
        public DateOnly DataConsul { get; set; }

        [JsonConverter(typeof(TimeOnlyJsonConverter))] // Conversor para TimeOnly
        public TimeOnly HoraConsul { get; set; }

        public string NomePaciente { get; set; } = null!;

        public DateOnly DataNascimento { get; set; }

        public string CpfPaciente { get; set; } = null!;

       public string SexoPaciente { get; set; } = null!;

        // Propriedades calculadas para formatação
        public string DataConsultaFormatada => DataConsul.ToString("dd/MM/yyyy");
        public string HoraConsultaFormatada => HoraConsul.ToString(@"hh\:mm");
    }
    
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private const string Format = "yyyy-MM-dd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateOnly.ParseExact(reader.GetString() ?? string.Empty, Format);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format));
        }
    }

    public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
    {
        private const string Format = "HH:mm:ss";

        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return TimeOnly.ParseExact(reader.GetString() ?? string.Empty, Format);
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format));
        }
    }

}
