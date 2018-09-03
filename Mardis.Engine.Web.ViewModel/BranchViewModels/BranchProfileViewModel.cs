using System;
using System.Collections.Generic;
using Mardis.Engine.DataAccess.MardisCore;

namespace Mardis.Engine.Web.ViewModel.BranchViewModels
{
    public class BranchProfileViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string OwnerName { get; set; }

        public string DistrictName { get; set; }

        public string Zone { get; set; }

        public string ParishName { get; set; }

        public string Neighborhood { get; set; }

        public string Direction { get; set; }

        public string Reference { get; set; }

        public string OwnerPhone { get; set; }

        public string OwnerMobile { get; set; }

        public string BranchTypeBusiness { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }
        public List<BranchImagesProfileViewModel> BranchImages { get; set; }

        public List<BranchTaskCampaignsProfileViewModel> TaskCampaigns { get; set; } = new List<BranchTaskCampaignsProfileViewModel>();
        public List<Sms> SmsListas { get; set; }= new List<Sms>();

        
    }
}
