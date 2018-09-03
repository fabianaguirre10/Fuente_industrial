using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class StatusCampaignDao:ADao
    {
        public StatusCampaignDao(MardisContext mardisContext) : base(mardisContext)
        {
        }

        /// <summary>
        /// Obtiene todos los estados de las campañas
        /// </summary>
        /// <returns>Listado con todos los estados de las campañas</returns>
        public List<StatusCampaign> GetStatusCampaigns()
        {
            return Context.StatusCampaigns
                .Where(sc => sc.StatusRegister == CStatusRegister.Active)
                .ToList();
        }
    }
}
