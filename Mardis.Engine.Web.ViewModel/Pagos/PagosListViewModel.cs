using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.Web.ViewModel.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.Web.ViewModel.Pagos
{
   public class PagosListViewModel: PaginatedList
    {

        public PagosListViewModel() : base("PagosList", "Pagos", "Index")
        {
        }
        public List<CoreFilterDetail> FilterList { get; set; }

        public List<PagosItemViewModel> PagosList { get; set; } = new List<PagosItemViewModel>();
    }
}
