using System.Collections.Generic;
using Mardis.Engine.Business.MardisCommon;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Web.Libraries.Security;
using Mardis.Engine.Web.Libraries.Services;
using Mardis.Engine.Web.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mardis.Engine.Web.Controllers
{
    public class TypePersonController : AController<TypePersonController>
    {
        readonly TypePersonBusiness _typePersonBusiness;

        public TypePersonController(UserManager<ApplicationUser> userManager, 
                                    IHttpContextAccessor httpContextAccessor, 
                                    MardisContext mardisContext, 
                                    ILogger<TypePersonController> logger,
                                    ILogger<ServicesFilterController> loggeFilter) : base(userManager, httpContextAccessor, mardisContext, logger)
        {
            _typePersonBusiness = new TypePersonBusiness(mardisContext);
        }

        [HttpGet]
        public List<TypePerson> GetAllActiveTypesPerson()
        {
            return _typePersonBusiness.GetAllActiveTypePersons();
        }
    }
}
