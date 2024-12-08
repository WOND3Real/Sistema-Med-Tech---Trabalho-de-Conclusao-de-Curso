using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMT.Models;
[Table("medicosespecialidades")]
public class MedicoEspecialidade
{
    [Key]
    [Column("crmmedico")]
    public string CrmMedico { get; set; }

    [Required]
    [StringLength(100)]
    [Column("nomemedico")]
    public string NomeMedico { get; set; }

    [Required]
    [StringLength(100)]
    [Column("sobrenomemedico")]
    public string SobrenomeMedico { get; set; }

    [Required]
    [StringLength(100)]
    [Column("especialidadenome")]
    public string EspecialidadeNome { get; set; }

    [Required]
    [Column("unidadeid")]
    public int UnidadeId { get; set; }
}

