using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Mobile_SMT;

public partial class AgendamentoMed : ContentPage
{
    private readonly HttpClient _httpClient;

    public AgendamentoMed()
    {
        InitializeComponent();

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5218/api/") // Substitua pelo endereço real da API
        };
    }

    // Evento para botão "Voltar"
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    // Evento para botão "Agendar Consulta"
    private async void OnAgendarButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HoraAgendamento());
    }

    // Método para consumir a API
    private async Task<List<Agendamento>> GetMedicosEspecialidadesAsync(int unidadeId)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<List<Agendamento>>($"medicosespecialidades/unidade/{unidadeId}");
            return response ?? new List<Agendamento>();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Não foi possível carregar os médicos: {ex.Message}", "OK");
            return new List<Agendamento>();
        }
    }

    // Carregar dados ao abrir a página
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Substitua 1 pelo ID real da unidade
        int unidadeId = SharedData.IdUnidadeSelecionada; 
        var medicos = await GetMedicosEspecialidadesAsync(unidadeId);

        if (medicos.Any())
        {
            BindingContext = medicos.First(); // Exemplo: Exibe o primeiro médico
        }
        else
        {
            await DisplayAlert("Atenção", "Nenhum médico encontrado para esta unidade.", "OK");
        }
    }
}

// Classe modelo para dados do médico
public class Agendamento
{
    public string CrmMedico { get; set; }
    public string NomeMedico { get; set; }
    public string SobrenomeMedico { get; set; }
    public string EspecialidadeNome { get; set; }
    public int UnidadeId { get; set; }
}
