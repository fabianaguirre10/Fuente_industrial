using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("BulkLoadStatus", Schema = "MardisCore")]
    public class BulkLoadStatus : IEntity, ISoftDelete
    {
        [Key]
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string StatusRegister { get; set; } = CStatusRegister.Active;

        public ICollection<BulkLoad> BulkLoads { get; set; } = new HashSet<BulkLoad>();
    }
}