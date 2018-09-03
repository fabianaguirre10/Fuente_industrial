using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataObject.MardisCommon;
using Mardis.Engine.Framework.Resources;
using Microsoft.Extensions.Caching.Memory;

namespace Mardis.Engine.Business.MardisCommon
{
    public class CountryBusiness
    {
        private readonly CountryDao _countryDao;
        private readonly IMemoryCache _myCache;
        private const string CacheName = "Country";

        public CountryBusiness(MardisContext mardisContext, IMemoryCache memoryCache)
        {
            _myCache = memoryCache;
            _countryDao = new CountryDao(mardisContext);
            if (_myCache.Get(CacheName) == null)
            {
                _myCache.Set(CacheName, _countryDao.GetCountries());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Country> GetCountries()
        {
            return _myCache.Get<List<Country>>(CacheName);
        }

        /// <summary>
        /// Dame country por código
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Country GetCountryByCode(string code)
        {
            return _myCache.Get<List<Country>>(CacheName)
                .FirstOrDefault(tb => tb.Code == code &&
                                      tb.StatusRegister == CStatusRegister.Active);
        }

    }
}
