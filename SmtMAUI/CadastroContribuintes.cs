using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace SmtMAUI;

public partial class CadastroContribuintes : ContentPage
{
    public ObservableCollection<Contribuinte> Contribuintes { get; set; }

    public CadastroContribuintes()
    {
        InitializeComponent(); // Essa chamada deve ser feita ao método gerado automaticamente
        Contribuintes = new ObservableCollection<Contribuinte>
        {
            new Contribuinte { Nome = "Nome do Contribuinte", Ocupacao = "Atendente", Identificador = "CPF", GuicheConsultorio = "Guichê: N", Especialidade = "N/A" },
            new Contribuinte { Nome = "Nome do Contribuinte", Ocupacao = "Médico", Identificador = "CRM", GuicheConsultorio = "Consultório: N", Especialidade = "Especialidade" }
        };
        BindingContext = this;
    }

    // Método para adicionar novo contribuinte
    private async void OnAdicionarContribuinteClicked(object sender, EventArgs e)
    {
        string nome = await DisplayPromptAsync("Novo Contribuinte", "Digite o nome:");
        string ocupacao = await DisplayPromptAsync("Novo Contribuinte", "Digite a ocupação:");
        string identificador = await DisplayPromptAsync("Novo Contribuinte", "Digite o identificador:");
        string guicheConsultorio = await DisplayPromptAsync("Novo Contribuinte", "Digite o guichê ou consultório:");
        string especialidade = await DisplayPromptAsync("Novo Contribuinte", "Digite a especialidade:");

        if (!string.IsNullOrWhiteSpace(nome) && !string.IsNullOrWhiteSpace(ocupacao))
        {
            Contribuintes.Add(new Contribuinte
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
public class Contribuinte
{
    public string Nome { get; set; }
    public string Ocupacao { get; set; }
    public string Identificador { get; set; }
    public string GuicheConsultorio { get; set; }
    public string Especialidade { get; set; }
}
