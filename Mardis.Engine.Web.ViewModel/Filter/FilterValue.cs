using System;

namespace Mardis.Engine.Web.ViewModel.Filter
{
    public class FilterValue
    {
        public Guid IdFilter { get; set; }

        public string NameFilter { get; set; }  

        public string Criteria { get; set; }

        public string Value { get; set; }

        public bool Visible { get; set; }
    }
}
