using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mardis.Engine.Business.MardisCore
{
    public class TypeServiceBusiness
    {
        readonly TypeServiceDao _typeServiceDao;

        public TypeServiceBusiness(MardisContext mardisContext)
        {
            _typeServiceDao = new TypeServiceDao(mardisContext);
        }

        /// <summary>
        /// Dame Tipo de Servicios
        /// </summary>
        /// <returns></returns>
        public List<TypeService> GetAll()
        {
            return _typeServiceDao.GetAll();
        }

        public List<SelectListItem> GetTypeBusinessList()
        {
            return GetAll()
                 .Select(t => new SelectListItem() { Text = t.Name, Value = t.Id.ToString() })
                 .ToList();
        }
    }
}
