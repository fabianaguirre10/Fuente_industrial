using System;
using Mardis.Engine.Business.MardisCore;
using Mardis.Engine.DataAccess;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.Libraries.Security;
using Mardis.Engine.Web.Libraries.Services;
using Mardis.Engine.Web.Model;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;

namespace Mardis.Engine.Web.Controllers
{
    public class ServiceDetailController : AController<ServiceDetailController>
    {
        #region Variables y Constructores

        private readonly Guid _idAccount;
        private readonly ServiceDetailBusiness _serviceDetailBusiness;

        public ServiceDetailController(UserManager<ApplicationUser> userManager,
                                IHttpContextAccessor httpContextAccessor,
                                MardisContext mardisContext,
                                ILogger<ServiceDetailController> logger,
                                ILogger<ServicesFilterController> loggeFilter,
                                    IDataProtectionProvider protectorProvider,
                                    IMemoryCache memoryCache)
            : base(userManager, httpContextAccessor, mardisContext, logger)
        {
            ControllerName = CTask.Controller;
            TableName = CTask.TableName;
            _serviceDetailBusiness = new ServiceDetailBusiness(mardisContext);

            _idAccount = ApplicationUserCurrent.AccountId;
        }

        #endregion

        [HttpGet]
        public JsonResult GetServiceDetailPoll(Guid idTask, Guid idService)
        {
            var model = _serviceDetailBusiness.GetPollSections(idTask, idService, _idAccount);

            return Json(model);
        }

    }
}
