using Mardis.Engine.DataAccess.MardisCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.DataAccess.MardisCommon
{
    [Table("Pollster", Schema = "MardisCommon")]
    public class Pollster : IEntityId
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string IMEI { get; set; }
        public string Phone { get; set; }
        public string Qsupport { get; set; }
        public DateTime Fecha_Inicio { get; set; }

        public DateTime Fecha_Fin { get; set; }
        public string Status {  get; set; }

        public string Oficina { get; set; }
        public string UserCel { get; set; }
        public string PassCel { get; set; }
    }
}
