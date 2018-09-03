using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mardis.Engine.DataAccess.MardisSecurity
{

    /// <summary>
    /// Tipo de Usuario en el Sistema
    /// </summary>
    [Table("TypeUser", Schema = "MardisSecurity")]
    public class TypeUser : IEntity, ISoftDelete
    {
        
        public TypeUser()
        {
            AuthorizationComponents = new HashSet<AuthorizationComponent>();
        }

        [Key]
        public System.Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string StatusRegister { get; set; }

      
        public ICollection<AuthorizationComponent> AuthorizationComponents { get; set; }
    }
}
