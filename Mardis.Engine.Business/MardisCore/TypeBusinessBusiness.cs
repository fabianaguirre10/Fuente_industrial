using System;
using System.Collections.Generic;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;

namespace Mardis.Engine.Business.MardisCore
{
    public class TypeBusinessBusiness 
    {
        readonly TypeBusinessDao _typeBusinessDao;

        public TypeBusinessBusiness(MardisContext mardisContext) 
        {
            _typeBusinessDao = new TypeBusinessDao(mardisContext);
        }
        
        public List<TypeBusiness> GetAllTypesBusinessByIdCustomer(Guid idCustomer, Guid idAccount)
        {
            return _typeBusinessDao.GetAllTypesBusinessByIdCustomer(idCustomer,idAccount);
        }

        public TypeBusiness GetOne(Guid id, Guid idAccount)
        {
            return _typeBusinessDao.GetOne(id,idAccount);
        }
    }
}
