using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataAccess.MardisSecurity;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataAccess.MardisCore
{
    /// <summary>
    /// Tabla de Campañas
    /// </summary>
    [Table("UserCanpaign", Schema = "MardisCore")]
    public class UserCanpaign
    {
        public UserCanpaign()
        {

        }

        [Key]
        public Guid id { get; set; }

        public Guid idCanpaign { get; set; } = Guid.Empty;

        public Guid idUser { get; set; }

        public string status { get; set; }


        [ForeignKey("idCanpaign")]
        public virtual Campaign Campaign { get; set; }

        [ForeignKey("idUser")]
        public User Supervisor { get; set; }
    }
}

