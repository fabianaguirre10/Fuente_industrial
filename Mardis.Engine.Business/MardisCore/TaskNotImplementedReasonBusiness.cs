using System.Collections.Generic;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;

namespace Mardis.Engine.Business.MardisCore
{
    public class TaskNotImplementedReasonBusiness : ABusiness
    {

        private readonly TaskNotImplementedReasonDao _taskNotImplementedReasonDao;

        public TaskNotImplementedReasonBusiness(MardisContext mardisContext) : base(mardisContext)
        {
            _taskNotImplementedReasonDao = new TaskNotImplementedReasonDao(mardisContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TaskNoImplementedReason> GetAllTaskNotImplementedReason()
        {
            return _taskNotImplementedReasonDao.GetAllTaskNotImplementedReason();
        }
    }
}
