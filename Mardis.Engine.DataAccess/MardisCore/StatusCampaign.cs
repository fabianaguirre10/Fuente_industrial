using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mardis.Engine.DataAccess.MardisCore
{

    [Table("StatusCampaign", Schema = "MardisCore")]
    public class StatusCampaign : IEntity, ISoftDelete
    {
        public StatusCampaign()
        {
            Campaigns = new HashSet<Campaign>();
        }

        [Key]
        public System.Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string StatusRegister { get; set; }

        public ICollection<Campaign> Campaigns { get; set; }
    }
}