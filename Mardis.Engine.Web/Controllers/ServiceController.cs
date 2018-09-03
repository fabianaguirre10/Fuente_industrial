using System;
using System.Collections.Generic;
using System.IO;
using Mardis.Engine.Business;
using Mardis.Engine.Business.MardisCore;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.Libraries.Security;
using Mardis.Engine.Web.Libraries.Services;
using Mardis.Engine.Web.Model;
using Mardis.Engine.Web.ViewModel.ServiceViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Mardis.Engine.Web.Controllers
{
    [Authorize]
    public class ServiceController : AController<ServiceController>
    {
        #region VARIABLES & CONSTRUCTORES

        private readonly TypeServiceBusiness _typeServiceBusiness;
        private readonly CustomerBusiness _customerBusiness;
        private readonly ServiceBusiness _serviceBusiness;
        private readonly TypePollBusiness _typePollBusiness;
        private readonly IDataProtector _protector;
        private readonly ChannelBusiness _channelBusiness;
        private readonly QuestionBusiness _questionBusiness;
        private IHostingEnvironment _Env;
        public ServiceController(
                                UserManager<ApplicationUser> userManager,
                                IHttpContextAccessor httpContextAccessor,
                                MardisContext mardisContext,
                                ILogger<ServiceController> logger,
                                ILogger<ServicesFilterController> loggerFilter,
                                IDataProtectionProvider protectorProvider,
                                IHostingEnvironment envrnmt)
            : base(userManager, httpContextAccessor, mardisContext, logger)
        {
            _typeServiceBusiness = new TypeServiceBusiness(mardisContext);
            _customerBusiness = new CustomerBusiness(mardisContext);
            _serviceBusiness = new ServiceBusiness(mardisContext);
            _typePollBusiness = new TypePollBusiness(mardisContext);
            _protector = protectorProvider.CreateProtector(GetType().FullName);
            _channelBusiness = new ChannelBusiness(mardisContext);
            _questionBusiness = new QuestionBusiness(mardisContext);
            _Env = envrnmt;
        }

        #endregion

        #region SECCIONES

        [HttpPost]
        public IActionResult AddSubSection(ServiceRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                LoadViewData(model.IdCustomer, model.Id.ToString());
                return View("Register", model);
            }

            model = _serviceBusiness.AddSubSection(model, ApplicationUserCurrent.AccountId);

            return RedirectToAction("Register", new { service = _protector.Protect(model.Id.ToString()) });
        }
        
        [HttpGet]
        public JsonResult AddSection()
        {
            var model = new ServiceDetailRegisterViewModel();

            return Json(model);
        }

        [HttpPost]
        public IActionResult DuplicateSection(ServiceRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                LoadViewData(model.IdCustomer, model.Id.ToString());
                return View("Register", model);
            }
            _serviceBusiness.DuplicateSection(model, ApplicationUserCurrent.AccountId);
            return RedirectToAction("Register", new { service = _protector.Protect(model.Id.ToString()) });
        }

        #endregion

        #region PREGUNTAS
        
        [HttpGet]
        public JsonResult AddQuestion()
        {
            var model = new QuestionRegisterViewModel();

            return Json(model);
        }

        #endregion

        #region RESPUESTAS

        [HttpGet]
        public JsonResult AddAnswer()
        {
            var model=new QuestionDetailRegisterViewModel();

            return Json(model);
        }

        [HttpPost]
        public IActionResult AddSubAnswer(ServiceRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                LoadViewData(model.IdCustomer, model.Id.ToString());
                return View("Register", model);
            }

            _serviceBusiness.AddSubAnswer(model, idAccount: ApplicationUserCurrent.AccountId);

            return RedirectToAction("Register", new { service = _protector.Protect(model.Id.ToString()) });
        }

        [HttpPost]
        public IActionResult AddAnswerAfter(ServiceRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                LoadViewData(model.IdCustomer, model.Id.ToString());
                return View("Register", model);
            }

            _serviceBusiness.AddAnswerAfter(model, ApplicationUserCurrent.AccountId);

            return RedirectToAction("Register", new { service = _protector.Protect(model.Id.ToString()) });
        }

        [HttpPost]
        public IActionResult DeleteAnswer(ServiceRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                LoadViewData(model.IdCustomer, model.Id.ToString());
                return View("Register", model);
            }

            _serviceBusiness.DeleteAnswer(model, ApplicationUserCurrent.AccountId);

            return RedirectToAction("Register", new { service = _protector.Protect(model.Id.ToString()) });
        }

        #endregion

        #region SERVICIOS

        [HttpGet]
        public IActionResult Index(string typeService, string customer)
        {
            var model = _serviceBusiness.GetIndexPageInformation(typeService, customer, _protector,
                ApplicationUserCurrent.AccountId);

            return View(model);
        }

        [HttpGet]
        public IActionResult Register(string service)
        {

            Service model = new Service();

            if (!string.IsNullOrEmpty(service))
            {
                model = _serviceBusiness.GetOne(Guid.Parse(_protector.Unprotect(service)),
                    ApplicationUserCurrent.AccountId);

            }

            LoadViewData(model.IdCustomer.ToString(),model.Id.ToString());

            ViewData[CService.IdRegister] = service;

            return View();
        }

        [HttpGet]
        public JsonResult Get(string idService)
        {
            var model = _serviceBusiness.GetService(idService, _protector, ApplicationUserCurrent.AccountId);

            LoadViewData(model.IdCustomer, model.Id.ToString());

            //SetSessionVariable("Sections", JSonConvertUtil.Convert(model.ServiceDetailList));

            return Json(model);
        }

        [HttpPost]
        public IActionResult Save(string service)
        {
            var model = JSonConvertUtil.Deserialize<ServiceRegisterViewModel>(service);

            TryValidateModel(model);

            if (!ModelState.IsValid)
            {
                return null;
            }
            _serviceBusiness.Save(model, ApplicationUserCurrent.AccountId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult Massive(IFormFile fileBranch)
        {
            try
            {
                DateTime localDate = DateTime.Now;
                if (fileBranch == null)
                {

                    ViewBag.error = "Verfique si el archivo fue cargado";
                    return Json("-1");
                }

                string LogFile = localDate.ToString("yyyyMMddHHmmss");
                var Filepath = _Env.WebRootPath + "\\form\\ " + LogFile + "_" + fileBranch.FileName.ToString();
                using (var fileStream = new FileStream(Filepath, FileMode.Create))
                {
                    fileBranch.CopyTo(fileStream);
                }

              var success=  _serviceBusiness.GenerateXml(Filepath, ApplicationUserCurrent.AccountId, "Encuesta Pruebas");
                return Json(success.ToString());
            }
            catch (Exception)
            {

                return Json("3");
            }
           
        }
        [HttpGet]
        public IActionResult xml(Guid idBranch, string returnUrl = null)
        {

            ViewData["ReturnUrl"] = returnUrl;
         
            ViewData[CBranch.IdRegister] = idBranch;

            return View();
        }
        [HttpGet]
        public IActionResult Delete(string service)
        {
            var idService = Guid.Parse(_protector.Unprotect(service));
            _serviceBusiness.DeleteService(idService, ApplicationUserCurrent.AccountId);
            return RedirectToAction("Index");
        }

        #endregion

        private void LoadViewData(string idCustomer, string idService)
        {
            ViewBag.Types = _typeServiceBusiness.GetTypeBusinessList();
            ViewBag.Customers = _customerBusiness.GetCustomerList(ApplicationUserCurrent.AccountId);
            ViewBag.TypePollList = _typePollBusiness.GetTypePollList();

            if (!string.IsNullOrEmpty(idService))
            {
                ViewBag.Questions = _questionBusiness.GetQuestionTolink(idService, ApplicationUserCurrent.AccountId, _protector);
            }

            if (!string.IsNullOrEmpty(idCustomer))
            {
                ViewBag.Channels = _channelBusiness.GetChanelListByCustomer(Guid.Parse(idCustomer),
                    ApplicationUserCurrent.AccountId);
            }
        }

        [HttpGet]
        public List<Service> GetServicesListByChannelId(Guid idChannel)
        {
            return _serviceBusiness.GetServicesByChannelId(ApplicationUserCurrent.AccountId, idChannel);
        }

    }
}
