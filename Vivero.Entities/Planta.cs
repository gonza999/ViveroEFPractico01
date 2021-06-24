using System.ComponentModel.DataAnnotations;

namespace Vivero.Entities
{
    public class Planta
    {
        public int PlantaId { get; set; }

        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }

        public int TipoDePlantaId { get; set; }

        public decimal Precio { get; set; }

        public virtual TipoDePlanta TiposDePlanta { get; set; }
    }
}
