using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.DataAccess.MardisSecurity;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("Task", Schema = "MardisCore")]
    public class TaskCampaign : IEntity, ISoftDelete
    {

        public TaskCampaign()
        {
          //  Campaign = new Campaign();
            Answers = new HashSet<Answer>();
        }

        [Key]
        public Guid Id { get; set; }

        public Guid IdAccount { get; set; }

        public Guid IdCampaign { get; set; }

        [ForeignKey("IdCampaign")]
        public Campaign Campaign { get; set; }

        public string Code { get; set; }

        public string ExternalCode { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime DateModification { get; set; } = DateTime.Now;

        public string Description { get; set; }

        public Guid IdBranch { get; set; }

        [ForeignKey("IdBranch")]
        public Branch Branch { get; set; } = new Branch();

        public Guid IdMerchant { get; set; }

        [ForeignKey("IdMerchant")]
        public User Merchant { get; set; }

        public string Route { get; set; }

        public string StatusRegister { get; set; } = CStatusRegister.Active;

        public Guid IdStatusTask { get; set; }

        [ForeignKey("IdStatusTask")]
        public StatusTask StatusTask { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.Now;

        public Guid? IdTaskNoImplementedReason { get; set; }

        [ForeignKey("IdTaskNoImplementedReason")]
        public TaskNoImplementedReason TaskNoImplementedReason { get; set; }

        public string CommentTaskNoImplemented { get; set; }

        public Guid? UserValidator { get; set; }

        [ForeignKey("UserValidator")]
        public User Validator { get; set; }

        public DateTime DateValidation { get; set; } = DateTime.Now;

        public string AggregateUri { get; set; }
        public string CodeGemini { get; set; }
        public ICollection<Answer> Answers { get; set; }

    }
}
