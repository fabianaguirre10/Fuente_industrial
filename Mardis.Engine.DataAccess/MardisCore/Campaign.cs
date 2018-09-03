using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataAccess.MardisSecurity;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataAccess.MardisCore
{
    /// <summary>
    /// Tabla de Campañas
    /// </summary>
    [Table("Campaign", Schema = "MardisCore")]
    public class Campaign : IEntity, ISoftDelete
    {
        public Campaign()
        {
            CampaignServices = new List<CampaignServices>();
            Tasks = new HashSet<TaskCampaign>();
            Activities = new HashSet<Activity>();
        }

        [Key]
        public Guid Id { get; set; }

        public Guid IdAccount { get; set; } = Guid.Empty;

        public Guid IdCustomer { get; set; }

        public Guid IdStatusCampaign { get; set; }

        public Guid IdChannel { get; set; }

        public Guid IdSupervisor { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; } = DateTime.Now;

        public DateTime RangeDate { get; set; } = DateTime.Now;

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string Comment { get; set; }

        public List<CampaignServices> CampaignServices { get; set; }

        public string StatusRegister { get; set; } = CStatusRegister.Active;

        [ForeignKey("IdAccount")]
        public virtual Account Account { get; set; }

        [ForeignKey("IdCustomer")]
        public Customer Customer { get; set; }

        [ForeignKey("IdStatusCampaign")]
        public StatusCampaign StatusCampaign { get; set; }

        [ForeignKey("IdChannel")]
        public Channel Channel { get; set; }

        [ForeignKey("IdSupervisor")]
        public User Supervisor { get; set; }

        public ICollection<TaskCampaign> Tasks { get; set; }
        public ICollection<Activity> Activities { get; set; }


    }
}

