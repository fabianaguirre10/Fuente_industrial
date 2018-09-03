using System;
using System.Collections.Generic;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataObject.MardisCommon;

namespace Mardis.Engine.Business.MardisCommon
{
    public class DistrictBusiness 
    {
        private readonly DistrictDao _districtDao;
        public DistrictBusiness(MardisContext mardisContext) 
        {
            _districtDao = new DistrictDao(mardisContext);
        }

        /// <summary>
        /// Dame distritos por Provincia
        /// </summary>
        /// <param name="idProvince"></param>
        /// <returns></returns>
        public List<District> GetDistrictByProvince(Guid idProvince) {

            return _districtDao.GetDistrictByProvince(idProvince);
        }
        public List<District> GetDistrict()
        {
            return _districtDao.GetDistrinct();
        }
    }
}
