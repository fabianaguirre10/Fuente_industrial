using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.Web.ViewModel.Paging;

namespace Mardis.Engine.Web.ViewModel.EquipmentViewModels
{
    public class EquipmentListViewModel : PaginatedList

    {

        public EquipmentListViewModel() : base("EquipmentList", "Equipment", "Index")
        {
        }
        public List<CoreFilterDetail> FilterList { get; set; }

        public List<EquipmentItemViewModel> EquipmentList { get; set; } = new List<EquipmentItemViewModel>();
    }
}
