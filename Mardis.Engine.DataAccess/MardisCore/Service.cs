using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("Service", Schema = "MardisCore")]
    public class Service : IEntity
    {

        public Service()
        {
            ServiceDetails = new List<ServiceDetail>();
            CampaignServicesList = new HashSet<CampaignServices>();
        }

        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public Guid IdTypeService { get; set; }

        public string PollTitle { get; set; }

        public Guid IdAccount { get; set; }

        public Guid IdCustomer { get; set; }

        public DateTime CreationDate { get; set; }

        public string StatusRegister { get; set; }

        public Guid IdChannel { get; set; }

        public string Icon { get; set; }

        public string IconColor { get; set; }

        public string Template { get; set; }

        [ForeignKey("IdTypeService")]
        public virtual TypeService TypeService { get; set; }

        [ForeignKey("IdCustomer")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("IdChannel")]
        public Channel Channel { get; set; }

        public List<ServiceDetail> ServiceDetails { get; set; }

        public ICollection<CampaignServices> CampaignServicesList { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
