using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.DataAccess.MardisCommon
{
    [Table("Map", Schema = "MardisCommon")]
    public class Map
    {
        [Key]
        public Guid Id { get; set; } = Guid.Empty;

        public Guid idAccount { get; set; } = Guid.Empty;

        public string scr { get; set; }

        public string status { get; set; }
        [ForeignKey("idAccount")]
        public Account Account { get; set; }
    }
}
