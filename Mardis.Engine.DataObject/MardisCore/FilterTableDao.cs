using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class FilterTableDao : ADao
    {
        public FilterTableDao(MardisContext mardisContext) 
               : base(mardisContext)
        {

        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="idFilterController"></param>
        /// <returns></returns>
        public List<FilterTable> GetFilterTable(Guid idFilterController)
        {
            var itemReturn = Context.FilterTables
                .Where(tb => tb.IdFilterController == idFilterController)
                .OrderBy(tb=>tb.TableInitial)
                .ToList();

            return itemReturn;
        }

        public List<FilterTable> GetVisiblesFilterTable(Guid idFilterController)
        {
            var itemReturn = Context.FilterTables
                .Where(tb => tb.IdFilterController == idFilterController &&
                             tb.Visible == CTable.Visible)
                .ToList();

            return itemReturn;
        }
    }
}
