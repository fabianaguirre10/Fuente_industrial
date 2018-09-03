using System;
using System.Collections.Generic;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;

namespace Mardis.Engine.Business.MardisCore
{
    public class TypeFilterBusiness
    {
        readonly TypeFilterDao _typeFilterDao;

        public TypeFilterBusiness(MardisContext mardisContext) 
        {
            _typeFilterDao = new TypeFilterDao(mardisContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idFilterField"></param>
        /// <returns></returns>
        public List<TypeFilter> GetTypeFilterByField(Guid idFilterField) {
            return _typeFilterDao.GetTypeFilterByField(idFilterField);
        }
    }
}
