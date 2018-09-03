using System;
using System.Collections.Generic;

namespace Mardis.Engine.Web.ViewModel.CampaignViewModels
{
    public class CampaignItemViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ImplementedTaskPercent { get; set; }

        public string NotImplementedTaskPercent { get; set; }

        public string StartedTaskPercent { get; set; }

        public string PendingTaskPercent { get; set; }

        public int TotalTasks { get; set; }

        public int CountPendingTasks { get; set; }

        public int CountImplementedTasks { get; set; }

        public int CountNotImplementedTasks { get; set; }

        public int CountStartedTasks { get; set; }

        public int RemainingDays { get; set; }

        public List<SectionCampaignDinamicViewModels> sectionCampaign { get; set; } =  new List<SectionCampaignDinamicViewModels>();
    }
}
