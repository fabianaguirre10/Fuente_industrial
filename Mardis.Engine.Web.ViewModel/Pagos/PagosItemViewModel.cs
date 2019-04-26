using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataAccess.MardisPedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.Web.ViewModel.Pagos
{
   public class PagosItemViewModel
    {
        public int Idpago { get; set; }
        public string CodCliente { get; set; }
        public string Nombre_Cliente { get; set; }
        public Nullable<decimal> ValorTotalPago { get; set; }
        public string FormaPago { get; set; }
        public string Fecha { get; set; }
        public string foto { get; set; }
        public string idvendedor { get; set; }
        public virtual Branch Branches { get; set; }
        public List<ChequePagos> ChequePagos { get; set; }

        public List<PagosDetalles> PagosDetalles { get; set; }
    

    }
}
