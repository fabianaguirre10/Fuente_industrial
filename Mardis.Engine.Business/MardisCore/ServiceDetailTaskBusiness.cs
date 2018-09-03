using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;

namespace Mardis.Engine.Business.MardisCore
{
    public class ServiceDetailTaskBusiness : ABusiness
    {

        private readonly ServiceDetailTaskDao _serviceDetailTaskDao;

        public ServiceDetailTaskBusiness(MardisContext mardisContext) : base(mardisContext)
        {
            _serviceDetailTaskDao=new ServiceDetailTaskDao(mardisContext);
        }

        public ServiceDetailTask AddSection(Guid idServiceDetail, Guid idTask)
        {
            var serviceDetailTask=new ServiceDetailTask()
            {
                IdServiceDetail = idServiceDetail,
                IdTask = idTask
            };

            var itemResult= _serviceDetailTaskDao.AddSection(serviceDetailTask);

            return itemResult;

        }

        public List<ServiceDetailTask> GetSections(Guid idTask, Guid idServiceDetail, Guid idAccount)
        {
            return Context.ServiceDetailTasks
                .Where(
                    s =>
                        s.IdServiceDetail == idServiceDetail && 
                        s.IdTask == idTask &&
                        s.TaskCampaign.IdAccount == idAccount)
                .ToList();
        }
    }
}
