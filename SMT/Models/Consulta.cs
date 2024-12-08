using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("consulta")]
public class Consulta
{
    [Key]
    [Column("idconsulta")]
    public int IdConsulta { get; set; }

    [Required]
    [StringLength(14)]
    [Column("paciente_cpfpaci")]
    public string PacienteCpfPaci { get; set; }

    [Required]
    [StringLength(15)]
    [Column("atendente_ctpsatend")]
    public string AtendenteCtpsatend { get; set; }

    [Required]
    [StringLength(7)]
    [Column("medico_crmmed")]
    public string MedicoCrmmed { get; set; }

    [Required]
    [Column("unidade_idunidade")]
    public int UnidadeIdunidade { get; set; }

    [Required]
    [Column("especialidade_idespecialidade")]
    public int EspecialidadeIdespecialidade { get; set; }

    [Required]
    [Column("dataconsul")]
    public DateTime DataConsul { get; set; }

    [Required]
    [Column("horaconsul")]
    public TimeSpan HoraConsul { get; set; }

    [Required]
    [StringLength(15)]
    [Column("statusconsul")]
    public string StatusConsul { get; set; }
}
