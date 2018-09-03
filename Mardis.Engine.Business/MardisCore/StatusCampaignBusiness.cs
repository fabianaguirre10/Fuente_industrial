using System.Collections.Generic;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;
using Microsoft.Extensions.Caching.Memory;

namespace Mardis.Engine.Business.MardisCore
{
    public class StatusCampaignBusiness
    {
        private readonly IMemoryCache _myCache;
        private const string CacheName="StatusCampaign";

        public StatusCampaignBusiness(MardisContext mardisContext, IMemoryCache memoryCache)
        {
            var statusCampaignDao = new StatusCampaignDao(mardisContext);
            _myCache = memoryCache;
            if (_myCache.Get(CacheName) == null)
            {
                _myCache.Set(CacheName, statusCampaignDao.GetStatusCampaigns());
            }
        }

        /// <summary>
        /// Obtiene todos los estados de las campañas
        /// </summary>
        /// <returns>Listado con todos los estados de las campañas</returns>
        public List<StatusCampaign> GetStatusCampaigns()
        {
            return _myCache.Get<List<StatusCampaign>>(CacheName);
        }
    }
}
