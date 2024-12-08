using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SMT.Models;

[Table("paciente")]
public partial class Paciente
{
    [Key]
    [Column("cpfpaci")]
    [Required(ErrorMessage = "O campo CPF é obrigatório.")]
    [StringLength(14, ErrorMessage = "O CPF deve ter no máximo 14 caracteres.")]
    public string CpfPaci { get; set; } = null!;

    [Column("nomepaci")]
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(45, ErrorMessage = "O Nome deve ter no máximo 45 caracteres.")]
    public string NomePaci { get; set; } = null!;

    [Column("sobrenomepaci")]
    [Required(ErrorMessage = "O campo Sobrenome é obrigatório.")]
    [StringLength(45, ErrorMessage = "O Sobrenome deve ter no máximo 45 caracteres.")]
    public string SobrenomePaci { get; set; } = null!;

    [Column("nascimentopaci")]
    [Required(ErrorMessage = "O campo Nascimento é obrigatório.")]
    public DateOnly NascimentoPaci { get; set; }

    [Column("genero")]
    [Required(ErrorMessage = "O campo Gênero é obrigatório.")]
    public string GeneroPaci { get; set; } = null!;

    [Column("emailpaci")]
    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [StringLength(45, ErrorMessage = "O Email deve ter no máximo 45 caracteres.")]
    [EmailAddress(ErrorMessage = "O campo Email deve ser um email válido.")]
    public string EmailPaci { get; set; } = null!;

    [Column("telefonepaci")]
    [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
    [StringLength(14, ErrorMessage = "O Telefone deve ter no máximo 14 caracteres.")]
    [Phone(ErrorMessage = "O campo Telefone deve ser um número válido.")]
    public string TelefonePaci { get; set; } = null!;

    [Column("senhapaci")]
    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    [StringLength(20, ErrorMessage = "A Senha deve ter no máximo 20 caracteres.")]
    public string SenhaPaci { get; set; } = null!;
}

public class LoginRequest
{
    public string EmailPaci { get; set; }
    public string Senhapaci { get; set; }
}

public class LoginResponse
{
    public string CpfPaciente { get; set; }
}
