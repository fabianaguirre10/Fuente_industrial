using System;
using System.Collections.Generic;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;

namespace Mardis.Engine.Business.MardisCore
{
    public class FilterTableBusiness
    {
        readonly FilterTableDao _filterTableDao;

        public FilterTableBusiness(MardisContext mardisContext)
        {
            _filterTableDao = new FilterTableDao(mardisContext);
        }

        /// <summary>
        /// Dame Filtros de Tabla
        /// </summary>
        /// <param name="idFilterController"></param>
        /// <returns></returns>
        public List<FilterTable> GetFilterTable(Guid idFilterController) {
            return _filterTableDao.GetFilterTable(idFilterController);
        }

        /// <summary>
        /// Dame Visibles de Filtro de Tabla
        /// </summary>
        /// <param name="idFilterController"></param>
        /// <returns></returns>
        public List<FilterTable> GetVisiblesFilterTable(Guid idFilterController)
        {
            return _filterTableDao.GetVisiblesFilterTable(idFilterController);
        }
    }
}
