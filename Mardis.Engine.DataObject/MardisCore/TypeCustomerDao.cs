using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class TypeCustomerDao : ADao
    {
        public TypeCustomerDao(MardisContext mardisContext) : base(mardisContext)
        {
        }

        public List<TypeCustomer> GetAllActiveList()
        {
            return Context.TypesCustomers
                .Where(t => t.StatusRegister == CStatusRegister.Active)
                .ToList();
        }
    }
}
