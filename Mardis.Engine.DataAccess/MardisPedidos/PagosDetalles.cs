using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.DataAccess.MardisPedidos
{
    [Table("PagosDetalles", Schema = "MardisPedidos")]
    public class PagosDetalles
    {
     
        [Key]
        public int idpagodetalle { get; set; }
        public int idpago { get; set; }
        public string Numfactura { get; set; }
        public Nullable<decimal> TotalFactura { get; set; }
        public Nullable<decimal> PagoRegistrado { get; set; }

        public virtual Pagos Pagos { get; set; }
    }
}
