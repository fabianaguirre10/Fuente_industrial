using System;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class FilterCriteriaDao : ADao
    {
        public FilterCriteriaDao(MardisContext mardisContext) 
               : base(mardisContext)
        {

        }
        
        public FilterCriteria GetFilterCriteria(Guid idFilterField, Guid idTypeFilter) {

            var returnValue = Context.FilterCriterias
                                     .Include(tb => tb.TypeFilter)
                                     .FirstOrDefault(tb => tb.IdFilterField == idFilterField && tb.IdTypeFilter == idTypeFilter);

            return returnValue;
        }
    }
}
