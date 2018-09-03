namespace Mardis.Engine.Web.ViewModel.Paging
{
    public interface IPaging
    {

        int PageIndex { get; set; }

        int PageSize { get; set; }

        int TotalPages { get; set; }

        bool HasPreviousPage { get; }

        bool HasNextPage { get; }

    }
}
