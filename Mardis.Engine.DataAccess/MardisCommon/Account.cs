using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.DataAccess.MardisSecurity;

namespace Mardis.Engine.DataAccess.MardisCommon
{
    [Table("Account", Schema = "MardisCommon")]
    public class Account : IEntity, ISoftDelete
    {
        public Account()
        {
            People = new List<Person>();
            Users = new List<User>();
        }

        [Key]
        public System.Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string StatusRegister { get; set; }
        
        public List<Person> People { get; set; }

        public List<User> Users { get; set; }
    }
}
