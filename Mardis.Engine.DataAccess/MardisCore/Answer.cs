using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataAccess.MardisSecurity;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("Answer", Schema = "MardisCore")]
    public class Answer : IEntity, ISoftDelete
    {

        public Answer()
        {
            AnswerDetails = new HashSet<AnswerDetail>();
        }

        [Key]
        public Guid Id { get; set; }

        public Guid IdAccount { get; set; } = Guid.Empty;

        [ForeignKey("IdAccount")]
        public Account Account { get; set; }

        public Guid IdTask { get; set; }

        [ForeignKey("IdTask")]
        public TaskCampaign TaskCampaign { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.Now;

        public Guid IdServiceDetail { get; set; }

        [ForeignKey("IdServiceDetail")]
        public ServiceDetail ServiceDetail { get; set; }

        public Guid IdMerchant { get; set; }

        [ForeignKey("IdMerchant")]
        public User Merchant { get; set; }

        public Guid IdQuestion { get; set; }

        [ForeignKey("IdQuestion")]
        public Question Question { get; set; }
        public int? sequenceSection { get; set; }

        public string StatusRegister { get; set; } = CStatusRegister.Active;

        public ICollection<AnswerDetail> AnswerDetails { get; set; }
    }
}
