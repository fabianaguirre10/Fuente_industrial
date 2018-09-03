using Mardis.Engine.Web.ViewModel.Filter;

namespace Mardis.Engine.Web.ViewModel.Paging
{
    public class PaginatedList : IPaging, IFilterList
    {
        public PaginatedList(string filterName, string controllerName, string actionName)
        {
            Properties = new FilterPropertiesViewModel();
            FilterName = filterName;
            Properties.ControllerName = controllerName;
            Properties.ActionName = actionName;
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public bool HasPreviousPage => (PageIndex > 1);

        public bool HasNextPage => (PageIndex < TotalPages);

        public string FilterName { get; }

        public FilterPropertiesViewModel Properties { get; set; }

    }
}
