using System;

namespace Mardis.Engine.Web.ViewModel.TaskViewModels
{
    public class MyTaskItemViewModel
    {
        public Guid Id { get; set; }

        public DateTime StartDate { get; set; }

        public string CampaignName { get; set; }

        public string BranchName { get; set; }

        public Guid BranchId { get; set; }

        public string BranchExternalCode { get; set; }

        public string BranchMardisCode { get; set; }

        public string Route { get; set; }

        public string Code { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }    
    }
}
