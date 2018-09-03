using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCommon;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.ViewModel;
using Mardis.Engine.Web.ViewModel.Filter;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class UserCanpaignDao : ADao
    {
        public UserCanpaignDao(MardisContext mardisContext) :
            base(mardisContext)
        {
            CoreFilterDao = new CoreFilterDao(mardisContext);
            CoreFilterDetailDao = new CoreFilterDetailDao(mardisContext);
        }

        public List<UserCanpaign> GetCampaignById(Guid idCampaign, Guid iduser)
        {
            var itemReturn =
                Context.UserCanpaign.Where(x => x.idCanpaign == idCampaign && x.idUser == iduser).ToList();

            return itemReturn;
        }
    }

}
