using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.DataAccess.MardisPedidos
{
    [Table("PEDIDOS", Schema = "MardisPedidos")]
    public class Pedidos
    {
        public Pedidos()
        {
            PedidosItems = new HashSet<PedidosItems>();
        }
        [Key]
        public int _id { get; set; }
        public string codCliente { get; set; }
        public string fecha { get; set; }
        public string idVendedor { get; set; }
        public Decimal totalNeto { get; set; }
        public Decimal totalFinal { get; set; }
        public Decimal? transferido { get; set; }
        public Decimal? gpsX { get; set; }
        public Decimal? gpsY { get; set; }
        public Decimal? facturar { get; set; }
        public HashSet<PedidosItems> PedidosItems { get; set; }
    }
}
