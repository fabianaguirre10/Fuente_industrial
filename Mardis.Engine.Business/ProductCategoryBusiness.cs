using System;
using System.Collections.Generic;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;

namespace Mardis.Engine.Business
{
    public class ProductCategoryBusiness
    {
        readonly ProductCategoryDao _productCategoryDao;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mardisContext"></param>
        public ProductCategoryBusiness(MardisContext mardisContext)
        {
            _productCategoryDao = new ProductCategoryDao(mardisContext);
        }
        
        public List<ProductCategory> GetProductCategoriesByCustomer(Guid idCustomer, Guid idAccount)
        {
            return _productCategoryDao.GetProductCategoriesByCustomer(idCustomer, idAccount);
        }

        public List<ProductCategory> GetProductCategories(Guid idCustomer, Guid idAccount)
        {
            return _productCategoryDao.GetProductCategories(idCustomer, idAccount);
        }
        
        public ProductCategory GetProductCategoryById(Guid idProductCategory, Guid idAccount)
        {
            return _productCategoryDao.GetProductCategoryById(idProductCategory, idAccount);
        }
        
        public ProductCategory GetOne(Guid id, Guid idAccount)
        {
            return _productCategoryDao.GetOne(id, idAccount);
        }
    }
}
