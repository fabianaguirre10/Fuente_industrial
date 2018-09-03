using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mardis.Engine.DataAccess.MardisCommon
{
    [Table("Region", Schema = "MardisCommon")]
    public class Region
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Management> Managements { get; set; } = new List<Management>();
    }
}
