using System;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;

namespace Mardis.Engine.Business.MardisCore
{
    /// <summary>
    /// Negocio de Ejecución de Filtro
    /// </summary>
    public class FilterExecutionBusiness
    {
        readonly FilterExecutionDao _filterExecutionDao;

        public FilterExecutionBusiness(MardisContext mardisContext)
        {
            _filterExecutionDao = new FilterExecutionDao(mardisContext);
        }

        
        public FilterExecution GetFilterExecution(string controller, Guid idUser)
        {
            return _filterExecutionDao.GetFilterExecution(controller, idUser);
        }
        
        public FilterExecution GetFilterExecution(Guid idFilterExecution)
        {
            return _filterExecutionDao.GetFilterExecution(idFilterExecution);
        }

        public void InsertOrUpdate(FilterExecution entity)
        {
            _filterExecutionDao.InsertOrUpdate(entity);
        }
    }
}
