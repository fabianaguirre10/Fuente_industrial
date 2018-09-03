using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class FilterExecutionDetailDao : ADao
    {
        public FilterExecutionDetailDao(MardisContext mardisContext)
            : base(mardisContext)
        {

        }
        
        public List<FilterExecutionDetail> GetExecutionDetailByExecution(Guid idFilterExecution) {

            var itemsReturn = Context.FilterExecutionDetails
                                     .Include(tb=>tb.FilterCriteria.TypeFilter)
                                     .Include(tb2=>tb2.FilterCriteria.FilterField)
                                     .Where(tb => tb.IdFilterExecution == idFilterExecution)
                                     .OrderBy(tb => tb.CreationFilter)
                                     .ToList();

            return itemsReturn;
        }


        public FilterExecutionDetail GetOne(Guid id) {

            var itemReturn = Context.FilterExecutionDetails
                                    .FirstOrDefault(tb => tb.Id == id);

            return itemReturn;
        }




    }
}
