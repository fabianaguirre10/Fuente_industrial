using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("Equipament_type", Schema = "MardisCore")]
   public class Equipament_type 
    {
        public Equipament_type()
        {
            Equipaments = new HashSet<Equipament>();
        }

        [Key]
        public int Id { get; set; }
        public string Code { get; set; }

        public string Description { get; set; }
        public string Status { get; set; }


        public ICollection<Equipament> Equipaments { get; set; }
    }
}
