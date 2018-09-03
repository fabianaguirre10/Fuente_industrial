using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.DataAccess.MardisCommon;

namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("EquipamentImages", Schema = "MardisCore")]
    public class EquipamentImages : IEntityId
    {
        public int Id { get; set; }
        public int IdEquipament { get; set; }
        public string NameContainer { get; set; }
        public string NameFile { get; set; }
        public string UrlImage { get; set; }
        public Guid IdAccount { get; set; }
        public string ContentType { get; set; }
        public int ORDER { get; set; }
        [ForeignKey("IdAccount")]
        public Account Account { get; set; }
        [ForeignKey("IdEquipament")]
        public Equipament Equipament { get; set; }



    }
}
