using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataObject.MardisCommon;
using Mardis.Engine.Framework;
using Mardis.Engine.Web.ViewModel.Filter;
using Mardis.Engine.Web.ViewModel.Paging;
using StackExchange.Redis;

namespace Mardis.Engine.Business
{
    public abstract class ABusiness
    {

        protected CoreFilterDetailDao CoreFilterDetailDao;
        protected CoreFilterDao CoreFilterDao;

        public MardisContext Context { get; }

        protected ABusiness(MardisContext mardisContext)
        {
            Context = mardisContext;
            CoreFilterDao = new CoreFilterDao(mardisContext);
            CoreFilterDetailDao = new CoreFilterDetailDao(mardisContext);
        }

        protected T ConfigurePagination<T>(T entity, int pageIndex, int pageSize, List<FilterValue> filterValues, int countTotal) where T : PaginatedList
        {
            entity.PageIndex = pageIndex;
            entity.TotalPages = (int)Math.Ceiling(countTotal / (double)pageSize);
            entity.PageSize = pageSize;
            entity.Properties.FilterValues = filterValues ?? new List<FilterValue>();
            entity.Properties.CurrentFilter = JSonConvertUtil.Convert(filterValues);

            var filterResult = CoreFilterDao.GetCoreFilter(entity.FilterName);

            if (filterResult != null)
            {
                entity.Properties.FilterList = CoreFilterDetailDao.GetCoreFilterDetails(filterResult.Id);
            }

            return entity;
        }

        protected List<FilterValue> AddHiddenFilter(string key, string value, List<FilterValue> filters, string filterName)
        {
            var filter = CoreFilterDao.GetCoreFilter(filterName);

            if (filter != null)
            {
                var appFilter = CoreFilterDetailDao.GetCoreFilterDetail(filter.Id, key);

                if (!filters.Where(filterValue => filterValue.IdFilter == appFilter.Id).ToList().Any())
                {
                    filters.Add(new FilterValue
                    {
                        Value = value,
                        Criteria = "==",
                        IdFilter = appFilter.Id,
                        NameFilter = key
                    });
                }
            }

            return filters;
        }
    }
}
