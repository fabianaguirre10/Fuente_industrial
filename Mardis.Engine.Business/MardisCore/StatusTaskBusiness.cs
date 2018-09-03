using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;
using Mardis.Engine.Framework;

namespace Mardis.Engine.Business.MardisCore
{
    public class StatusTaskBusiness : ABusiness
    {
        private readonly RedisCache _myCache;
        private const string CacheName = "StatusTaskIndustrial";

        public StatusTaskBusiness(MardisContext mardisContext, RedisCache cache) : base(mardisContext)
        {
            var statusTaskDao = new StatusTaskDao(mardisContext);
            _myCache = cache;
            if (_myCache.Get<List<StatusTask>>(CacheName) == null)
            {
                _myCache.Set(CacheName, statusTaskDao.GetAllStatusTasks());
            }
        }

        /// <summary>
        /// Recupera el status de tareas por el Nombre
        /// </summary>
        /// <param name="nameStatusTask">Nombre del Status de la Tarea</param>
        /// <returns>Objeto StatusTask </returns>
        public StatusTask GeStatusTaskByName(string nameStatusTask)
        {
            return _myCache.Get<List<StatusTask>>(CacheName).FirstOrDefault(s => s.Name == nameStatusTask);
        }

        /// <summary>
        /// Obtiene todos los estados de las Tareas
        /// </summary>
        /// <returns></returns>
        public List<StatusTask> GetAllStatusTasks(Guid idaccount, Guid iduser)
        {

            var _status = from st in _myCache.Get<List<StatusTask>>(CacheName)
                          join stc in Context.StatusTaskAccounts
                          on st.Id equals stc.Idstatustask
                          join stu in Context.StatustaskUsers
                           on   st.Id equals stu.Idstatustask
                          where stc.Idaccount == idaccount
                          && stu.Iduser== iduser
                          orderby stc.ORDER
                          select st;
            return _status.ToList();
        }

        public StatusTask GetStatusTask(Guid idStatusTask)
        {
            return _myCache.Get<List<StatusTask>>(CacheName).FirstOrDefault(s => s.Id == idStatusTask);
        }

    }
}
