using System;
using System.Collections.Generic;
namespace SMT.Models;

public enum Genero
{
    Masculino, Feminino, Outro
}

public partial class Paciente
{
    public string Cpfpaci { get; set; } = null!;

    public string Nomepaci { get; set; } = null!;

    public string Sobrenomepaci { get; set; } = null!;

    public DateOnly Nascimentopaci { get; set; }

    public Genero genero { get; set; }

    public string Emailpaci { get; set; } = null!;

    public string Telefonepaci { get; set; } = null!;

    public string Senhapaci { get; set; } = null!;

    public virtual ICollection<Consultum> Consulta { get; set; } = new List<Consultum>();
}
