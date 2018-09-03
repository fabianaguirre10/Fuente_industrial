using System.Collections.Generic;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;

namespace Mardis.Engine.Business.MardisCommon
{
    public class TypePersonBusiness 
    {
        readonly TypePersonDao _typePersonDao;

        public TypePersonBusiness(MardisContext mardisContext) 
        {
            _typePersonDao = new TypePersonDao(mardisContext);
        }

        public List<TypePerson> GetAllActiveTypePersons()
        {
            return _typePersonDao.GetAllActiveTypePersons();
        }
    }
}
