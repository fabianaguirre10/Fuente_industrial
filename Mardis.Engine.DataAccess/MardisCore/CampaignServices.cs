using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.DataAccess.MardisCommon;

namespace Mardis.Engine.DataAccess.MardisCore
{
    /// <summary>
    /// Tabla de servicios por campaña
    /// </summary>
    [Table("CampaignServices", Schema = "MardisCore")]
    public class CampaignServices : IEntity, ISoftDelete
    {
        [Key]
        public Guid Id { get; set; }
        public Guid IdAccount { get; set; } = Guid.Empty;
        public Guid IdService { get; set; }
        public Guid IdCampaign { get; set; }
        public string StatusRegister { get; set; }


        [ForeignKey("IdCampaign")]
        public Campaign Campaign { get; set; }
        [ForeignKey("IdAccount")]
        public virtual Account Account { get; set; }
        [ForeignKey("IdService")]
        public Service Service { get; set; }
    }
}