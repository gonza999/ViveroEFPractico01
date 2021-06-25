using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vivero.Entities
{
    [Table("Plantas")]
    public class Planta
    {
        public int PlantaId { get; set; }

        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }

        public int TipoDePlantaId { get; set; }

        public decimal Precio { get; set; }

        public int TipoDeEnvaseId { get; set; }

        public virtual TipoDeEnvase TipoDeEnvase { get; set; }

        public virtual TipoDePlanta TiposDePlanta { get; set; }
    }
}
