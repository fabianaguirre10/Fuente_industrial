using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class BulkLoadDao : ADao
    {
        public BulkLoadDao(MardisContext mardisContext)
            : base(mardisContext)
        {

        }
        
        public List<BulkLoad> GetDataByAccount(Guid idAccount)
        {
            var itemsResult = Context.BulkLoads
                                     .Where(tb => tb.IdAccount == idAccount &&
                                                  tb.StatusRegister == CStatusRegister.Active)
                                     .OrderBy(tb => tb.CreatedDate)
                                     .ToList(); 

            return itemsResult;                                   
        }
        
        public BulkLoad GetOne(Guid id)
        {
            var itemReturn = Context.BulkLoads
                                    .FirstOrDefault(tb => tb.Id == id &&
                                           tb.StatusRegister == CStatusRegister.Active);


            return itemReturn;
        }

    }
}
