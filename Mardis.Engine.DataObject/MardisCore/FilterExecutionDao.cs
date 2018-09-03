using System;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class FilterExecutionDao : ADao
    {
        public FilterExecutionDao(MardisContext mardisContext)
               : base(mardisContext)
        {

        }
        
        public FilterExecution GetFilterExecution(string controller, Guid idUser)
        {

            var valueReturn =
                Context.FilterExecutions
                       .Include(tb => tb.FilterControllers)
                       .Join(Context.FilterControllers,
                             fe => fe.IdFilterController,
                             fc => fc.Id,
                             (fe, fc) => new { fe, fc })
                       .Where(tb => tb.fc.NameController == controller
                             && tb.fe.IdUser == idUser)
                       .Select(tb => tb.fe)
                       .FirstOrDefault();

            return valueReturn;
        }
        
        public FilterExecution GetFilterExecution(Guid idFilterExecution)
        {
            var query = Context.FilterExecutions
                               .Include(tb => tb.FilterControllers)
                               .FirstOrDefault(tb => tb.Id == idFilterExecution);

            return query;
        }
    }
}
