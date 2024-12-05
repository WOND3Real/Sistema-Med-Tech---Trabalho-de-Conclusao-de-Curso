using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMT.Models;

[Table("unidade")]
public partial class Unidade
{
    [Key]
    [Column("idunidade")]
    public int Idunidade { get; set;}

    [Column("nomeunidade")]
    [Required(ErrorMessage = "O campo Nome da Unidade é obrigatório.")]
    [StringLength(20, ErrorMessage = "O Nome da Unidade deve ter no máximo 20 caracteres.")]
    public string Nomeunidade { get; set; } = null!;

    [Column("cepuni")]
    [Required(ErrorMessage = "O campo CEP é obrigatório.")]
    [StringLength(10, ErrorMessage = "O CEP deve ter no máximo 10 caracteres.")]
    public string Cepuni { get; set; } = null!;

    [Column("logradourouni")]
    [Required(ErrorMessage = "O campo Logradouro é obrigatório.")]
    [StringLength(45, ErrorMessage = "O Logradouro deve ter no máximo 45 caracteres.")]
    public string Logradourouni { get; set; } = null!;

    [Column("numerouni")]
    [Required(ErrorMessage = "O campo Número é obrigatório.")]
    [StringLength(6, ErrorMessage = "O Número deve ter no máximo 6 caracteres.")]
    public string Numerouni { get; set; } = null!;

    [Column("bairrouni")]
    [Required(ErrorMessage = "O campo Bairro é obrigatório.")]
    [StringLength(20, ErrorMessage = "O Bairro deve ter no máximo 20 caracteres.")]
    public string Bairrouni { get; set; } = null!;

    [Column("cidadeuni")]
    [Required(ErrorMessage = "O campo Cidade é obrigatório.")]
    [StringLength(20, ErrorMessage = "A Cidade deve ter no máximo 20 caracteres.")]
    public string Cidadeuni { get; set; } = null!;

    [Column("estadouni")]
    [Required(ErrorMessage = "O campo Estado é obrigatório.")]
    [StringLength(20, ErrorMessage = "O Estado deve ter no máximo 20 caracteres.")]
    public string Estadouni { get; set; } = null!;

    [Column("paisuni")]
    [Required(ErrorMessage = "O campo País é obrigatório.")]
    [StringLength(20, ErrorMessage = "O País deve ter no máximo 20 caracteres.")]
    public string Paisuni { get; set; } = null!;

    public virtual ICollection<Administrador> Administradors { get; set; } = new List<Administrador>();

    public virtual ICollection<Consultum> Consulta { get; set; } = new List<Consultum>();
}
