using System;
using System.Collections.Generic;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;

namespace Mardis.Engine.Business.MardisCore
{
    public class FilterFieldsBusiness
    {
        readonly FilterFieldsDao _filterFieldsDao;

        public FilterFieldsBusiness(MardisContext mardisContext)
        {
            _filterFieldsDao = new FilterFieldsDao(mardisContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idFilterTable"></param>
        /// <returns></returns>
        public List<FilterField> GetFilterFieldByTable(Guid idFilterTable) {

            return _filterFieldsDao.GetFilterFieldByTable(idFilterTable);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="idFilterTable"></param>
        /// <returns></returns>
        public List<FilterField> GetFilterVisibleFieldByTable(Guid idFilterTable)
        {
            return _filterFieldsDao.GetFilterVisibleFieldByTable(idFilterTable);
        }
    }
}
