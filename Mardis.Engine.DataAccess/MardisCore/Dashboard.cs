using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataAccess.MardisSecurity;
using Mardis.Engine.Framework.Resources;
namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("dashboard", Schema = "MardisCore")]
    public class Dashboard
    {
        [Key]
        public int  id { get; set; }
        public Guid idcampaign { get; set; } = Guid.Empty;
        public string Name { get; set; }
        public string url { get; set; }
        public string statusRegister { get; set; }
        [ForeignKey("idcampaign")]
        public virtual Campaign Campaign { get; set; }
    }
}
