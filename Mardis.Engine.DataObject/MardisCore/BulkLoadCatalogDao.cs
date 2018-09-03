using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class BulkLoadCatalogDao : ADao
    {
        public BulkLoadCatalogDao(MardisContext mardisContext) 
               : base(mardisContext)
        {

        }

        /// <summary>
        /// Dame uno
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BulkLoadCatalog GetOne(Guid id)
        {
            var itemReturn = Context.BulkLoadCatalogs
                                     .FirstOrDefault(tb => tb.Id == id &&
                                                tb.StatusRegister == CStatusRegister.Active);

            return itemReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BulkLoadCatalog> GetLoadCatalog()
        {
            return Context.BulkLoadCatalogs
                          .Where(tb => tb.StatusRegister == CStatusRegister.Active)
                          .ToList();
        }
    }
}
