using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCommon;

namespace Mardis.Engine.DataObject.MardisCommon
{
    public class CoreFilterDetailDao : ADao
    {
        public CoreFilterDetailDao(MardisContext mardisContext) : base(mardisContext)
        {
        }

        public List<CoreFilterDetail> GetCoreFilterDetails(Guid idCoreFilter)
        {
            return Context.CoreFilterDetails
                .Where(d => d.IdCoreFilter == idCoreFilter &&
                            d.Visible)
                .ToList();
        }

        public CoreFilterDetail GetCoreFilterDetail(Guid idCoreFilterDetail)
        {
            return Context.CoreFilterDetails
                .FirstOrDefault(d => d.Id == idCoreFilterDetail);
        }

        public CoreFilterDetail GetCoreFilterDetail(Guid idCoreFilter, string property)
        {
            return Context.CoreFilterDetails
                .FirstOrDefault(d => d.Property == property &&
                d.IdCoreFilter == idCoreFilter);
        }

        public CoreFilterDetail GetCoreFilterDetail(Guid idCoreFilter, string property, string table)
        {
            return Context.CoreFilterDetails
                .FirstOrDefault(d => d.Property == property &&
                d.IdCoreFilter == idCoreFilter &&
                d.Table == table);
        }
    }
}
