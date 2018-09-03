using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("BranchImages", Schema = "MardisCore")]
    public class BranchImages : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public Guid IdBranch { get; set; }

        [ForeignKey("IdBranch")]
        public Branch Branch { get; set; }

        public string NameContainer { get; set; }

        public string NameFile { get; set; }

        public string UrlImage { get; set; }

        public Guid IdCampaign { get; set; }

        [ForeignKey("IdCampaign")]
        public Campaign Campaign { get; set; }

        public string ContentType { get; set; }
       public int Order { get; set; }
    }
}
