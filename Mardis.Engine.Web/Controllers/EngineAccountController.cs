using System.Linq;
using Mardis.Engine.Business.MardisCommon;
using Mardis.Engine.DataAccess;
using Mardis.Engine.Web.Libraries.Security;
using Mardis.Engine.Web.Model;
using Mardis.Engine.Web.ViewModel.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mardis.Engine.Web.Controllers
{
    [Authorize]
    public class EngineAccountController : AController<EngineAccountController>
    {

        private readonly AccountBusiness _accountBusiness;
        private readonly IDataProtector _protector;

        public EngineAccountController(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            MardisContext mardisContext,
            ILogger<EngineAccountController> logger,
            IDataProtectionProvider protectorProvider) :
            base(userManager, httpContextAccessor, mardisContext, logger)
        {
            _accountBusiness = new AccountBusiness(mardisContext);
            _protector = protectorProvider.CreateProtector(GetType().FullName);
        }

        public IActionResult Index(string filterValues, bool deleteFilter, int pageSize = 15, int pageIndex = 1)
        {
            var filters = GetFilters(filterValues, deleteFilter);
            var model = _accountBusiness.GetAccounts(_protector, filters, pageSize, pageIndex);
            return View(model);
        }

        public IActionResult Register(string account)
        {
            var model = _accountBusiness.GetAccount(_protector, account);
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(AccountRegisterViewModel model)
        {
            _accountBusiness.Save(model,_protector);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteAccount(AccountListViewModel model)
        {
            var ids = model.Accounts.Where(a => a.Selected).Select(a => a.Id).ToArray();
            return RedirectToAction("Index");
        }
    }
}
