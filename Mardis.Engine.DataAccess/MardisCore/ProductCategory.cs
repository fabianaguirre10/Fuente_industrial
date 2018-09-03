using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("ProductCategory", Schema = "MardisCore")]
    public class ProductCategory : IEntity, ISoftDelete
    {

        public ProductCategory()
        {
            Products = new List<Product>();
        }

        [Key]
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string StatusRegister { get; set; } = CStatusRegister.Active;

        public Guid IdCustomer { get; set; }

        [ForeignKey("IdCustomer")]
        public Customer Customer { get; set; }

        public List<Product> Products { get; set; }

        public List<Question> Questions { get; set; }
    }
}
