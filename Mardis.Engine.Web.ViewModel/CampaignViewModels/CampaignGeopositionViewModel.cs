using System;
using System.Collections.Generic;
using Mardis.Engine.Web.ViewModel.Paging;

namespace Mardis.Engine.Web.ViewModel.CampaignViewModels
{
    public class CampaignGeopositionViewModel : PaginatedList
    {
        public Guid IdCampaign { get; set; }

        public List<GeoPositionViewModel> LocationList { get; set; }

        public string CampaignName { get; set; }

        public CampaignGeopositionViewModel() : base("CampaignMap", "Campaign", "Geoposition")
        {
        }
    }
}
