using Mardis.Engine.Framework.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.DataAccess.MardisCore
{

    [Table("Activity", Schema = "MardisCore")]
    public class Activity : IEntityId
    {

        public Activity()
        {
            //  Campaign = new Campaign();
        }

        [Key]
        public int Id { get; set; }

        public Guid IdAccount { get; set; }

        public Guid IdCampaign { get; set; }

        [ForeignKey("IdCampaign")]
        public Campaign Campaign { get; set; }

        public string Code { get; set; }

        public string ExternalCode { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime DateModification { get; set; } = DateTime.Now;

        public string Description { get; set; }

        public string StatusRegister { get; set; } = CStatusRegister.Active;


        public DateTime DateCreation { get; set; } = DateTime.Now;

        public string AggregateUri { get; set; }

        public string guide { get; set; }

        public string car_license { get; set; }
        public string typeActivity { get; set; }
        public int idequipment { get; set; }
        [ForeignKey("idequipment")]
       public Equipament Equipament { get; set; }
    }
}
