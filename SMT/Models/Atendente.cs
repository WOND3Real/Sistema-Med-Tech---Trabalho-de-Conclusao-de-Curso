using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMT.Models;

[Table("atendente")]
public partial class Atendente
{
    [Key]
    [Column("ctpsatend")]
    [Required(ErrorMessage = "O campo CTPS é obrigatório.")]
    [StringLength(15, ErrorMessage = "O CTPS deve ter no máximo 15 caracteres.")]
    public string Ctpsatend { get; set; } = null!;

    [Column("nomeatend")]
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(45, ErrorMessage = "O Nome deve ter no máximo 45 caracteres.")]
    public string Nomeatend { get; set; } = null!;

    [Column("sobrenomeatend")]
    [Required(ErrorMessage = "O campo Sobrenome é obrigatório.")]
    [StringLength(45, ErrorMessage = "O Sobrenome deve ter no máximo 45 caracteres.")]
    public string Sobrenomeatend { get; set; } = null!;

    [Column("inicioturnoatend")]
    [Required(ErrorMessage = "O campo Início do Turno é obrigatório.")]
    public TimeOnly Inicioturnoatend { get; set; }

    [Column("fimturnoatend")]
    [Required(ErrorMessage = "O campo Fim do Turno é obrigatório.")]
    public TimeOnly Fimturnoatend { get; set; }

    [Column("senhaatend")]
    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    [StringLength(20, ErrorMessage = "A Senha deve ter no máximo 20 caracteres.")]
    public string Senhaatend { get; set; } = null!;

    public virtual ICollection<Consultum> Consulta { get; set; } = new List<Consultum>();
}
