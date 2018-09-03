using System.Collections.Generic;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject;

namespace Mardis.Engine.Business
{
    public class CatalogBusiness
    {
        private readonly CatalogDao _catalogDao;

        public CatalogBusiness(MardisContext mardisContext)
        {
            _catalogDao = new CatalogDao(mardisContext);
        }

        /// <summary>
        /// Dame todos los estados de Clientes
        /// </summary>
        /// <returns></returns>
        public List<StatusCustomer> GetStatusCustomers()
        {
            return _catalogDao.GetStatusCustomers();
        }

        /// <summary>
        /// Dame tipo de clientes
        /// </summary>
        /// <returns></returns>
        public List<TypeCustomer> GetTypeCustomers()
        {
            return _catalogDao.GetTypeCustomers();
        }
    }
}
