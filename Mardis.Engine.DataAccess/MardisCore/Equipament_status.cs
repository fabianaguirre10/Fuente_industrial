using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mardis.Engine.DataAccess.MardisCore
{

    [Table("Equipament_status", Schema = "MardisCore")]
    public class Equipament_status 
    {
        public Equipament_status()
        {
            Equipaments = new HashSet<Equipament>();
            Equipaments_time = new HashSet<Equipament_time>();
        }

        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
      

        public ICollection<Equipament> Equipaments { get; set; }
        public ICollection<Equipament_time> Equipaments_time { get; set; }
    }
}
