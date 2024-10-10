using System;
using System.Collections.Generic;

namespace SMT.Models;

public partial class Medico
{
    public string Crmmed { get; set; } = null!;

    public string Nomemed { get; set; } = null!;

    public string Sobrenomemed { get; set; } = null!;

    public string Telefonemed { get; set; } = null!;

    public string Emailmed { get; set; } = null!;

    public string Senhamed { get; set; } = null!;

    public virtual ICollection<Consultum> Consulta { get; set; } = new List<Consultum>();

    public virtual ICollection<Especialidade> EspecialidadeIdespecialidades { get; set; } = new List<Especialidade>();

    public virtual ICollection<Unidade> UnidadeIdunidades { get; set; } = new List<Unidade>();

    public virtual ICollection<Unidade> UnidadeIdunidadesNavigation { get; set; } = new List<Unidade>();
}
