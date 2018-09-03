using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.DataAccess.MardisCommon;

namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("Equipament_time", Schema = "MardisCore")]
    public class Equipament_time : IEntityId
    {
        [Key]
        public int Id { get; set; }
        public int Idequipament { get; set; }
        public int idstatus { get; set; }
        public string aggregateuri { get; set; }
        public string type { get; set; }
        public DateTime creationDate { get; set; }
        [ForeignKey("Idequipament")]
        public Equipament Equipament { get; set; }
        [ForeignKey("idstatus")]
        public Equipament_status Equipament_status { get; set; }



    }
}
