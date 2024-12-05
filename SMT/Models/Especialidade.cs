using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMT.Models;

[Table("especialidade")]
public partial class Especialidade
{
    [Key]
    [Column("idespecialidade")]
    public int Idespecialidade { get; set; }

    [Column("nomeespec")]
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O Nome da especialidade deve ter no máximo 100 caracteres.")]
    public string Nomeespec { get; set; } = null!;

    public virtual ICollection<Consultum> Consulta { get; set; } = new List<Consultum>();
}
