using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMT.Models;

[Table("medico")]
public partial class Medico
{
    [Key]
    [Column("crmmed")]
    [Required(ErrorMessage = "O campo CRM é obrigatório.")]
    [StringLength(7, ErrorMessage = "O CRM deve ter no máximo 7 caracteres.")]
    public string Crmmed { get; set; } = null!;

    [Column("nomemed")]
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(45, ErrorMessage = "O Nome deve ter no máximo 45 caracteres.")]
    public string Nomemed { get; set; } = null!;

    [Column("sobrenomemed")]
    [Required(ErrorMessage = "O campo Sobrenome é obrigatório.")]
    [StringLength(45, ErrorMessage = "O Sobrenome deve ter no máximo 45 caracteres.")]
    public string Sobrenomemed { get; set; } = null!;

    [Column("telefonemed")]
    [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
    [StringLength(14, ErrorMessage = "O Telefone deve ter no máximo 14 caracteres.")]
    [Phone(ErrorMessage = "O campo Telefone deve ser um número válido.")]
    public string Telefonemed { get; set; } = null!;

    [Column("emailmed")]
    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [StringLength(45, ErrorMessage = "O Email deve ter no máximo 45 caracteres.")]
    [EmailAddress(ErrorMessage = "O campo Email deve ser um email válido.")]
    public string Emailmed { get; set; } = null!;

    [Column("senhamed")]
    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    [StringLength(20, ErrorMessage = "A Senha deve ter no máximo 20 caracteres.")]
    public string Senhamed { get; set; } = null!;

    public virtual ICollection<Consultum> Consulta { get; set; } = new List<Consultum>();
}
