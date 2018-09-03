using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataAccess.MardisCore
{
    /// <summary>
    /// Tabla de Canales
    /// </summary>
    [Table("Channel", Schema = "MardisCore")]
    public class Channel : IEntity, ISoftDelete
    {
       
        public Channel()
        {
            BranchCustomers = new List<BranchCustomer>();
        }

        [Key]
        public System.Guid Id { get; set; }

        public System.Guid IdAccount { get; set; }

        public System.Guid IdCustomer { get; set; }

        public string Name { get; set; }

        public string StatusRegister { get; set; } = CStatusRegister.Active;

        [ForeignKey("IdAccount")]
        public Account Account { get; set; }

        public List<BranchCustomer> BranchCustomers { get; set; }

        [ForeignKey("IdCustomer")]
        public Customer Customer { get; set; }
    }
}
