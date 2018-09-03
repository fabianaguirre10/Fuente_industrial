using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mardis.Engine.DataAccess.MardisCommon;
namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("statusTaskAccount", Schema = "MardisCore")]
   public class StatusTaskAccount:IEntityId
    {
        public StatusTaskAccount()
        {

        }

        [Key]
        public int id { get; set; }

        public Guid Idaccount { get; set; } = Guid.Empty;

        public Guid Idstatustask { get; set; }

        public int ORDER { get; set; }


        [ForeignKey("Idaccount")]
        public virtual Account Cuenta { get; set; }

    [ForeignKey("Idstatustask")]
        public StatusTask StatusTasks { get; set; }
    }
}
