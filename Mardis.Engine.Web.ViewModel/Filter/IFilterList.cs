namespace Mardis.Engine.Web.ViewModel.Filter
{
    public interface IFilterList
    {
        string FilterName { get; }

        FilterPropertiesViewModel Properties { get; set; }
    }
}
