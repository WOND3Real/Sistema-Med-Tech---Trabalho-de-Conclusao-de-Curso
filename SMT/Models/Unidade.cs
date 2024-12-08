using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMT.Models
{
    [Table("unidade")]
    public class Unidade
    {
        [Key]
        [Column("idunidade")]
        public int IdUnidade { get; set; }

        [Required]
        [Column("nomeunidade")]
        [MaxLength(20)]
        public string NomeUnidade { get; set; } = null!;

        [Required]
        [Column("cepuni")]
        [MaxLength(10)]
        public string CepUni { get; set; } = null!;

        [Required]
        [Column("logradourouni")]
        [MaxLength(45)]
        public string LogradouroUni { get; set; } = null!;

        [Required]
        [Column("numerouni")]
        [MaxLength(6)]
        public string NumeroUni { get; set; } = null!;

        [Required]
        [Column("bairrouni")]
        [MaxLength(20)]
        public string BairroUni { get; set; } = null!;

        [Required]
        [Column("cidadeuni")]
        [MaxLength(20)]
        public string CidadeUni { get; set; } = null!;

        [Required]
        [Column("estadouni")]
        [MaxLength(20)]
        public string EstadoUni { get; set; } = null!;

        [Required]
        [Column("paisuni")]
        [MaxLength(20)]
        public string PaisUni { get; set; } = null!;
    }
}
