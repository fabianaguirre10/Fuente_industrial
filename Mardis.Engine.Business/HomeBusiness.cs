using System;
using System.Collections.Generic;
using Mardis.Engine.Business.MardisCore;
using Mardis.Engine.Business.MardisSecurity;
using Mardis.Engine.Converter;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisSecurity;
using Mardis.Engine.DataObject.MardisCore;
using Mardis.Engine.Web.ViewModel.DashBoardViewModels;
using Mardis.Engine.Web.ViewModel.Filter;
using Microsoft.AspNetCore.DataProtection;
using Mardis.Engine.DataObject.MardisCommon;
using Mardis.Engine.Framework.Resources.PagesConstants;

namespace Mardis.Engine.Business
{
    public class HomeBusiness : ABusiness
    {

        private readonly CampaignBusiness _campaignBusiness;
        private readonly UserBusiness _userBusiness;
        private readonly TaskCampaignDao _taskCampaignDao;
        private readonly ProfileBusiness _profileBusiness;
        private readonly CoreFilterDetailDao _coreFilterDetailDao;
        private readonly CoreFilterDao _coreFilterDao;

        public HomeBusiness(MardisContext mardisContext) : base(mardisContext)
        {
            _campaignBusiness = new CampaignBusiness(mardisContext);
            _userBusiness = new UserBusiness(mardisContext);
            _taskCampaignDao = new TaskCampaignDao(mardisContext);
            _profileBusiness = new ProfileBusiness(mardisContext);
            _coreFilterDetailDao = new CoreFilterDetailDao(mardisContext);
            _coreFilterDao = new CoreFilterDao(mardisContext);
        }

        public DashBoardViewModel GetDashBoard(DashBoardViewModel model, List<FilterValue> filters, int pageIndex, int pageSize, Guid idAccount, IDataProtector protector)
        {
            var idCampaign = string.IsNullOrEmpty(model.IdCampaign) ? Guid.Empty : Guid.Parse(protector.Unprotect(model.IdCampaign));
            var totalTasks = 0;


            if (idCampaign != Guid.Empty)
            {
                var merchants = _userBusiness.GetMerchantsByCampaign(idCampaign, idAccount, filters, pageIndex, pageSize);

                //model.MerchantList = DashBoardConvert.ConvertUserListToDashBoardMerchantViewModelList(merchants);
                model.MerchantList = GetMerchantInformation(merchants);
                var campaign = _campaignBusiness.GetCampaignById(idCampaign, idAccount);
                var campaignDetails = _campaignBusiness.GetCampaignTaskDetails(idCampaign, idAccount);

                model = ConvertDashBoard.FromCampaign(model, campaign, campaignDetails);

                var tasks = _taskCampaignDao.GetPaginatedTasksByCampaign(pageIndex, pageSize, filters, idAccount);

                model.BranchList = ConvertTask.ConvertTaskListToCampaignBranchesViewModelList(tasks);

                totalTasks = _taskCampaignDao.GetPaginatedTasksCount(idAccount, filters);
            }

            return ConfigurePagination(model, pageIndex, pageSize, filters, totalTasks);
        }

        private List<DashBoardMerchantViewModel> GetMerchantInformation(List<User> merchants)
        {
            var listResult = new List<DashBoardMerchantViewModel>();

            var filter = _coreFilterDao.GetCoreFilter("DashBoard");
            var filterMerchant = _coreFilterDetailDao.GetCoreFilterDetail(filter.Id, "IdMerchant");

            foreach (var merchant in merchants)
            {

                var filterMerch = new FilterValue()
                {
                    Criteria = "==",
                    NameFilter = "IdMerchant",
                    Value = merchant.Id.ToString(),
                    IdFilter = filterMerchant.Id
                };

                var filters = new List<FilterValue>()
                {
                     filterMerch
                };

                var profile = _profileBusiness.GetById(merchant.IdProfile);
                var taskCount = _taskCampaignDao.GetPaginatedTasksCount(merchant.IdAccount, filters);

                var statusFilter = _coreFilterDetailDao.GetCoreFilterDetail(filter.Id, "Name", "StatusTask");

                var filterStatus = new FilterValue()
                {
                    Criteria = "==",
                    NameFilter = "IdStatusTask",
                    Value = CTask.StatusImplemented,
                    IdFilter = statusFilter.Id
                };

                filters.Add(filterStatus);

                var countImplemented = _taskCampaignDao.GetPaginatedTasksCount(merchant.IdAccount, filters);

                filterStatus.Value = CTask.StatusNotImplemented;
                filters = new List<FilterValue>()
                {
                    filterMerch,
                    filterStatus
                };

                var countNotImplemented = _taskCampaignDao.GetPaginatedTasksCount(merchant.IdAccount, filters);

                filterStatus.Value = CTask.StatusStarted;
                filters = new List<FilterValue>()
                {
                    filterMerch,
                    filterStatus
                };

                var countStarted = _taskCampaignDao.GetPaginatedTasksCount(merchant.IdAccount, filters);

                filterStatus.Value = CTask.StatusPending;
                filters = new List<FilterValue>()
                {
                    filterMerch,
                    filterStatus
                };

                var countPending = _taskCampaignDao.GetPaginatedTasksCount(merchant.IdAccount, filters);

                var model = new DashBoardMerchantViewModel()
                {
                    Id = merchant.Id,
                    Code = profile.Code,
                    Name = profile.Name,
                    TaskCount = taskCount,
                    PercentImplementedTasks = ((countImplemented * 100) / taskCount) + "%",
                    PercentStartedTasks = ((countStarted * 100) / taskCount) + "%",
                    PercentNotImplementedTasks = ((countNotImplemented * 100) / taskCount) + "%",
                    PercentPendingTasks = ((countPending * 100) / taskCount) + "%"
                };

                listResult.Add(model);
            }

            return listResult;
        }
    }
}
