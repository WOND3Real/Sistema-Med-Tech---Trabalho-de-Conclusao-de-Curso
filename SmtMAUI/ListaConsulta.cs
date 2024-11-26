using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace SmtMAUI;

public partial class ListaConsulta : ContentPage
{
    public ObservableCollection<Consulta> Consulta { get; set; }

    public ListaConsulta()
    {
        InitializeComponent(); 
        Consulta = new ObservableCollection<Consulta>
        {
            new Consulta { Nome = "Nome do Contribuinte", Ocupacao = "Atendente", Identificador = "CPF", GuicheConsultorio = "Guichê: N", Especialidade = "N/A" },
            new Consulta { Nome = "Nome do Contribuinte", Ocupacao = "Médico", Identificador = "CRM", GuicheConsultorio = "Consultório: N", Especialidade = "Especialidade" }
        };
        BindingContext = this;
    }

    // Método para adicionar novo contribuinte
    private async void OnAdicionarContribuinteClicked(object sender, EventArgs e)
    {
        string nome = await DisplayPromptAsync("Novo Contribuinte", "Digite o Codigo:");
        string ocupacao = await DisplayPromptAsync("Novo Contribuinte", "Digite a Data:");
        string identificador = await DisplayPromptAsync("Novo Contribuinte", "Digite a Hora:");
        string guicheConsultorio = await DisplayPromptAsync("Novo Contribuinte", "Digite o CPF do Paciente:");
        string especialidade = await DisplayPromptAsync("Novo Contribuinte", "Digite a especialidade:");

        if (!string.IsNullOrWhiteSpace(nome) && !string.IsNullOrWhiteSpace(ocupacao))
        {
            Consulta.Add(new Consulta
            {
                Nome = nome,
                Ocupacao = ocupacao,
                Identificador = identificador,
                GuicheConsultorio = guicheConsultorio,
                Especialidade = especialidade
            });
        }
    }
}

// Classe para os dados do contribuinte
public class Consulta
{
    public string Nome { get; set; }
    public string Ocupacao { get; set; }
    public string Identificador { get; set; }
    public string GuicheConsultorio { get; set; }
    public string Especialidade { get; set; }
}
