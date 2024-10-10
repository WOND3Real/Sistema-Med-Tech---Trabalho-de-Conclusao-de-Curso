using System;
using System.Collections.Generic;

namespace SMT.Models;

public partial class Especialidade
{
    public int Idespecialidade { get; set; }

    public int Nomeespec { get; set; }

    public virtual ICollection<Consultum> Consulta { get; set; } = new List<Consultum>();

    public virtual ICollection<Medico> MedicoCrmmeds { get; set; } = new List<Medico>();

    public virtual ICollection<Unidade> UnidadeIdunidades { get; set; } = new List<Unidade>();
}
