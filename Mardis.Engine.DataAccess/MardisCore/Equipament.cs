using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataAccess.MardisSecurity;
using Mardis.Engine.Framework.Resources;


namespace Mardis.Engine.DataAccess.MardisCore
{  /// <summary>
   /// Tabla de equipos de frio Manejo de custodios
   /// </summary>
    [Table("Equipament", Schema = "MardisCore")]
    public class Equipament : IEntityId
    {
        [Key]
        public int Id { get; set; }

        public int? IdType { get; set; } = 0;

        public string Sticker { get; set; }
        public string EQplate { get; set; }
        public string Series { get; set; } = "";
        public string brand { get; set; }

        public string Model { get; set; }
        public int? NDoor { get; set; } = 0;

        public Guid? Idbranch { get; set; } = null;
        public int Status { get; set; }

        public string description { get; set; }

        public string maker { get; set; }

        public string guide { get; set; }
        public DateTime CreationDate { get; set; }
        public string usr_web { get; set; }
        public string aggregateuri { get; set; }
        public Guid? IdAccount { get; set; } = Guid.Empty;

        [ForeignKey("IdAccount")]
        public virtual Account Accounts { get; set; }
        [ForeignKey("Idbranch")]
        public virtual Branch Branches { get; set; }

        [ForeignKey("Status")]
        public virtual Equipament_status Equipament_statuss { get; set; }

        [ForeignKey("IdType")]
        public virtual Equipament_type Equipament_types { get; set; }
        public ICollection<EquipamentImages> EquipamentImg { get; set; } = new HashSet<EquipamentImages>();
        public ICollection<Activity> Activities { get; set; } = new HashSet<Activity>();

    }


    public interface IEntityId
    {
    }
}

