using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMT.Models;
public enum Statusconsulta{
    Pendente,Andamento,Concluida,Cancelada
}

[Table("consulta")]
public partial class Consultum
{
    public int Idconsulta { get; set; }

    public string PacienteCpfpaci { get; set; } = null!;

    public string AtendenteCtpsatend { get; set; } = null!;

    public string MedicoCrmmed { get; set; } = null!;

    public int UnidadeIdunidade { get; set; }

    public int EspecialidadeIdespecialidade { get; set; }

    public DateOnly Dataconsul { get; set; }

    public TimeOnly Horaconsul { get; set; }

    public Statusconsulta statusconsul { get; set; }

    public virtual Atendente AtendenteCtpsatendNavigation { get; set; } = null!;

    public virtual Especialidade EspecialidadeIdespecialidadeNavigation { get; set; } = null!;

    public virtual Medico MedicoCrmmedNavigation { get; set; } = null!;

    public virtual Paciente PacienteCpfpaciNavigation { get; set; } = null!;

    public virtual Unidade UnidadeIdunidadeNavigation { get; set; } = null!;
}
