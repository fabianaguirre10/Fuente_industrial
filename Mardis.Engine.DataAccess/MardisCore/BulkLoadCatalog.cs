using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("BulkLoadCatalog", Schema = "MardisCore")]
    public class BulkLoadCatalog : IEntity, ISoftDelete
    {
        [Key]
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int ColumnNumber { get; set; }

        public string StatusRegister { get; set; } = CStatusRegister.Active;

        public string Separator { get; set; }

        public ICollection<BulkLoad> BulkLoads { get; set; } = new HashSet<BulkLoad>();
    }
}
