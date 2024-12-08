using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMT.Models
{
    [Table("consultapaciente")]
    public partial class ConsultaPaciente
    {
        [Key]
        [Column("idconsulta")]
        public int IdConsulta { get; set; }

        [Column("dataconsulta")]
        public DateOnly DataConsul { get; set; }

        [Column("horaconsulta")]
        public TimeOnly HoraConsul { get; set; }

        [Column("nomepaciente")]
        public string NomePaciente { get; set; } = null!;

        [Column("datanascimento")]
        public DateOnly DataNascimento { get; set; }

        [Column("cpfpaciente")]
        public string CpfPaciente { get; set; } = null!;

        [Column("sexopaciente")]
        public string SexoPaciente { get; set; } = null!;
    }
}
