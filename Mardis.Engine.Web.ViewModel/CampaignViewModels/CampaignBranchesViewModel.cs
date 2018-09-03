using System;

namespace Mardis.Engine.Web.ViewModel.CampaignViewModels
{
    public class CampaignBranchesViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Status { get; set; }

        public DateTime Date { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public string Implemented { get; set; }

        public string NotImplemented { get; set; }

        public string Started { get; set; }

        public string Pending { get; set; }
    }
}
