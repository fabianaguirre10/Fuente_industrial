using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mardis.Engine.DataAccess.MardisCommon
{
    [Table("CoreFilter", Schema = "MardisCommon")]
    public class CoreFilter
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<CoreFilterDetail> CoreFilterDetails { get; set; } = new List<CoreFilterDetail>();
    }
}
