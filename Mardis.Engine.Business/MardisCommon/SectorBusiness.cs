using System;
using System.Collections.Generic;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataObject.MardisCommon;

namespace Mardis.Engine.Business.MardisCommon
{
    /// <summary>
    /// Business de Sectores
    /// </summary>
    public class SectorBusiness
    {
        readonly SectorDao _sectorDao;

        public SectorBusiness(MardisContext mardisContext)
        {
            _sectorDao = new SectorDao(mardisContext);
        }

        /// <summary>
        /// Dame Sectores por Distritos
        /// </summary>
        /// <param name="idDistrict"></param>
        /// <returns></returns>
        public List<Sector> GetSectorByDistrict(Guid idDistrict)
        {
            return _sectorDao.GetSectorByDistrict(idDistrict);
        }
    }
}
