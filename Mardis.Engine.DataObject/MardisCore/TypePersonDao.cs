using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class TypePersonDao : ADao
    {
        public TypePersonDao(MardisContext mardisContext) : base(mardisContext)
        {
        }

        public List<TypePerson> GetAllActiveTypePersons()
        {
            return Context.TypesPerson.
                Where(tp => tp.StatusRegister == CStatusRegister.Active)
                .ToList();
        }
    }
}
