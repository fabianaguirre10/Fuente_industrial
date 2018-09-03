using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mardis.Engine.DataAccess.MardisCore
{
   /// <summary>
    /// Estado del Cliente en el Sistema
    /// </summary>
    [Table("StatusCustomer", Schema = "MardisCore")]
    public class StatusCustomer : IEntity, ISoftDelete
    {
        
        public StatusCustomer()
        {
            Customers = new HashSet<Customer>();
        }

        [Key]
        public System.Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string StatusRegister { get; set; }
        
        public ICollection<Customer> Customers { get; set; }
    }
}
