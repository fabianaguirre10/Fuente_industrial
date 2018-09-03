using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataAccess.MardisSecurity;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataAccess.MardisCommon
{
    /// <summary>
    /// Tabla de Personas en la cuenta por oficina
    /// </summary>
    [Table("Person", Schema = "MardisCommon")]
    public class Person : IEntity, ISoftDelete
    {
       
        public Person()
        {
            Users = new HashSet<User>();
            //this.Branches = new HashSet<Branch>();
        }

        [Key]
        public Guid Id { get; set; } = Guid.Empty;
        public Guid IdAccount { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string TypeDocument { get; set; } = "CI";
        public string Document { get; set; }
        public string StatusRegister { get; set; } = CStatusRegister.Active;
        public string Phone { get; set; }
        public string Mobile { get; set; }

        [ForeignKey("IdAccount")]
        public virtual  Account Account { get; set; }
        //public virtual ICollection<Branch> Branches { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
