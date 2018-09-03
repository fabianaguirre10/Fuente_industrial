using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("BulkLoad", Schema = "MardisCore")]
    public class BulkLoad : IEntity, ISoftDelete
    {
        [Key]
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string ContainerName { get; set; }

        public Guid IdBulkLoadStatus { get; set; }

        [ForeignKey("IdBulkLoadStatus")]
        public BulkLoadStatus BulkLoadStatus { get; set; }

        public Guid IdBulkLoadCatalog { get; set; }

        [ForeignKey("IdBulkLoadCatalog")]
        public BulkLoadCatalog BulkLoadCatalog { get; set; }

        public string StatusRegister { get; set; } = CStatusRegister.Active;

        public Guid IdAccount { get; set; }

        //[Column(TypeName = "datetime2")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int TotalAdded { get; set; }

        public int TotalUpdated { get; set; }

        public int TotalFailed { get; set; }

        public int CurrentFile { get; set; }

        public int TotalRegister { get; set; }

    }
}