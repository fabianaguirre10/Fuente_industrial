using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCommon;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Web.ViewModel.Filter;
using Microsoft.EntityFrameworkCore;
using Mardis.Engine.Web.ViewModel.BranchViewModels;
using Mardis.Engine.Web.ViewModel.EquipmentViewModels;
using Mardis.Engine.Framework;



namespace Mardis.Engine.DataObject.MardisCore
{
  public  class DashboardDao : ADao
    {

        public DashboardDao(MardisContext mardisContext)
           : base(mardisContext)
        {
            CoreFilterDetailDao = new CoreFilterDetailDao(mardisContext);
            CoreFilterDao = new CoreFilterDao(mardisContext);
        }

        public Dashboard GetOne(Guid id) {


            return Context.Dashboards
                          .Where(x => x.idcampaign.Equals(id)).First();
        }

    }
}
