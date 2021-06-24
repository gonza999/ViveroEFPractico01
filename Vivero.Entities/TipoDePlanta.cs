using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vivero.Entities
{
    public class TipoDePlanta
    {
        
        public TipoDePlanta()
        {
            Plantas = new HashSet<Planta>();
        }

        [Key]
        public int TipoDePlantaId { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        public virtual ICollection<Planta> Plantas { get; set; }
    }
}
