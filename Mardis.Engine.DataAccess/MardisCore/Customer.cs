using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mardis.Engine.DataAccess.MardisCommon;

namespace Mardis.Engine.DataAccess.MardisCore
{
    /// <summary>
    /// Clientes de la Cuenta en el Sistema
    /// </summary>
    [Table("Customer", Schema = "MardisCore")]
    public class Customer : IEntity, ISoftDelete
    {

        public Customer()
        {
            BranchCustomers = new List<BranchCustomer>();
            Channels = new List<Channel>();
            TypeBusiness = new List<TypeBusiness>();
            Products = new List<Product>();
            ProductCategories = new List<ProductCategory>();
        }

        [Key]
        public Guid Id { get; set; }
        public Guid IdAccount { get; set; }
        public string Code { get; set; }
        public DateTime DateCreation { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public Guid IdTypeCustomer { get; set; }
        public Guid IdStatusCustomer { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string StatusRegister { get; set; }

        [ForeignKey("IdAccount")]
        public Account Account { get; set; }
        [ForeignKey("IdStatusCustomer")]
        public StatusCustomer StatusCustomer { get; set; }
        [ForeignKey("IdTypeCustomer")]
        public TypeCustomer TypeCustomer { get; set; }
        public List<BranchCustomer> BranchCustomers { get; set; }
        public List<Channel> Channels { get; set; }
        public List<TypeBusiness> TypeBusiness { get; set; }

        public List<Product> Products { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }
    }
}
