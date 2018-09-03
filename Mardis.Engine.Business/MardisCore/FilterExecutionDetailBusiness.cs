using System;
using System.Collections.Generic;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;

namespace Mardis.Engine.Business.MardisCore
{
    public class FilterExecutionDetailBusiness
    {
        readonly FilterExecutionDetailDao _filterExecutionDetailDao;

        public FilterExecutionDetailBusiness(MardisContext mardisContext)
        {
            _filterExecutionDetailDao = new FilterExecutionDetailDao(mardisContext);
        }

        /// <summary>
        /// Dame Detalles de Ejecución 
        /// </summary>
        /// <param name="idFilterExecution"></param>
        /// <returns></returns>
        public List<FilterExecutionDetail> GetExecutionDetailByExecution(Guid idFilterExecution) {

            return _filterExecutionDetailDao.GetExecutionDetailByExecution(idFilterExecution);
        }


        public FilterExecutionDetail GetOne(Guid id) {

            return _filterExecutionDetailDao.GetOne(id);
        }

        public void InsertOrUpdate(FilterExecutionDetail entity)
        {
            _filterExecutionDetailDao.InsertOrUpdate(entity);
        }

        public void PhysicalDelete(FilterExecutionDetail entity)
        {
            _filterExecutionDetailDao.PhysicalDelete(entity);
        }

    }
}
