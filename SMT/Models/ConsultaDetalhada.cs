using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("consultadetalhada")]
public class ConsultaDetalhada
{
    [Key]
    [Column("idconsulta")]
    public int IdConsulta { get; set; }      
    
    [Column("dataconsulta")]
    [DataType(DataType.Date)]
    public DateOnly DataConsulta { get; set; }  
    
    [Column("horaconsulta")]
    [DataType(DataType.Time)]
    public TimeOnly HoraConsulta { get; set; }  
    
    [Column("cpfpaciente")]
    public string CpfPaciente { get; set; } = null!;  
    
    [Column("especialidade")]
    public string Especialidade { get; set; } = null!;
    
    [Column("statusconsulta")]
    public string StatusConsulta { get; set; } = null!;
    
    [Column("consultorio")]
    public double Consultorio { get; set; }
}
