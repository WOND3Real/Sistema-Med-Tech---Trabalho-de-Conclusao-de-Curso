using Newtonsoft.Json;

public class ContadorData
{
    [JsonProperty("total_consultas")]
    public int TotalConsultas { get; set; }

    [JsonProperty("consultas_concluidas")]
    public int ConsultasConcluidas { get; set; }

    [JsonProperty("consultas_canceladas")]
    public int ConsultasCanceladas { get; set; }

    [JsonProperty("total_contribuintes")]
    public int TotalContribuintes { get; set; }
}
