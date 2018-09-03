using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.DataAccess.MardisSecurity;

namespace Mardis.Engine.DataAccess.MardisCommon
{
    [Table("CodigoReservados", Schema = "MardisCommon")]
    public class CodigoReservados 
    {

        [Key]
        public int Id { get; set; }
        public System.Guid idAccount { get; set; }

        public int Code { get; set; }

        public string estado { get; set; }

        public string uri { get; set; }
        public string imei_id { get; set; }
        public string codeunico { get; set; }

    }

}
