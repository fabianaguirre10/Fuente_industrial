using System;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;

namespace Mardis.Engine.Business.MardisCore
{
    public class FilterCriteriaBusiness
    {
        readonly FilterCriteriaDao _filterCriteriaDao;

        public FilterCriteriaBusiness(MardisContext mardisContext)
        {
            _filterCriteriaDao = new FilterCriteriaDao(mardisContext);
        }
        
        public FilterCriteria GetFilterCriteria(Guid idFilterField, Guid idTypeFilter) {

            return _filterCriteriaDao.GetFilterCriteria(idFilterField, idTypeFilter);
        }

        public void InsertOrUpdate(FilterCriteria entity)
        {
            _filterCriteriaDao.InsertOrUpdate(entity);
        }
    }
}
