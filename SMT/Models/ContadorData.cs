using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SMT.Models;

public class ContadorData
{
    [Column("total_consultas")]
    [JsonPropertyName("total_consultas")]
    public int TotalConsultas { get; set; }

    [Column("consultas_concluidas")]
    [JsonPropertyName("consultas_concluidas")]
    public int ConsultasConcluidas { get; set; }
    
    [Column("consultas_canceladas")]
    [JsonPropertyName("consultas_canceladas")]
    public int ConsultasCanceladas { get; set; }

    [Column("total_contribuintes")]
    [JsonPropertyName("total_contribuintes")]
    public int TotalContribuintes { get; set; }
}