using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.DataAccess.MardisPedidos
{
    [Table("pedidosItems", Schema = "MardisPedidos")]
    public class PedidosItems
    {
     
        [Key]
        public int _id { get; set; }
        public int idPedido { get; set; }
        public String idArticulo { get; set; }
        public Decimal? cantidad { get; set; }
        public Decimal? importeUnitario { get; set; }
        public Decimal? porcDescuento { get; set; }
        public Decimal? total { get; set; }
        public int? transferido { get; set; }
        public Decimal? ppago { get; set; }
        public Decimal? nespecial { get; set; }
        public String formapago { get; set; }
        public String unidad { get; set; }

        [ForeignKey("idPedido")]
        public Pedidos Pedidos { get; set; }

        public String numero_factura { get; set; }
    }
}
