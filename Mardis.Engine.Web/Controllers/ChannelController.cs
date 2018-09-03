using System;
using System.Collections.Generic;
using Mardis.Engine.Business.MardisCore;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Web.Libraries.Security;
using Mardis.Engine.Web.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mardis.Engine.Web.Controllers
{
    [Authorize]
    public class ChannelController:AController<ChannelController>
    {

        private readonly ChannelBusiness _channelBusiness;

        public ChannelController(
                        UserManager<ApplicationUser> userManager, 
                        IHttpContextAccessor httpContextAccessor, 
                        MardisContext mardisContext, 
                        ILogger<ChannelController> logger) 
            : base(userManager, httpContextAccessor, mardisContext, logger)
        {
            _channelBusiness = new ChannelBusiness(mardisContext);
        }

        [HttpGet]
        public List<Channel> GetAllChannelsByCustomerId(Guid idCustomer)
        {
            return _channelBusiness.GetChannelsByCustomerId(idCustomer,ApplicationUserCurrent.AccountId);
        }
    }
}
