using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class FilterFieldsDao : ADao
    {
        public FilterFieldsDao(MardisContext mardisContext) 
            : base(mardisContext)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idFilterTable"></param>
        /// <returns></returns>
        public List<FilterField> GetFilterFieldByTable(Guid idFilterTable) {


            var itemReturn = Context.FilterFields
                                    .Where(tb => tb.IdFilterTable == idFilterTable)
                                    .Select(tb => tb)
                                    .Distinct()
                                    .ToList();


            return itemReturn;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="idFilterTable"></param>
        /// <returns></returns>
        public List<FilterField> GetFilterVisibleFieldByTable(Guid idFilterTable)
        {


            var itemReturn = Context.FilterFields
                                    .Join(Context.FilterCriterias,
                                        tb => tb.Id,
                                        fc => fc.IdFilterField,
                                        (tb, fc) => new { tb, fc })
                                    .Where(tb => tb.tb.IdFilterTable == idFilterTable && 
                                                 tb.tb.Visible == CTable.Visible)
                                    .Select(tb => tb.tb)
                                    .Distinct()
                                    .ToList();


            return itemReturn;
        }
    }
}
