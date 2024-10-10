using System;
using System.Collections.Generic;

namespace SMT.Models;

public partial class Atendente
{
    public string Ctpsatend { get; set; } = null!;

    public string Nomeatend { get; set; } = null!;

    public string Sobrenomeatend { get; set; } = null!;

    public TimeOnly Inicioturnoatend { get; set; }

    public TimeOnly Fimturnoatend { get; set; }

    public string Senhaatend { get; set; } = null!;

    public virtual ICollection<Consultum> Consulta { get; set; } = new List<Consultum>();
}
