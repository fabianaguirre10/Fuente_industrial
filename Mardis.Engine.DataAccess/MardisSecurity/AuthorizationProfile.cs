using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mardis.Engine.DataAccess.MardisSecurity
{
    [Table("AuthorizationProfile", Schema = "MardisSecurity")]
    public class AuthorizationProfile : IEntity
    {

        [Key]
        public System.Guid Id { get; set; }

        public System.Guid IdProfile { get; set; }

        public System.Guid IdMenu { get; set; }

        [ForeignKey("IdProfile")]
        public virtual Profile Profile { get; set; }

        [ForeignKey("IdMenu")]
        public virtual Menu Menu { get; set; }

    }
}
