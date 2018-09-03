using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("TypeService", Schema = "MardisCore")]
    public class TypeService: IEntity, ISoftDelete
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeService()
        {
            Services = new List<Service>();
        }
    
        public System.Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string StatusRegister { get; set; }
    
       
        public List<Service> Services { get; set; }
    }
}
