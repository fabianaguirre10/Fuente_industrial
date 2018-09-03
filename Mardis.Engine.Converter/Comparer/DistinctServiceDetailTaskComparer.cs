using System.Collections.Generic;
using Mardis.Engine.DataAccess.MardisCore;

namespace Mardis.Engine.Web.Libraries.Convert
{
    public class DistinctServiceDetailTaskComparer : IEqualityComparer<ServiceDetailTask>
    {
        public bool Equals(ServiceDetailTask x, ServiceDetailTask y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(ServiceDetailTask obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}