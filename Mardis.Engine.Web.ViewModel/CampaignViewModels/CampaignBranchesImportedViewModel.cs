using System;
using Mardis.Engine.Web.ViewModel.Paging;

namespace Mardis.Engine.Web.ViewModel.CampaignViewModels
{
    public class CampaignBranchesImportedViewModel : PaginatedList
    {
        public CampaignBranchesImportedViewModel() : base("SelectCampaign", "Campaign", "ImportBranches")
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string[,] Results { get; set; } = new string[0, 0];

        public int CountBranchesResult { get; set; }
    }
}
