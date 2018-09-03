using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mardis.Engine.DataAccess.MardisSecurity
{
    /// <summary>
    /// Tabla de Componentes del Sistema
    /// </summary>
    [Table("Component", Schema = "MardisSecurity")]
    public class Component : IEntity, ISoftDelete
    {
        
        public Component()
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
