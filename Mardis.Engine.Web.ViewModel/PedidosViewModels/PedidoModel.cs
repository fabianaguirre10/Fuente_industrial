using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataAccess.MardisPedidos;
using Mardis.Engine.Web.ViewModel.TaskViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.Web.ViewModel.PedidosViewModels
{
    public class PedidoModel
    {
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
        public List<PedidoItemsModels> PedidosItems { get; set; }
        public MyTaskViewModel tarea { get; set; }
        //public Articulos articulos { get; set; }
        public Branch branch { get; set; }
    }
}
