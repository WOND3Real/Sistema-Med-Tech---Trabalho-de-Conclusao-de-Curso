using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMT.Models
{
    [Table("atendente")]
    public class Atendente
    {
        [Key]
        [StringLength(15)]
        [Column("ctpsatend")]
        public string CTPAtend { get; set; }

        [Required]
        [StringLength(45)]
        [Column("nomeatend")]
        public string NomeAtend { get; set; }

        [Required]
        [StringLength(45)]
        [Column("sobrenomeatend")]
        public string SobrenomeAtend { get; set; }

        [Required]
        [Column("inicioturnoatend")]
        public TimeSpan InicioTurnoAtend { get; set; }

        [Required]
        [Column("fimturnoatend")]
        public TimeSpan FimTurnoAtend { get; set; }

        [Required]
        [StringLength(20)]
        [Column("senhaatend")]
        public string SenhaAtend { get; set; }
    }
}
