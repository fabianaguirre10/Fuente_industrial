using System;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCommon;

namespace Mardis.Engine.DataObject.MardisCommon
{
    public class CoreFilterDao : ADao
    {
        public CoreFilterDao(MardisContext mardisContext) : base(mardisContext)
        {
        }

        public CoreFilter GetCoreFilter(Guid idCoreFilter)
        {
            return Context.CoreFilters
                .FirstOrDefault(f => f.Id == idCoreFilter);
        }

        public CoreFilter GetCoreFilter(string nameCoreFilter)
        {
            return Context.CoreFilters
                .FirstOrDefault(f => f.Name == nameCoreFilter);
        }
    }
}
