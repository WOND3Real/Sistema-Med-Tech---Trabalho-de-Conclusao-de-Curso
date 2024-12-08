using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmtMAUI
{
    public partial class ListaConsulta : ContentPage
    {
        public ObservableCollection<ConsultaDetalhada> Consultas { get; set; }

        private readonly HttpClient _httpClient;

        public ListaConsulta()
        {
            Console.WriteLine("Construtor de ListaConsulta chamado.");
            InitializeComponent();
            _httpClient = new HttpClient();
            Consultas = new ObservableCollection<ConsultaDetalhada>();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            try
            {
                Console.WriteLine("OnAppearing chamado.");
                base.OnAppearing();
                await CarregarConsultasAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no OnAppearing: {ex.Message}");
            }
        }

        private async Task CarregarConsultasAsync()
        {
            try
            {
                // Obter as consultas da API
                var consultas = await ObterConsultasDetalhadasAsync();

                // Limpar e atualizar a ObservableCollection
                Consultas.Clear();
                foreach (var consulta in consultas)
                {
                    Consultas.Add(consulta);
                }

                Console.WriteLine($"Total de consultas carregadas: {Consultas.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar consultas: {ex.Message}");
            }
        }

        // Método para obter as consultas detalhadas da API
        public async Task<List<ConsultaDetalhada>> ObterConsultasDetalhadasAsync()
        {
            try
            {
                // Chama a API para obter os dados
                var response = await _httpClient.GetStringAsync("http://localhost:5218/api/ConsultaDetalhada");

                // Configurações para deserialização
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // Garante que a deserialização seja insensível a maiúsculas e minúsculas
                };

                // Deserializa a resposta JSON em uma lista de objetos ConsultaDetalhada
                var consultas = JsonSerializer.Deserialize<List<ConsultaDetalhada>>(response, options);
                Console.WriteLine($"Resposta da API: {response}");

                // Verifique se as consultas foram deserializadas corretamente
                Console.WriteLine($"Consultas deserializadas com sucesso. Total de consultas: {consultas?.Count}");

                // Log para verificar se as propriedades estão sendo atribuídas corretamente
                foreach (var consulta in consultas)
                {
                    Console.WriteLine($"Consulta ID: {consulta.IdConsulta}, CPF: {consulta.CpfPaciente}, Consultorio: {consulta.Consultorio}");
                }

                // Retorna a lista de consultas
                return consultas ?? new List<ConsultaDetalhada>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar as consultas: {ex.Message}");
                return new List<ConsultaDetalhada>(); // Retorna uma lista vazia em caso de erro
            }
        }
    }
}

// Classe para os dados da consulta
public class ConsultaDetalhada
{
    public int IdConsulta { get; set; } 
    
    [JsonConverter(typeof(JsonDateOnlyConverter))] // Converter a data para DateOnly
    public DateOnly DataConsulta { get; set; } 
    
    public TimeSpan HoraConsulta { get; set; }
    
    public string CpfPaciente { get; set; } = null!;
    
    public string Especialidade { get; set; } = null!;
    
    public string StatusConsulta { get; set; } = null!;
    
    public double Consultorio { get; set; }

    // Propriedades calculadas para formatar os valores
    public string DataConsultaFormatada => DataConsulta.ToString("dd/MM/yyyy");
    public string HoraConsultaFormatada => HoraConsulta.ToString(@"hh\:mm");
}

// Converte DateOnly para JSON
public class JsonDateOnlyConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.Parse(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
    }
}
