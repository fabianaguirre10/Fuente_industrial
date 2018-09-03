using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Mardis.Engine.Converter;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.ViewModel;
using Mardis.Engine.Web.ViewModel.CampaignViewModels;
using Mardis.Engine.Web.ViewModel.Filter;
using Mardis.Engine.Web.ViewModel.TaskViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Mardis.Engine.Business.MardisCore
{
    public class UserCanpaignBusiness : ABusiness
    {
        private readonly UserCanpaignDao _userCanpaignDao;

        public UserCanpaignBusiness(MardisContext mardisContext) : base(mardisContext)
        {
            _userCanpaignDao = new UserCanpaignDao(mardisContext);

        }

        public List<UserCanpaign> GetUserCampaignById(Guid idCampaign, Guid idUser)
        {
            return _userCanpaignDao.GetCampaignById(idCampaign, idUser);
        }

    }
}
