using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMT.Models;

[Table("administrador")]
public partial class Administrador
{
    [Key]
    [Column("idadministrador")]
    public int IdAdministrador { get; set; }

    [Column("unidade_idunidade")]
    public int UnidadeIdUnidade { get; set; }

    [Column("nomeadm")]
    public string NomeAdm { get; set; } = null!;

    [Column("sobrenomeadm")]
    public string SobrenomeAdm { get; set; } = null!;

    [Column("senhaadm")]
    public string SenhaAdm { get; set; } = null!;
}
