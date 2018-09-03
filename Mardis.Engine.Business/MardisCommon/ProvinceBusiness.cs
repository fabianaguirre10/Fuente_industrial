using System;
using System.Collections.Generic;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataObject.MardisCommon;
using Microsoft.Extensions.Caching.Memory;

namespace Mardis.Engine.Business.MardisCommon
{
    public class ProvinceBusiness
    {
        private readonly ProvinceDao _provinceDao;
        private const string CacheName = "Province";

        public ProvinceBusiness(MardisContext mardisContext, IMemoryCache memoryCache) 
        {
            _provinceDao = new ProvinceDao(mardisContext);
            var myCache = memoryCache;
            if (myCache.Get(CacheName) == null)
            {
                myCache.Set(CacheName, _provinceDao.GetAll());
            }
        }

        public ProvinceBusiness()
        {
        }

        /// <summary>
        /// Obtiene todas las provincias de un Pais dado
        /// </summary>
        /// <param name="idCountry"> Id del Pais </param>
        /// <returns></returns>
        public List<Province> GetProvincesByCountry(Guid idCountry)
        {
            return _provinceDao.GetProvincesByCountry(idCountry);
        }
    }
}
