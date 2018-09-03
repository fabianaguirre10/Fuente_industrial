using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataAccess.MardisCore
{
    [Table("Product", Schema = "MardisCore")]
    public class Product : IEntity, ISoftDelete
    {
        [Key]
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public Guid IdProductCategory { get; set; }

        [ForeignKey("IdProductCategory")]
        public ProductCategory ProductCategory { get; set; }

        public string StatusRegister { get; set; } = CStatusRegister.Active;

        public Guid IdAccount { get; set; }

        [ForeignKey("IdAccount")]
        public Account Account { get; set; }

        public Guid IdCustomer { get; set; }

        [ForeignKey("IdCustomer")]
        public Customer Customer { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

    }
}