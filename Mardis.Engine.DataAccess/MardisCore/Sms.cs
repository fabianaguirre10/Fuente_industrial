using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.DataAccess.MardisCommon;

namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("Sms", Schema = "MardisCore")]
    public class Sms : IEntity
    {
        public Guid Id { get; set; }
        public Guid idAccount { get; set; }
        public Guid idCampaign { get; set; }
        public string Mensaje { get; set; }
        public int enviados { get; set; }
        public DateTime fecha { get; set; }
        public string motivo { get; set; }
        public string estado { get; set; }
        public DateTime hora_envio { get; set; }
        [ForeignKey("idAccount")]
        public Account Account { get; set; }
        [ForeignKey("idCampaign")]
        public Campaign Campaign { get; set; }



    }
}
