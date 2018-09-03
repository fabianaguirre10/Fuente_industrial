using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class ProductCategoryDao : ADao
    {
        public ProductCategoryDao(MardisContext mardisContext) : base(mardisContext)
        {

        }
        
        public List<ProductCategory> GetProductCategoriesByCustomer(Guid idCustomer, Guid idAccount)
        {
            return Context.ProductCategories
                .Where(pc => pc.IdCustomer == idCustomer &&
                             pc.StatusRegister == CStatusRegister.Active &&
                             pc.Customer.IdAccount == idAccount)
                .ToList();
        }
        
        public List<ProductCategory> GetProductCategories(Guid idCustomer, Guid idAccount)
        {
            return Context.ProductCategories
                .Where(pc => pc.IdCustomer == idCustomer &&
                             pc.StatusRegister == CStatusRegister.Active &&
                             pc.Customer.IdAccount == idAccount)
                .ToList();
        }
        
        public ProductCategory GetProductCategoryById(Guid idProductCategory, Guid idAccount)
        {
            return Context.ProductCategories
                .SingleOrDefault(pc => pc.Id == idProductCategory &&
                                pc.StatusRegister == CStatusRegister.Active &&
                                pc.Customer.IdAccount == idAccount);
        }
        
        public ProductCategory GetOne(Guid id, Guid idAccount)
        {
            return Context.ProductCategories
                                             .FirstOrDefault(tb => tb.StatusRegister == CStatusRegister.Active &&
                                                   tb.Id == id &&
                                                   tb.Customer.IdAccount == idAccount);
        }
    }
}
