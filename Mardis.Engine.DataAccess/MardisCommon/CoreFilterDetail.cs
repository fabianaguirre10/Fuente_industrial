using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mardis.Engine.DataAccess.MardisCommon
{
    [Table("CoreFilterDetail", Schema = "MardisCommon")]
    public class CoreFilterDetail
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Table { get; set; }

        public string Property { get; set; }

        public Guid IdCoreFilter { get; set; }

        public bool Visible { get; set; } = true;

        public bool ManyToMany { get; set; } = false;

        [ForeignKey("IdCoreFilter")]
        public CoreFilter CoreFilter { get; set; }
    }
}