using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mardis.Engine.Web.ViewModel.CampaignViewModels;
using Mardis.Engine.Web.ViewModel.Paging;

namespace Mardis.Engine.Web.ViewModel.DashBoardViewModels
{
    public class DashBoardViewModel : PaginatedList
    {
        public DashBoardViewModel() : base("DashBoard", "Home", "DashBoard")
        {
        }

        [Required]
        [Display(Name = "Campaña")]
        public string IdCampaign { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CountImplementedTasks { get; set; }

        public int CountNotImplementedTasks { get; set; }

        public int CountStartedTasks { get; set; }

        public int CountPendingTasks { get; set; }

        public string PercentImplementedTasks { get; set; }

        public string PercentNotImplementedTasks { get; set; }

        public string PercentStartedTasks { get; set; }

        public string PercentPendingTasks { get; set; }

        public int RemainingDays { get; set; }

        public List<CampaignBranchesViewModel> BranchList { get; set; } = new List<CampaignBranchesViewModel>();

        public List<DashBoardMerchantViewModel> MerchantList { get; set; } = new List<DashBoardMerchantViewModel>();
    }
}
