using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Mardis.Engine.Converter;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.ViewModel;
using Mardis.Engine.Web.ViewModel.CampaignViewModels;
using Mardis.Engine.Web.ViewModel.Filter;
using Mardis.Engine.Web.ViewModel.TaskViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Mardis.Engine.Web.ViewModel.BranchViewModels;
using System.Threading.Tasks;
using AutoMapper;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataObject.MardisCommon;

namespace Mardis.Engine.Business.MardisCore
{
    public class CampaignBusiness : ABusiness
    {
        private readonly CampaignDao _campaignDao;
        private readonly SequenceBusiness _sequenceBusiness;
        private readonly CustomerDao _customerDao;
        private readonly TaskCampaignDao _taskCampaignDao;
        private readonly StatusTaskDao _statusTaskDao;
        private readonly CampaignServicesDao _campaignServicesDao;
        private readonly ServiceDao _serviceDao;
        private readonly BranchDao _branchDao;
        private readonly UserCanpaignDao _userCanpaignDao;
        private readonly DashboardDao _dashboardDao;
        private readonly PersonDao _personDao;
        public CampaignBusiness(MardisContext mardisContext) : base(mardisContext)
        {
            _campaignDao = new CampaignDao(mardisContext);
            _sequenceBusiness = new SequenceBusiness(mardisContext);
            _customerDao = new CustomerDao(mardisContext);
            _taskCampaignDao = new TaskCampaignDao(mardisContext);
            _statusTaskDao = new StatusTaskDao(mardisContext);
            _campaignServicesDao = new CampaignServicesDao(mardisContext);
            _serviceDao = new ServiceDao(mardisContext);
            _branchDao = new BranchDao(mardisContext);
            _userCanpaignDao = new UserCanpaignDao(mardisContext);
            _dashboardDao = new DashboardDao(mardisContext);
            _personDao = new PersonDao(mardisContext);
        }

        public Campaign GetCampaignById(Guid idCampaign, Guid idAccount)
        {
            return _campaignDao.GetCampaignById(idCampaign, idAccount);
        }

        public Campaign GetCampaignByName(string nameCampaign, Guid idAccount)
        {
            return _campaignDao.GetCampaignByName(nameCampaign, idAccount);
        }
        public object GetCampanigAccount()
        {
            return _campaignDao.GetCampaing();
        }

