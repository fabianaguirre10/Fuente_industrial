using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class TypeFilterDao : ADao
    {
        public TypeFilterDao(MardisContext mardisContext) 
               : base(mardisContext)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idFilterField"></param>
        /// <returns></returns>
        public List<TypeFilter> GetTypeFilterByField(Guid idFilterField) {
            var itemsReturn = Context.TypeFilters
                                 .Join(Context.FilterCriterias,
                                        tb => tb.Id,
                                        fc => fc.IdTypeFilter,
                                        (tb, fc) => new { tb, fc })
                                 .Where(tb => tb.fc.IdFilterField == idFilterField)
                                 .Select(tb => tb.tb).ToList();

            return itemsReturn;
        }
    }
}
