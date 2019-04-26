using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.Web.ViewModel.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.Web.ViewModel.BranchViewModels
{
    public class BranchListViewModelPago: PaginatedList
    {
        public BranchListViewModelPago() : base("BranchList", "Pagos", "Index")
        {
        }

        public List<CoreFilterDetail> FilterList { get; set; }

        public List<BranchItemViewModel> BranchList { get; set; } = new List<BranchItemViewModel>();
    }
}
