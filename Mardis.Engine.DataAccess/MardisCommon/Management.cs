using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mardis.Engine.DataAccess.MardisCommon
{
    [Table("Management", Schema = "MardisCommon")]
    public class Management
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int IdRegion { get; set; }

        [ForeignKey("IdRegion")]
        public Region Region { get; set; }

        public List<District> Districts { get; set; } = new List<District>();
    }
}
