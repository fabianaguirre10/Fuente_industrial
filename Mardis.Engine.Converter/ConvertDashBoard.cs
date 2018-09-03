using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataAccess.MardisSecurity;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.ViewModel;
using Mardis.Engine.Web.ViewModel.DashBoardViewModels;
using System;
using AutoMapper;
using Mardis.Engine.Web.ViewModel.CampaignViewModels;

namespace Mardis.Engine.Converter
{
    public class ConvertDashBoard
    {
        [Obsolete]
        public static List<DashBoardMerchantViewModel> ConvertUserListToDashBoardMerchantViewModelList(
            List<User> userList)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<User, DashBoardMerchantViewModel>()
                .ForMember(dest => dest.TaskCount, opt => opt.MapFrom(src => src.TaskCampaigns.Count))
                .ForMember(dest => dest.PercentPendingTasks, opt => opt.MapFrom(src => ((src.TaskCampaigns.Count(t => t.StatusTask.Name == CTask.StatusPending) * 100) / src.TaskCampaigns.Count) + "%"))
                .ForMember(dest => dest.PercentNotImplementedTasks, opt => opt.MapFrom(src => ((src.TaskCampaigns.Count(t => t.StatusTask.Name == CTask.StatusNotImplemented) * 100) / src.TaskCampaigns.Count) + "%"))
                .ForMember(dest => dest.PercentStartedTasks, opt => opt.MapFrom(src => ((src.TaskCampaigns.Count(t => t.StatusTask.Name == CTask.StatusStarted) * 100) / src.TaskCampaigns.Count) + "%"))
                .ForMember(dest => dest.PercentImplementedTasks, opt => opt.MapFrom(src => ((src.TaskCampaigns.Count(t => t.StatusTask.Name == CTask.StatusImplemented) * 100) / src.TaskCampaigns.Count) + "%"))
                );

            return Mapper.Map<List<User>, List<DashBoardMerchantViewModel>>(userList);
        }

        [Obsolete]
        public static List<CampaignSelectViewModel> ConvertCampaignListToSelectViewModelList(List<Campaign> campaignList)
        {
            return campaignList
                .Select(c =>
                    new CampaignSelectViewModel()
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                .ToList();
        }

        public static DashBoardViewModel FromCampaign(DashBoardViewModel model,Campaign campaign, CampaignTaskDetailViewModel campaignDetails)
        {
            var totalTasks = 0;

            model.IdCampaign = campaign.Id.ToString();
            model.Name = campaign.Name;
            model.CountImplementedTasks = campaignDetails.CountImplementedTasks;
            model.CountNotImplementedTasks = campaignDetails.CountNotImplementedTasks;
            model.CountPendingTasks = campaignDetails.CountPendingTasks;
            model.CountStartedTasks = campaignDetails.CountStartedTasks;
            model.RemainingDays = campaignDetails.RemainingDays;

            totalTasks = model.CountImplementedTasks + model.CountNotImplementedTasks + model.CountPendingTasks +
                             model.CountStartedTasks;

            model.PercentImplementedTasks = Math.Ceiling((double)((campaignDetails.CountImplementedTasks * 100) / totalTasks)) + "%";
            model.PercentNotImplementedTasks = Math.Ceiling((double)((campaignDetails.CountNotImplementedTasks * 100) / totalTasks)) + "%";
            model.PercentPendingTasks = Math.Ceiling((double)((campaignDetails.CountPendingTasks * 100) / totalTasks)) + "%";

            //model.PercentStartedTasks = Math.Ceiling((double)((campaignDetails.CountStartedTasks * 100) / totalTasks)) + "%";

            model.PercentStartedTasks = (100 -
                                        Math.Ceiling(
                                            (double) ((campaignDetails.CountImplementedTasks * 100) / totalTasks)) -
                                        Math.Ceiling(
                                            (double) ((campaignDetails.CountNotImplementedTasks * 100) / totalTasks)) -
                                        Math.Ceiling((double) ((campaignDetails.CountPendingTasks * 100) / totalTasks)))+"%";

            model.StartDate = campaign.StartDate;
            model.EndDate = campaign.EndDate;

            return model;
        }

    }
}
