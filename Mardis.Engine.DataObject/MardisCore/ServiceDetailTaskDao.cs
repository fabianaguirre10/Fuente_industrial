using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class ServiceDetailTaskDao : ADao
    {
        public ServiceDetailTaskDao(MardisContext mardisContext) : base(mardisContext)
        {
        }

        public ServiceDetailTask AddSection(ServiceDetailTask serviceDetailTask)
        {
            return InsertOrUpdate(serviceDetailTask);
        }

        public int GetCount(Guid idServiceDetail, Guid idTask, Guid idAccount)
        {
            return Context.ServiceDetailTasks
                .Count(s => s.IdServiceDetail == idServiceDetail &&
                            s.IdTask == idTask &&
                            s.StatusRegister == CStatusRegister.Active &&
                            s.TaskCampaign.IdAccount == idAccount);
        }

        public List<ServiceDetailTask> GetServiceDetailTasks(Guid idServiceDetail, Guid idTask, Guid idAccount)
        {
            return Context.ServiceDetailTasks
                .Where(s => s.IdServiceDetail == idServiceDetail &&
                            s.IdTask == idTask &&
                            s.StatusRegister == CStatusRegister.Active &&
                            s.TaskCampaign.IdAccount == idAccount)
                .ToList();
        }

    }
}
