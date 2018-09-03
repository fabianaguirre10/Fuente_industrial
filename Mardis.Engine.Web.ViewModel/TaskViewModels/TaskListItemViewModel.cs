using System;

namespace Mardis.Engine.Web.ViewModel.TaskViewModels
{
    public class TaskListItemViewModel
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string CampaignName { get; set; }

        public Guid CampaignId { get; set; }

        public string BranchName { get; set; }

        public string BranchCode { get; set; }  

        public string StatusName { get; set; }

        public string MerchantName { get; set; }

        public string Route { get; set; }

        public DateTime StartDate { get; set; }
    }
}
