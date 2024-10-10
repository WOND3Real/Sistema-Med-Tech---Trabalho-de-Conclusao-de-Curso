using System;
using System.Collections.Generic;

namespace SMT.Models;

public partial class Unidade
{
    public int Idunidade { get; set; }

    public string Nomeunidade { get; set; } = null!;

    public string Cepuni { get; set; } = null!;

    public string Logradourouni { get; set; } = null!;

    public string Numerouni { get; set; } = null!;

    public string Bairrouni { get; set; } = null!;

    public string Cidadeuni { get; set; } = null!;

    public string Estadouni { get; set; } = null!;

    public string Paisuni { get; set; } = null!;

    public virtual ICollection<Administrador> Administradors { get; set; } = new List<Administrador>();

    public virtual ICollection<Consultum> Consulta { get; set; } = new List<Consultum>();

    public virtual ICollection<Especialidade> EspecialidadeIdespecialidades { get; set; } = new List<Especialidade>();

    public virtual ICollection<Medico> MedicoCrmmeds { get; set; } = new List<Medico>();

    public virtual ICollection<Medico> MedicoCrmmedsNavigation { get; set; } = new List<Medico>();
}
