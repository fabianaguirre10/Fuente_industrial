using System.Collections.Generic;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.Web.ViewModel.Paging;

namespace Mardis.Engine.Web.ViewModel.CustomerViewModels
{
    public class CustomerListViewModel : PaginatedList
    {
        public CustomerListViewModel() : base("CustomerList", "Customer", "Index")
        {
        }

        public List<CoreFilterDetail> FilterList { get; set; }

        public List<CustomerItemViewModel> ItemsList { get; set; } = new List<CustomerItemViewModel>();
    }
}
