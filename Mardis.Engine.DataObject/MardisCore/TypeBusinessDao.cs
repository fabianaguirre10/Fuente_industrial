using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class TypeBusinessDao : ADao
    {
        public TypeBusinessDao(MardisContext mardisContext) : base(mardisContext)
        {
        }
        
        public List<TypeBusiness> GetAllTypesBusinessByIdCustomer(Guid idCustomer, Guid idAccount)
        {
            return Context.TypeBusiness
                    .Where(tb => tb.IdCustomer == idCustomer
                                && tb.StatusRegister == CStatusRegister.Active &&
                                tb.IdAccount==idAccount)
                    .ToList();
        }

        public TypeBusiness GetOne(Guid id, Guid idAccount)
        {

            return Context.TypeBusiness
                                   .FirstOrDefault(tb => tb.Id == id && tb.IdAccount == idAccount);
        }
    }
}
