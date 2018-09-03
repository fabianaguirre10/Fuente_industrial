using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataObject.MardisCommon;

namespace Mardis.Engine.Business.MardisCommon
{
    public class MapBusiness
    {
        readonly MapDao _mapDao;

        public MapBusiness(MardisContext mardisContext)
        {
            _mapDao = new MapDao(mardisContext);
        }

        public List<Map> GetCodigoReservado(Guid idaccount)
        {
            return _mapDao.GetMap(idaccount);
        }
    }
}
