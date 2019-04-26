using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.DataAccess.MardisPedidos
{
    [Table("Pagos", Schema = "MardisPedidos")]
   public class Pagos
    {
        public Pagos()
        {
            this.ChequePagos = new HashSet<ChequePagos>();
            this.PagosDetalles = new HashSet<PagosDetalles>();
        }
        [Key]
        public int Idpago { get; set; }
        public string CodCliente { get; set; }
        public Nullable<decimal> ValorTotalPago { get; set; }
        public string FormaPago { get; set; }
        public string Fecha { get; set; }
        public string foto { get; set; }
        public byte[] imagen { get; set; }
        public string idvendedor { get; set; }

        public virtual ICollection<ChequePagos> ChequePagos { get; set; }
        public virtual ICollection<PagosDetalles> PagosDetalles { get; set; }
    }
}
