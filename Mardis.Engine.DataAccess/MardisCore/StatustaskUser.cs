using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mardis.Engine.DataAccess.MardisSecurity;
namespace Mardis.Engine.DataAccess.MardisCore
{

    [Table("StatustaskUser", Schema = "MardisCore")]
    public  class StatustaskUser : IEntityId
    {
        public StatustaskUser()
        {

        }

        [Key]
        public int id { get; set; }

        public Guid Iduser { get; set; } = Guid.Empty;

        public Guid Idstatustask { get; set; }

        [ForeignKey("Iduser")]
        public virtual  User users { get; set; }

        [ForeignKey("Idstatustask")]
        public StatusTask StatusTasks { get; set; }
    }
}