using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mardis.Engine.DataAccess.MardisSecurity
{
    [Table("AuthorizationComponent", Schema = "MardisSecurity")]
    public class AuthorizationComponent : IEntity
    {

        [Key]
        public System.Guid Id { get; set; }

        public System.Guid IdTypeUser { get; set; }

        public System.Guid IdComponent { get; set; }

        [ForeignKey("IdTypeUser")]
        public virtual TypeUser TypeUser { get; set; }

        [ForeignKey("IdComponent")]
        public virtual Component Component { get; set; }

    }
}
