using System.Collections.Generic;
using Mardis.Engine.DataAccess.MardisCommon;

namespace Mardis.Engine.Web.ViewModel.Filter
{
    public class FilterPropertiesViewModel
    {
        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string CurrentFilter { get; set; }

        public string IdFilter { get; set; }

        public string Criteria { get; set; }

        public string Value { get; set; }

        public List<CoreFilterDetail> FilterList { get; set; } = new List<CoreFilterDetail>();

        public List<FilterValue> FilterValues { get; set; } = new List<FilterValue>();
    }
}