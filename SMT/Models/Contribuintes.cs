using System.ComponentModel.DataAnnotations.Schema;
namespace SMT.Models;

public class Contribuinte
{
    [Column("nome")]
    public string Nome { get; set; } = null!;

    [Column("ocupacao")]
    public string Ocupacao { get; set; } = null!;

    [Column("identificador")]
    public string Identificador { get; set; } = null!;

    [Column("Consultorio ou Guiche")]
    public double ConsultorioOuGuiche { get; set; }

    [Column("especialidade")]
    public string Especialidade { get; set; } = null!;
}
