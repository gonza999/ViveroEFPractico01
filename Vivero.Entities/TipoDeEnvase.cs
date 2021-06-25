using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivero.Entities
{
    [Table("TiposDeEnvases")]
    public class TipoDeEnvase
    {
        public int TipoDeEnvaseId { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        public virtual ICollection<Planta> Plantas { get; set; }
    }
}
