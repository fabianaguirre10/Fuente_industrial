using System;
using System.Collections.Generic;
using Mardis.Engine.Business;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.Libraries.Security;
using Mardis.Engine.Web.Libraries.Services;
using Mardis.Engine.Web.Model;
using Mardis.Engine.Web.ViewModel.CustomerViewModels;
using Mardis.Engine.Web.ViewModel.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mardis.Engine.Web.Controllers
{
    /// <summary>
    /// Controlador de Clientes
    /// </summary>
    [Authorize]
    public class CustomerController : AController<CustomerController>
    {
        private readonly CustomerBusiness _customerBusiness;

        public CustomerController(UserManager<ApplicationUser> userManager,
                                 IHttpContextAccessor httpContextAccessor,
                                 MardisContext mardisContext,
                                 ILogger<CustomerController> logger,
                                 ILogger<ServicesFilterController> loggeFilter)
            : base(userManager, httpContextAccessor, mardisContext, logger)
        {
            _customerBusiness = new CustomerBusiness(mardisContext);
            //ServicesFilterController = new ServicesFilterController(userManager, httpContextAccessor,
                                                                     //mardisContext, loggeFilter);
            TableName = CCustomer.TableName;
            ControllerName = CCustomer.Controller;
        }

        [HttpGet]
        public IActionResult Register(Guid idCustomer)
        {
            var model = _customerBusiness.GetNewCustomerRegisterViewModel();

            if (idCustomer != Guid.Empty)
            {
                model = _customerBusiness.GetCustomer(idCustomer, ApplicationUserCurrent.AccountId);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(CustomerRegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            _customerBusiness.Save(model, ApplicationUserCurrent.AccountId);
            return RedirectToAction("Index");
        }

        public IActionResult Index(string filterValues, int pageSize = 15, int pageIndex = 1)
        {
            var filters = JSonConvertUtil.Deserialize<List<FilterValue>>(filterValues);
            var viewModel = _customerBusiness.GetPaginatedCustomers(filters, pageSize, pageIndex, ApplicationUserCurrent.AccountId);
            return View(viewModel);
        }

        [HttpGet]
        public string GetOneCustomer(string id)
        {

            var itemReturn = new Customer();

            if (!String.IsNullOrEmpty(id))
            {
                itemReturn = _customerBusiness.GetOne(new Guid(id), ApplicationUserCurrent.AccountId);
            }
            else
            {
                itemReturn.DateCreation = DateTime.Now;
            }

            return JSonConvertUtil.Convert(itemReturn);
        }

        [HttpPost]
        public IActionResult AddChannel(CustomerRegisterViewModel model)
        {
            var customer = _customerBusiness.AddChannel(model, ApplicationUserCurrent.AccountId);
            return RedirectToAction("Register", new { idCustomer = customer.Id });
        }

        [HttpPost]
        public IActionResult DeleteChannel(CustomerRegisterViewModel model)
        {
            _customerBusiness.DeleteChannel(model, ApplicationUserCurrent.AccountId);
            return RedirectToAction("Register", new { idCustomer = model.Id });
        }

        [HttpPost]
        public IActionResult AddTypeBusiness(CustomerRegisterViewModel model)
        {
            var customer = _customerBusiness.AddTypeBusiness(model, ApplicationUserCurrent.AccountId);
            return RedirectToAction("Register", new { idCustomer = customer.Id });
        }

        [HttpPost]
        public IActionResult DeleteTypeBusiness(CustomerRegisterViewModel model)
        {
            _customerBusiness.DeleteTypeBusiness(model, ApplicationUserCurrent.AccountId);
            return RedirectToAction("Register", new { idCustomer = model.Id });
        }

        [HttpPost]
        public IActionResult AddProductCategory(CustomerRegisterViewModel model)
        {
            var customer = _customerBusiness.AddProductCategory(model, ApplicationUserCurrent.AccountId);
            return RedirectToAction("Register", new { idCustomer = customer.Id });
        }

        [HttpPost]
        public IActionResult DeleteProductCategory(CustomerRegisterViewModel model)
        {
            _customerBusiness.DeleteProductCategory(model, ApplicationUserCurrent.AccountId);
            return RedirectToAction("Register", new { idCustomer = model.Id });
        }
    }
}
