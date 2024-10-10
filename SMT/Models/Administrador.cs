using System;
using System.Collections.Generic;

namespace SMT.Models;

public partial class Administrador
{
    public int Idadministrador { get; set; }

    public int UnidadeIdunidade { get; set; }

    public string? Nomeadm { get; set; }

    public string? Sobrenomeadm { get; set; }

    public string? Senhaadm { get; set; }

    public virtual Unidade UnidadeIdunidadeNavigation { get; set; } = null!;
}
