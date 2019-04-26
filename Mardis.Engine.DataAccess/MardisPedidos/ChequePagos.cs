using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.DataAccess.MardisPedidos
{
    [Table("ChequePagos", Schema = "MardisPedidos")]
    public class ChequePagos
    {
        [Key]
        public int idcheque { get; set; }
        public int idpago { get; set; }
        public string numerocheque { get; set; }
        public string banco { get; set; }
        public Nullable<decimal> valor { get; set; }
        public string fecha { get; set; }

        public virtual Pagos Pagos { get; set; }
    }
}
