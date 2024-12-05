using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMT.Models;

[Table("consulta")]
public partial class Consultum
{
    [Key]
    [Column("idconsulta")]
    public int IdConsulta { get; set; }

    [Column("paciente_cpfpaci")]
    [Required(ErrorMessage = "O campo CPF do Paciente é obrigatório.")]
    [StringLength(14, ErrorMessage = "O CPF do Paciente deve ter no máximo 14 caracteres.")]
    public string PacienteCpfPaci { get; set; } = null!;

    [Column("atendente_ctpsatend")]
    [Required(ErrorMessage = "O campo CTPS do Atendente é obrigatório.")]
    [StringLength(15, ErrorMessage = "O CTPS do Atendente deve ter no máximo 15 caracteres.")]
    public string AtendenteCtpsAtend { get; set; } = null!;

    [Column("medico_crmmed")]
    [Required(ErrorMessage = "O campo CRM do Médico é obrigatório.")]
    [StringLength(7, ErrorMessage = "O CRM do Médico deve ter no máximo 7 caracteres.")]
    public string MedicoCrmMed { get; set; } = null!;

    [Column("unidade_idunidade")]
    [Required(ErrorMessage = "O campo ID da Unidade é obrigatório.")]
    public int UnidadeIdUnidade { get; set; }

    [Column("especialidade_idespecialidade")]
    [Required(ErrorMessage = "O campo ID da Especialidade é obrigatório.")]
    public int EspecialidadeIdEspecialidade { get; set; }

    [Column("dataconsul")]
    [Required(ErrorMessage = "O campo Data da Consulta é obrigatório.")]
    public DateOnly DataConsul { get; set; }

    [Column("horaconsul")]
    [Required(ErrorMessage = "O campo Hora da Consulta é obrigatório.")]
    public TimeOnly HoraConsul { get; set; }

    [Column("statusconsul")]
    [Required(ErrorMessage = "O campo Status da Consulta é obrigatório.")]
    public string StatusConsul { get; set; } = null!;

    // Navegação para Atendente
    [ForeignKey("AtendenteCtpsAtend")]
    public virtual Atendente AtendenteCtpsAtendNavigation { get; set; } = null!;

    // Navegação para Especialidade
    [ForeignKey("EspecialidadeIdEspecialidade")]
    public virtual Especialidade EspecialidadeIdEspecialidadeNavigation { get; set; } = null!;

    // Navegação para Médico
    [ForeignKey("MedicoCrmMed")]
    public virtual Medico MedicoCrmMedNavigation { get; set; } = null!;

    // Navegação para Paciente
    [ForeignKey("PacienteCpfPaci")]
    public virtual Paciente PacienteCpfPaciNavigation { get; set; } = null!;

    // Navegação para Unidade
    [ForeignKey("UnidadeIdUnidade")]
    public virtual Unidade UnidadeIdUnidadeNavigation { get; set; } = null!;
}