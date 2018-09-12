using Mardis.Engine.DataAccess.MardisPedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.Web.ViewModel.PedidosViewModels
{
   public  class PedidoItemsModels
    {
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
        public Articulos articulos { get; set; }
        public String numero_factura { get; set; }
    }
}
