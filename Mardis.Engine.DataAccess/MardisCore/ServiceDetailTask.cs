using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("ServiceDetailTask", Schema = "MardisCore")]
    public class ServiceDetailTask : IEntity, ISoftDelete
    {
        [Key]
        public Guid Id { get; set; }

        public string StatusRegister { get; set; } = CStatusRegister.Active;

        public Guid IdServiceDetail { get; set; }

        [ForeignKey("IdServiceDetail")]
        public ServiceDetail ServiceDetail { get; set; }

        public Guid IdTask { get; set; }

        [ForeignKey("IdTask")]
        public TaskCampaign TaskCampaign { get; set; }

    }
}
