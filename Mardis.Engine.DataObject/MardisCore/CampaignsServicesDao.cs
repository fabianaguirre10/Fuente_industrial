using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class CampaignsServicesDao : ADao
    {
        private readonly ServiceDao _serviceDao;
        public CampaignsServicesDao(MardisContext mardisContext) : base(mardisContext)
        {
            _serviceDao = new ServiceDao(mardisContext);
        }
        
        public List<CampaignServices> GetCampaignsServicesByCampaign(Guid idCampaign)
        {
            var itemsRetun = Context.CampaignsServices
                                    .Where(cs => cs.IdCampaign == idCampaign &&
                                                 cs.StatusRegister == CStatusRegister.Active)
                                    .ToList();

            foreach (var itemTemp in itemsRetun)
            {
                itemTemp.Service = _serviceDao.GetOne(itemTemp.IdService, itemTemp.IdAccount);
            }

            return itemsRetun;
        }
    }
}
