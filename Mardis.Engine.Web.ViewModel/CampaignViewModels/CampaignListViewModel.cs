using System.Collections.Generic;
using Mardis.Engine.Web.ViewModel.Paging;

namespace Mardis.Engine.Web.ViewModel.CampaignViewModels
{
    public class CampaignListViewModel : PaginatedList
    {
        public CampaignListViewModel() : base("CampaignList", "Campaign", "Index")
        {
        }

        public List<CampaignItemViewModel> CampaignList { get; set; } = new List<CampaignItemViewModel>();
    }
}
