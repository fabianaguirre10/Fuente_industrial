using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mardis.Engine.DataAccess.MardisCore
{
    /// <summary>
    /// Tabla de Clientes por Locales
    /// </summary>
    [Table("BranchCustomer", Schema = "MardisCore")]
    public class BranchCustomer : IEntity
    {
        [Key]
        public System.Guid Id { get; set; }
        public System.Guid IdBranch { get; set; }
        public System.Guid IdCustomer { get; set; }
        public System.Guid IdTypeBusiness { get; set; }
        public System.Guid IdChannel { get; set; }

        [ForeignKey("IdBranch")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("IdChannel")]
        public virtual Channel Channel { get; set; }
        [ForeignKey("IdCustomer")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("IdTypeBusiness")]
        public virtual TypeBusiness TypeBusiness { get; set; }
    }
}
