
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataAccess.MardisCore;

namespace Mardis.Engine.DataAccess.MardisSecurity
{
    /// <summary>
    /// Clase de Usuario
    /// </summary>
    [Table("User", Schema = "MardisSecurity")]
    public class User : IEntity, ISoftDelete
    {
        /// <summary>
        /// Id de Usuario
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string StatusRegister { get; set; }

        public Guid IdProfile { get; set; }

        public Guid IdPerson { get; set; }

        public Guid? Key { get; set; }

        public DateTime? DateKey { get; set; }

        public Guid IdAccount { get; set; }

        public string InitialPage { get; set; }

        [ForeignKey("IdAccount")]
        public virtual  Account Account { get; set; }

        [ForeignKey("IdPerson")]
        public virtual Person Person { get; set; }

        [ForeignKey("IdProfile")]
        public virtual Profile Profile { get; set; }

        [InverseProperty("Merchant")]
        public List<TaskCampaign> TaskCampaigns { get; set; }=new List<TaskCampaign>();

        [InverseProperty("Validator")]
        public List<TaskCampaign> ValidatedCampaigns { get; set; }
    }
}
