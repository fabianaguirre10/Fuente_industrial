using System;
using System.Collections.Generic;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;

namespace Mardis.Engine.Business.MardisCore
{
    public class BulkLoadCatalogBusiness : ABusiness
    {

        private readonly BulkLoadCatalogDao _bulkLoadCatalogDao;

        public BulkLoadCatalogBusiness(MardisContext mardisContext) 
              : base(mardisContext)
        {
            _bulkLoadCatalogDao = new BulkLoadCatalogDao(mardisContext);
        }

        /// <summary>
        /// Dame uno
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BulkLoadCatalog GetOne(Guid id)
        {
            return _bulkLoadCatalogDao.GetOne(id);
        }

        /// <summary>
        /// Dame carga de catalogo
        /// </summary>
        /// <returns></returns>
        public List<BulkLoadCatalog> GetLoadCatalog()
        {
            return _bulkLoadCatalogDao.GetLoadCatalog();
        }

    }
}