        public Campaign SaveCampaign(Campaign campaign, List<ListCampaignServicesViewModel> itemServices, Guid idAccount)
        {
            campaign.Supervisor = null;
            campaign.Customer = null;
            campaign.CampaignServices.Clear();
            campaign.IdAccount = idAccount;
            campaign.StatusRegister = CStatusRegister.Active;

            foreach (var servicios in itemServices)
            {
                campaign.CampaignServices.Add(new CampaignServices()
                {
                    IdService = servicios.IdService,
                    StatusRegister = CStatusRegister.Active,
                    IdAccount = idAccount
                });

            }

            var stateRegister = EntityState.Added;

            using (var transaccion = Context.Database.BeginTransaction())
            {
                try
                {
                    if (string.IsNullOrEmpty(campaign.Code))
                    {
                        var nextSequence = _sequenceBusiness.NextSequence(CCampaign.SequenceCode, idAccount);
                        var cust = _customerDao.GetCustomerById(campaign.IdCustomer, idAccount);

                        campaign.Code = nextSequence.ToString();
                        campaign.Name = nextSequence.SequenceCurrent.ToString();
                        campaign.Name += "-" + cust.Abbreviation.Trim() + "-" +
                            campaign.CreationDate.ToString("MMMM", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        //elimino los CampaignServices
                        Context.CampaignsServices.RemoveRange(Context.CampaignsServices.Where(cs => cs.IdCampaign == campaign.Id));
                        Context.SaveChanges();
                        campaign.CampaignServices.Clear();
                    }

                    if (Guid.Empty != campaign.Id)
                    {
                        stateRegister = EntityState.Modified;
                    }

                    Context.Campaigns.Add(campaign);
                    Context.Entry(campaign).State = stateRegister;
                    Context.SaveChanges();
                    transaccion.Commit();
                }
                catch (Exception)
                {
                    transaccion.Rollback();
                    campaign = null;
                }
            }

            return campaign;
        }

        public bool Save(CampaignRegisterViewModel model, Guid accountId)
        {
            var campaign = ConvertCampaign.FromCampaignRegisterViewModel(model);
            var campaignServices = GetCampaignServices(model);

            SaveCampaign(campaign, campaignServices, accountId);
            return true;
        }

        private List<ListCampaignServicesViewModel> GetCampaignServices(CampaignRegisterViewModel model)
        {
            var resultList = new List<ListCampaignServicesViewModel>();

            if (!string.IsNullOrEmpty(model.DeletedServices))
            {
                if (!string.IsNullOrEmpty(model.NewServices))
                {
                    var deleted = model.DeletedServices.Split(';').Where(d => d != string.Empty);
                    foreach (var item in deleted)
                    {
                        model.NewServices = model.NewServices.Replace(";" + item, "");
                    }
                }
            }
            if (!string.IsNullOrEmpty(model.NewServices))
            {
                resultList.AddRange(model.NewServices.Split(';').Where(s => s != string.Empty).Select(item => new ListCampaignServicesViewModel()
                {
                    IdService = Guid.Parse(item)
                }));
            }

            return resultList;
        }

        public CampaignTaskDetailViewModel GetCampaignTaskDetails(Guid idCampaign, Guid idAccount)
        {
            var campaign = _campaignDao.GetOne(idCampaign, idAccount);

            int numberNotImplementedTasks;
            int numberStartedTasks;
            int numberPendingTasks;
            int numberImplementedTasks;
            var totalTasks = GetCampaignStatistics(idAccount, campaign, out numberNotImplementedTasks, out numberStartedTasks, out numberPendingTasks, out numberImplementedTasks);

            var ts = campaign.EndDate - DateTime.Now;

            var itemResult = new CampaignTaskDetailViewModel()
            {
                CountImplementedTasks = numberImplementedTasks,
                CountNotImplementedTasks = numberNotImplementedTasks,
                CountPendingTasks = numberPendingTasks,
                CountStartedTasks = numberStartedTasks,
                RemainingDays = ts.Days,
                CountTotalTasks = totalTasks
            };

            return itemResult;
        }

        public CampaignTaskDetailViewModel GeCampaignTaskDetailsByMerchant(Guid idCampaign, Guid idMerchant, Guid idAccount)
        {
#if DEBUG
            var myWatch = new Stopwatch();
            myWatch.Start();
#endif
            var statusTask = _statusTaskDao.GetStatusTaskByName(CTask.StatusImplemented);
            var implementedTaskCount = _taskCampaignDao.GetTaskCountByStatusAndMerchant(idCampaign, statusTask.Id, idMerchant, idAccount);

            statusTask = _statusTaskDao.GetStatusTaskByName(CTask.StatusNotImplemented);
            var notImplementedTaskCount = _taskCampaignDao.GetTaskCountByStatusAndMerchant(idCampaign, statusTask.Id, idMerchant, idAccount);

            statusTask = _statusTaskDao.GetStatusTaskByName(CTask.StatusPending);
            var pendingTaskCount = _taskCampaignDao.GetTaskCountByStatusAndMerchant(idCampaign, statusTask.Id, idMerchant, idAccount);

            statusTask = _statusTaskDao.GetStatusTaskByName(CTask.StatusStarted);
            var startedTaskCount = _taskCampaignDao.GetTaskCountByStatusAndMerchant(idCampaign, statusTask.Id, idMerchant, idAccount);

            var campaign = _campaignDao.GetOne(idCampaign, idAccount);

            TimeSpan ts = campaign.EndDate - DateTime.Now;


            var itemResult = new CampaignTaskDetailViewModel()
            {
                CountImplementedTasks = implementedTaskCount,
                CountNotImplementedTasks = notImplementedTaskCount,
                CountPendingTasks = pendingTaskCount,
                CountStartedTasks = startedTaskCount,
                RemainingDays = ts.Days,
                CountTotalTasks = implementedTaskCount +
                                    notImplementedTaskCount +
                                    pendingTaskCount +
                                    startedTaskCount
            };

#if DEBUG
            myWatch.Stop();
            Debugger.Log(0, "Campañas", $"ms: {myWatch.ElapsedMilliseconds}");
#endif

            return itemResult;
        }

        public List<Campaign> GetActiveCampaignsList(Guid idAccount)
        {
            return _campaignDao.GetActiveCampaignsList(idAccount);
        }
        public List<Dashboard> GetActiveCampaignsListDasboard(Guid idAccount, Guid idUser)
        {
            return _campaignDao.GetActiveCampaignsListDasboard(idAccount, idUser);
        }

        public Dashboard GetDashOne(Guid Id)
        {
            return _dashboardDao.GetOne(Id);
        }

        public Campaign GetSimpleCampaignById(Guid idCampaign, Guid idAccount)
        {
            return _campaignDao.GetOne(idCampaign, idAccount);
        }

        public CampaignBranchesImportedViewModel ImportBranch(Guid idAccount, List<FilterValue> filters)
        {
            return ConfigurePagination(new CampaignBranchesImportedViewModel(), 1, 10, filters, 1);
        }

        public TaskPerCampaignViewModel GetTaskPerCampaignViewModel(Guid idCampaign, Guid idAccount, List<FilterValue> filters)
        {
            filters = filters ?? new List<FilterValue>();

            var itemResult = new TaskPerCampaignViewModel();

            filters = AddHiddenFilter("IdCampaign", idCampaign.ToString(), filters, itemResult.FilterName);

            itemResult = new TaskPerCampaignViewModel
            {
                IdCampaign = idCampaign,
                ImplementedTasksList =
                    ConvertTask.ConvertTaskToMyTaskViewItemModel(_taskCampaignDao.GetPaginatedTasksByCampaignAndStatus(
                         CTask.StatusImplemented, 1, int.MaxValue, filters, idAccount)),
                NotImplementedTasksList =
                    ConvertTask.ConvertTaskToMyTaskViewItemModel(_taskCampaignDao.GetPaginatedTasksByCampaignAndStatus(
                        CTask.StatusNotImplemented, 1, int.MaxValue, filters, idAccount)),
                PendingTasksList =
                    ConvertTask.ConvertTaskToMyTaskViewItemModel(_taskCampaignDao.GetPaginatedTasksByCampaignAndStatus(
                        CTask.StatusPending, 1, int.MaxValue, filters, idAccount)),
                StartedTasksList =
                    ConvertTask.ConvertTaskToMyTaskViewItemModel(_taskCampaignDao.GetPaginatedTasksByCampaignAndStatus(
                        CTask.StatusStarted, 1, int.MaxValue, filters, idAccount))
            };
            return ConfigurePagination(itemResult, 1, int.MaxValue, filters, int.MaxValue);
        }

        public CampaignBranchesImportedViewModel GetBranchesSelected(Guid idAccount, List<FilterValue> filters)
        {
            var model = new CampaignBranchesImportedViewModel();
            var branches = _branchDao.GetAll(filters, 1, int.MaxValue, idAccount);

            var columns = branches.GroupBy(b => b.IdDistrict).Select(b => b.Key).ToList();
            var rows = 0;

            foreach (var idDistrict in columns)
            {
                var count = branches.Count(b => b.IdDistrict == idDistrict);
                if (count > rows)
                {
                    rows = count;
                }
            }

            model.Results = new string[rows + 2, columns.Count + 1];



            return ConfigurePagination(model, 1, 10, filters, 1);
        }
        #region taskDinamic

        public TaskPerCampaignViewModel GetPaginatedTaskPerCampaignViewModelDinamic(Guid idCampaign,  int pageIndex, int pageSize, List<FilterValue> filters, Guid idAccount)
        {
            filters = filters ?? new List<FilterValue>();

            var itemResult = new TaskPerCampaignViewModel();

            filters = AddHiddenFilter("IdCampaign", idCampaign.ToString(), filters, itemResult.FilterName);
            itemResult.tasks = new List<MyStatusTaskViewModel>();
            var data = _taskCampaignDao.statusAllow(idAccount, pageIndex, pageSize);
            int aux = 0;
            int   max = 0;

            foreach (var allow in data)
            {
                aux = _taskCampaignDao.GetTaskCountByCampaignAndStatus(allow.Name, filters, idAccount);
                var taskslist = GetMyTaskViewItemModel(allow.Name,pageIndex, pageSize, filters, idAccount);
                max = (aux > max) ? aux : max;
                itemResult.tasks.Add(new MyStatusTaskViewModel { TasksList = taskslist, CountTasks = aux, type = allow.Name, color = allow.color });
            }
            itemResult.IdCampaign = idCampaign;
            return ConfigurePagination(itemResult, pageIndex, pageSize, filters, max);
        }
        #endregion
        public TaskPerCampaignViewModel GetPaginatedTaskPerCampaignViewModel(Guid idCampaign, int pageIndex, int pageSize, List<FilterValue> filters, Guid idAccount)
        {
            filters = filters ?? new List<FilterValue>();

            var itemResult = new TaskPerCampaignViewModel();

            filters = AddHiddenFilter("IdCampaign", idCampaign.ToString(), filters, itemResult.FilterName);

            var max = _taskCampaignDao.GetTaskCountByCampaignAndStatus(CTask.StatusImplemented, filters, idAccount);
            var countImplementedTasks = max;
            var countNotImplementedTasks = _taskCampaignDao.GetTaskCountByCampaignAndStatus(CTask.StatusNotImplemented, filters, idAccount);

            max = (max > countNotImplementedTasks) ? max : countNotImplementedTasks;

            var countPendingTasks = _taskCampaignDao.GetTaskCountByCampaignAndStatus(CTask.StatusPending, filters, idAccount);

            max = (max > countPendingTasks) ? max : countPendingTasks;

            var countStartedTasks = _taskCampaignDao.GetTaskCountByCampaignAndStatus(CTask.StatusStarted, filters, idAccount);

            max = (max > countStartedTasks) ? max : countStartedTasks;

            itemResult.IdCampaign = idCampaign;
            itemResult.ImplementedTasksList = GetMyTaskViewItemModel(CTask.StatusImplemented, pageIndex, pageSize, filters, idAccount);
            itemResult.NotImplementedTasksList = GetMyTaskViewItemModel(CTask.StatusNotImplemented, pageIndex, pageSize, filters, idAccount);
            itemResult.PendingTasksList = GetMyTaskViewItemModel(CTask.StatusPending, pageIndex, pageSize, filters, idAccount);
            itemResult.StartedTasksList = GetMyTaskViewItemModel(CTask.StatusStarted, pageIndex, pageSize, filters, idAccount);

            itemResult.CountImplementedTasks = countImplementedTasks;
            itemResult.CountNotImplementedTasks = countNotImplementedTasks;
            itemResult.CountPendingTasks = countPendingTasks;
            itemResult.CountStartedTasks = countStartedTasks;

            return ConfigurePagination(itemResult, pageIndex, pageSize, filters, max);
        }

        private List<MyTaskItemViewModel> GetMyTaskViewItemModel(string status, int pageIndex, int pageSize, List<FilterValue> filters, Guid idAccount)
        {
            return
                ConvertTask.ConvertTaskToMyTaskViewItemModel(
                    _taskCampaignDao.GetPaginatedTasksByCampaignAndStatus(status, pageIndex, pageSize, filters,
                        idAccount));
        }

        public CampaignGeopositionViewModel GetCampaignGeoposition(List<FilterValue> filterValues, string campaign, Guid idAccount, IDataProtector protector)
        {
            var idCampaign = Guid.Parse(protector.Unprotect(campaign));

            var resultItem = new CampaignGeopositionViewModel()
            {
                IdCampaign = idCampaign
            };

            filterValues = AddHiddenFilter("IdCampaign", idCampaign.ToString(), filterValues, resultItem.FilterName);

            var locations = _campaignDao.GetCampaignGeopositionBranches(filterValues, idAccount);

            resultItem.LocationList = locations;

            return ConfigurePagination(resultItem, int.MaxValue, int.MaxValue, filterValues, int.MaxValue);
        }


        #region CampaingDinamic

        public CampaignListViewModel GetPaginatedCampaignsDinamic(List<FilterValue> filterValues, int pageSize, int pageNumber, Guid idAccount, IDataProtector protector, Guid userid, Guid _typeuser)
        {
#if DEBUG
            var myWatch = new Stopwatch();
            myWatch.Start();
#endif

            var itemResult = new CampaignListViewModel();
            var campaigns = _campaignDao.GetPaginatedCampaignListbyUser(filterValues, pageSize, pageNumber, idAccount, userid, _typeuser);
            var countCampaigns = _campaignDao.GetPaginatedCampaignCount(filterValues, pageSize, pageNumber, idAccount, _typeuser, userid);
            var _data = _taskCampaignDao.statusAllow(idAccount, pageNumber, pageSize);
            foreach (var campaign in campaigns)
            {
                var ts = campaign.EndDate - DateTime.Now;

                int numberNotImplementedTasks;
                int numberStartedTasks;
                int numberPendingTasks;
                int numberImplementedTasks;
                //var totalTasks = GetCampaignStatistics(idAccount, campaign, out numberNotImplementedTasks, out numberStartedTasks, out numberPendingTasks, out numberImplementedTasks);
                var totalTasks = _campaignDao.NumbertaskbyCampaign(campaign.Id);
                var usercampaign = _userCanpaignDao.GetCampaignById(campaign.Id, userid);
                if (_typeuser.Equals(new Guid("30DB815C-8B82-47EE-9279-B28922BEB616")))
                {
                    if (usercampaign.Count > 0)
                    {
                        var cvm = new CampaignItemViewModel
                        {
                            EndDate = campaign.EndDate,
                            Id = protector.Protect(campaign.Id.ToString()),
                            Name = campaign.Name,
                            StartDate = campaign.StartDate,
                            RemainingDays = ts.Days,

                            //ImplementedTaskPercent = (totalTasks > 0) ? ((numberImplementedTasks * 100) / totalTasks) + "%" : "0%",
                            //NotImplementedTaskPercent = (totalTasks > 0) ? ((numberNotImplementedTasks * 100) / totalTasks) + "%" : "0%",
                            //StartedTaskPercent = (totalTasks > 0) ? ((numberStartedTasks * 100) / totalTasks) + "%" : "0%",
                            //PendingTaskPercent = (totalTasks > 0) ? ((numberPendingTasks * 100) / totalTasks) + "%" : "0%",
                            //CountImplementedTasks = numberImplementedTasks,
                            //CountNotImplementedTasks = numberNotImplementedTasks,
                            //CountPendingTasks = numberPendingTasks,
                            //CountStartedTasks = numberStartedTasks,
                            TotalTasks = totalTasks
                        };
                        int aux = 0;
                        int max = 0;
             
                        foreach (var allow in _data)
                        {
                            aux = _taskCampaignDao.GetTaskCountByCampaignAndStatusStadi(allow.Name, campaign.Id, idAccount);
                            max = (aux > max) ? aux : max;
                            cvm.sectionCampaign.Add(new SectionCampaignDinamicViewModels  { name=allow.Name,total=aux,percent= (totalTasks > 0) ? ((aux * 100) / totalTasks) + "%" : "0%",color=allow
                            .color});
                        }
                        itemResult.CampaignList.Add(cvm);
                    }
                }
                else
                {
                    var cvm = new CampaignItemViewModel
                    {
                        EndDate = campaign.EndDate,
                        Id = protector.Protect(campaign.Id.ToString()),
                        Name = campaign.Name,
                        StartDate = campaign.StartDate,
                        RemainingDays = ts.Days,

                        //ImplementedTaskPercent = (totalTasks > 0) ? ((numberImplementedTasks * 100) / totalTasks) + "%" : "0%",
                        //NotImplementedTaskPercent = (totalTasks > 0) ? ((numberNotImplementedTasks * 100) / totalTasks) + "%" : "0%",
                        //StartedTaskPercent = (totalTasks > 0) ? ((numberStartedTasks * 100) / totalTasks) + "%" : "0%",
                        //PendingTaskPercent = (totalTasks > 0) ? ((numberPendingTasks * 100) / totalTasks) + "%" : "0%",
                        //CountImplementedTasks = numberImplementedTasks,
                        //CountNotImplementedTasks = numberNotImplementedTasks,
                        //CountPendingTasks = numberPendingTasks,
                        //CountStartedTasks = numberStartedTasks,

                        TotalTasks = totalTasks
                    };
                    int aux = 0;
                    int max = 0;

                    foreach (var allow in _data)
                    {
                        aux = _taskCampaignDao.GetTaskCountByCampaignAndStatusStadi(allow.Name, campaign.Id, idAccount);
                        max = (aux > max) ? aux : max;
                        cvm.sectionCampaign.Add(new SectionCampaignDinamicViewModels
                        {
                            name = allow.Name,
                            total = aux,
                            percent = (totalTasks > 0) ? ((aux * 100) / totalTasks) + "%" : "0%",
                            color = allow
                        .color
                        });
                    }

                    itemResult.CampaignList.Add(cvm);
                }

            }

#if DEBUG
            myWatch.Stop();
            Debugger.Log(0, "Campañas", $"ms: {myWatch.ElapsedMilliseconds}");
#endif

            return ConfigurePagination(itemResult, pageNumber, pageSize, filterValues, countCampaigns);
        }
        #endregion
        public CampaignListViewModel GetPaginatedCampaigns(List<FilterValue> filterValues, int pageSize, int pageNumber, Guid idAccount, IDataProtector protector, Guid userid, Guid _typeuser)
        {
#if DEBUG
            var myWatch = new Stopwatch();
            myWatch.Start();
#endif

            var itemResult = new CampaignListViewModel();
            var campaigns = _campaignDao.GetPaginatedCampaignList(filterValues, pageSize, pageNumber, idAccount);
            var countCampaigns = _campaignDao.GetPaginatedCampaignCount(filterValues, pageSize, pageNumber, idAccount, _typeuser, userid);

            foreach (var campaign in campaigns)
            {
                var ts = campaign.EndDate - DateTime.Now;

                int numberNotImplementedTasks;
                int numberStartedTasks;
                int numberPendingTasks;
                int numberImplementedTasks;
                var totalTasks = GetCampaignStatistics(idAccount, campaign, out numberNotImplementedTasks, out numberStartedTasks, out numberPendingTasks, out numberImplementedTasks);

                var usercampaign = _userCanpaignDao.GetCampaignById(campaign.Id, userid);
                if (_typeuser.Equals(new Guid("30DB815C-8B82-47EE-9279-B28922BEB616")))
                {
                    if (usercampaign.Count > 0)
                    {
                        var cvm = new CampaignItemViewModel
                        {
                            EndDate = campaign.EndDate,
                            Id = protector.Protect(campaign.Id.ToString()),
                            Name = campaign.Name,
                            StartDate = campaign.StartDate,
                            RemainingDays = ts.Days,

                            ImplementedTaskPercent = (totalTasks > 0) ? ((numberImplementedTasks * 100) / totalTasks) + "%" : "0%",
                            NotImplementedTaskPercent = (totalTasks > 0) ? ((numberNotImplementedTasks * 100) / totalTasks) + "%" : "0%",
                            StartedTaskPercent = (totalTasks > 0) ? ((numberStartedTasks * 100) / totalTasks) + "%" : "0%",
                            PendingTaskPercent = (totalTasks > 0) ? ((numberPendingTasks * 100) / totalTasks) + "%" : "0%",
                            CountImplementedTasks = numberImplementedTasks,
                            CountNotImplementedTasks = numberNotImplementedTasks,
                            CountPendingTasks = numberPendingTasks,
                            CountStartedTasks = numberStartedTasks,
                            TotalTasks = totalTasks
                        };


                        itemResult.CampaignList.Add(cvm);
                    }
                }
                else
                {
                    var cvm = new CampaignItemViewModel
                    {
                        EndDate = campaign.EndDate,
                        Id = protector.Protect(campaign.Id.ToString()),
                        Name = campaign.Name,
                        StartDate = campaign.StartDate,
                        RemainingDays = ts.Days,

                        ImplementedTaskPercent = (totalTasks > 0) ? ((numberImplementedTasks * 100) / totalTasks) + "%" : "0%",
                        NotImplementedTaskPercent = (totalTasks > 0) ? ((numberNotImplementedTasks * 100) / totalTasks) + "%" : "0%",
                        StartedTaskPercent = (totalTasks > 0) ? ((numberStartedTasks * 100) / totalTasks) + "%" : "0%",
                        PendingTaskPercent = (totalTasks > 0) ? ((numberPendingTasks * 100) / totalTasks) + "%" : "0%",
                        CountImplementedTasks = numberImplementedTasks,
                        CountNotImplementedTasks = numberNotImplementedTasks,
                        CountPendingTasks = numberPendingTasks,
                        CountStartedTasks = numberStartedTasks,
                        TotalTasks = totalTasks
                    };


                    itemResult.CampaignList.Add(cvm);
                }

            }

#if DEBUG
            myWatch.Stop();
            Debugger.Log(0, "Campañas", $"ms: {myWatch.ElapsedMilliseconds}");
#endif

            return ConfigurePagination(itemResult, pageNumber, pageSize, filterValues, countCampaigns);
        }

        private int GetCampaignStatistics(Guid idAccount, Campaign campaign, out int numberNotImplementedTasks,
            out int numberStartedTasks, out int numberPendingTasks, out int numberImplementedTasks)
        {
            var taskInformation = _taskCampaignDao.GetTaskCountPerStatus(campaign.Id, idAccount).ToList();

            numberImplementedTasks = taskInformation.FirstOrDefault(t => t.StatusName == CTask.StatusImplemented)?.Count ??
                                         0;
            numberNotImplementedTasks =
                taskInformation.FirstOrDefault(t => t.StatusName == CTask.StatusNotImplemented)?.Count ?? 0;
            numberStartedTasks = taskInformation.FirstOrDefault(t => t.StatusName == CTask.StatusStarted)?.Count ?? 0;
            numberPendingTasks = taskInformation.FirstOrDefault(t => t.StatusName == CTask.StatusPending)?.Count ?? 0;

            var totalTasks = numberPendingTasks + numberImplementedTasks + numberNotImplementedTasks +
                         numberStartedTasks;
            return totalTasks;
        }

        public CampaignRegisterViewModel GetCampaign(Guid idCampaign, Guid idAccount)
        {
            var model = new CampaignRegisterViewModel();
            if (idCampaign != Guid.Empty)
            {
                var campaign = _campaignDao.GetOne(idCampaign, idAccount);
                campaign.CampaignServices = _campaignServicesDao.GetCampaignServicesByCampaign(idCampaign, idAccount);
                foreach (var campaignService in campaign.CampaignServices)
                {
                    campaignService.Service = _serviceDao.GetOne(campaignService.IdService, idAccount);
                }
                model = ConvertCampaign.ToCampaignRegisterViewModel(campaign, model);
            }

            return model;
        }

        #region administracion Rutas
        public IList<RouteBranchViewModel> GetActiveRoute( Guid idAccount)
        {
            IList<RouteBranchViewModel> model = new List <RouteBranchViewModel>();
                     model= _campaignServicesDao.GetActRoute(idAccount);

            return model;
        }

        public async Task<int> ChangeStatusRoute(Guid idAccount, string route)
        {

          int  model = await _campaignServicesDao.UpdateStatusRoute(idAccount, route);

            return model;
        }


        public IList<EncuestadorViewModel> GetRoute(Guid idaccount, string route)
        {

            var imei = _campaignServicesDao.GetIMEIRoute(route, idaccount);
            IList<string> encuestadores = new List<string>();
            string[] separadas;

            foreach (var person in imei) {
                if (person != null) { 
                string UniqueImei = person;
                separadas = UniqueImei.Split('-');
                for (int xi = 0; xi < separadas.Length; xi++)
                {

                    encuestadores.Add(separadas[xi]);
                }
                }
            }
            var data = _campaignServicesDao.GetIdPersonByDocumentAndTypeDocumentAndAccount(encuestadores, "IMEI", idaccount);

            IList<EncuestadorViewModel> _model = new List<EncuestadorViewModel>();

            foreach (var item in data) {
                _model.Add(new EncuestadorViewModel { Code = item.IMEI, Name = item.Name, Phone = item.Phone,Oficina=item.Oficina });
            }
            return _model;
        }
        public int deleteRoute(Guid idaccount, string route,string imei)
        {

            return _campaignServicesDao.UpdateRouteImei(imei, route, idaccount);


    
        }
        public int deleteCuenta(Guid idaccount, string type)
        {

            return _campaignServicesDao.UpdateRouteAccount( idaccount, type);



        }
        //public IList<Pollster> GetActiveEncuestadores(Guid idAccount)
        //{
        //    IList<Pollster> model = new List<Pollster>();
        //    model = _personDao.GetActiveIMEI(idAccount);

        //    return model;
        //}
        public IList<Pollster> GetEncuestadoresbyIMEI(Guid idAccount)
        {
            IList<Pollster> model = new List<Pollster>();
            model = _personDao.GetActiveIMEI(idAccount);

            return model;
        }
        public int SaveImei(Guid idaccount, string id, string route)
        {

            return _campaignServicesDao.AddRouteImei(id, route, idaccount);



        }
        #endregion

    }
}
