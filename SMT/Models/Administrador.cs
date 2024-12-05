using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMT.Models;

[Table("administrador")]
public partial class Administrador
{
    [Key]
    [Column("idadministrador")]
    [Required(ErrorMessage = "O campo ID do Administrador é obrigatório.")]
    public int IdAdministrador { get; set; }

    [Column("unidade_idunidade")]
    [Required(ErrorMessage = "O campo Unidade é obrigatório.")]
    public int UnidadeIdUnidade { get; set; }

    [Column("nomeadm")]
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(20, ErrorMessage = "O Nome deve ter no máximo 20 caracteres.")]
    public string NomeAdm { get; set; } = null!;

    [Column("sobrenomeadm")]
    [Required(ErrorMessage = "O campo Sobrenome é obrigatório.")]
    [StringLength(20, ErrorMessage = "O Sobrenome deve ter no máximo 20 caracteres.")]
    public string SobrenomeAdm { get; set; } = null!;

    [Column("senhaadm")]
    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    [StringLength(20, ErrorMessage = "A Senha deve ter no máximo 20 caracteres.")]
    public string SenhaAdm { get; set; } = null!;

    [ForeignKey("UnidadeIdUnidade")]
    public virtual Unidade UnidadeIdUnidadeNavigation { get; set; } = null!;
}
