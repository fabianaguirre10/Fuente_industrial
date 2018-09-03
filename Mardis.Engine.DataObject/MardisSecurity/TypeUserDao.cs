using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisSecurity;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisSecurity
{
    public class TypeUserDao : ADao
    {
        public TypeUserDao(MardisContext mardisContext) : base(mardisContext)
        {
        }

        public List<TypeUser> GetAll()
        {
            return Context.TypeUsers
                .Where(t => t.StatusRegister == CStatusRegister.Active)
                .ToList();
        }
    }
}
