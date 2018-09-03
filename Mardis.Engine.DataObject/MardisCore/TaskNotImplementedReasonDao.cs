using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class TaskNotImplementedReasonDao:ADao
    {
        public TaskNotImplementedReasonDao(MardisContext mardisContext) : base(mardisContext)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TaskNoImplementedReason> GetAllTaskNotImplementedReason()
        {
            return Context.TaskNoImplementedReasons
                .Where(tn => tn.StatusRegister == CStatusRegister.Active)
                .ToList();
        }


    }
}
