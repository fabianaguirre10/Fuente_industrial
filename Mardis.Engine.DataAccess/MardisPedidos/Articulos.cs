using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.DataAccess.MardisPedidos
{
    [Table("ARTICULOS", Schema = "MardisPedidos")]
    public class Articulos 
    {
        public Articulos()
        {
         
        }
        [Key]
        public int _id { get; set; }
        public string idArticulo { get; set; }
        public string descripcion { get; set; }
        public string idRubro { get; set; }
        public decimal? iva { get; set; }
        public decimal? impuestosInternos { get; set; }
        public Boolean exento { get; set; }
        public decimal? precio1 { get; set; }
        public decimal? precio2 { get; set; }
        public decimal? precio3 { get; set; }
        public decimal? precio4 { get; set; }
        public decimal? precio5 { get; set; }
        public decimal? precio6 { get; set; }
        public decimal? precio7 { get; set; }
        public decimal? precio8 { get; set; }
        public decimal? precio9 { get; set; }
        public decimal? precio10 { get; set; }
        public string categoria { get; set; }
        public string barcode { get; set; }

    }
}
