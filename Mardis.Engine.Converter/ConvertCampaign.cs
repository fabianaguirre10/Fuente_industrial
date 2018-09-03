using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Web.ViewModel.CampaignViewModels;

namespace Mardis.Engine.Converter
{
    public class ConvertCampaign
    {
        public static Campaign FromCampaignRegisterViewModel(CampaignRegisterViewModel model)
        {
            return new Campaign()
            {
                Id = model.Id,
                IdChannel = model.IdChannel,
                IdCustomer = model.IdCustomer,
                IdStatusCampaign = model.IdStatusCampaign,
                IdSupervisor = model.IdSupervisor,
                Name = model.Name,
                Code = model.Code,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                CreationDate = model.CreationDate,
                RangeDate = model.RangeDate,
                StatusRegister = CStatusRegister.Active,
                Comment = model.Comment
            };
        }

        public static CampaignRegisterViewModel ToCampaignRegisterViewModel(Campaign campaign, CampaignRegisterViewModel model)
        {
            model.Name = campaign.Name;
            model.Code = campaign.Code;
            model.StartDate = campaign.StartDate;
            model.EndDate = campaign.EndDate;
            model.CreationDate = campaign.CreationDate;
            model.RangeDate = campaign.RangeDate;
            model.Id = campaign.Id;
            model.IdStatusCampaign = campaign.IdStatusCampaign;
            model.IdCustomer = campaign.IdCustomer;
            model.IdChannel = campaign.IdChannel;
            model.IdSupervisor = campaign.IdSupervisor;
            model.Comment = campaign.Comment;
            model.ServiceList=new List<Service>();

            foreach (var cService in campaign.CampaignServices)
            {
                model.ServiceList.Add(cService.Service);
            }

            model.NewServices = model.ServiceList.Aggregate(string.Empty, (x, y) => x + (";" + y.Id.ToString()));

            return model;
        }
    }
}
