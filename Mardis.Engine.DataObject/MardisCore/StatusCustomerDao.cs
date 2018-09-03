using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class StatusCustomerDao : ADao
    {
        public StatusCustomerDao(MardisContext mardisContext) : base(mardisContext)
        {
        }

        public List<StatusCustomer> GetAllActive()
        {
            return Context.StatusCustomers
                .Where(s => s.StatusRegister == CStatusRegister.Active)
                .ToList();
        }
    }
}
