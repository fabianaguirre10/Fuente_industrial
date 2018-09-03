using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class StatusTaskDao : ADao
    {
        public StatusTaskDao(MardisContext mardisContext) : base(mardisContext)
        {

        }

        /// <summary>
        /// Dame un registro por id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StatusTask GetOne(Guid id)
        {
            return Context.StatusTasks
                                      .FirstOrDefault(tb => tb.Id == id &&
                                                 tb.StatusRegister == CStatusRegister.Active);
        }

        /// <summary>
        /// Recupera el status de tareas por el Nombre
        /// </summary>
        /// <param name="nameStatusTask">Nombre del Status de la Tarea</param>
        /// <returns>Objeto StatusTask </returns>
        public StatusTask GetStatusTaskByName(string nameStatusTask)
        {
            return Context.StatusTasks
                .FirstOrDefault(st => st.Name == nameStatusTask);
        }

        /// <summary>
        /// Obtiene todos los estados de las Tareas
        /// </summary>
        /// <returns></returns>
        public List<StatusTask> GetAllStatusTasks()
        {
            return Context.StatusTasks
                .Where(st => st.StatusRegister == CStatusRegister.Active)
                .ToList();
        }
    }
}
