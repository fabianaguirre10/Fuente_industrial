using System.Collections.Generic;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.Web.ViewModel.Paging;

namespace Mardis.Engine.Web.ViewModel.BranchViewModels
{
    public class BranchListViewModel : PaginatedList
    {
        public BranchListViewModel() : base("BranchList", "Branch", "Index")
        {
        }

        public List<CoreFilterDetail> FilterList { get; set; }

        public List<BranchItemViewModel> BranchList { get; set; } = new List<BranchItemViewModel>();
    }
}
