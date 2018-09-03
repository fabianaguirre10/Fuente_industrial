using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Mardis.Engine.Business.MardisCore;
using Mardis.Engine.Business.MardisSecurity;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.Libraries.Security;
using Mardis.Engine.Web.Libraries.Services;
using Mardis.Engine.Web.Libraries.Util;
using Mardis.Engine.Web.Model;
using Mardis.Engine.Web.ViewModel.TaskViewModels;
using Mardis.Engine.Web.ViewModel.EquipmentViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Mardis.Engine.Web.App_code;
using Microsoft.AspNetCore.Hosting;
using Mardis.Engine.DataObject.MardisCore;

namespace Mardis.Engine.Web.Controllers
{
    [Authorize]
    public class EquipmentController : AController<EquipmentController>
    {
        #region Variables y Constructores

        private readonly TaskCampaignBusiness _taskCampaignBusiness;
        private readonly EquipmentBusiness _equipmentBusiness;
        private readonly StatusTaskBusiness _statusTaskBusiness;
        private readonly TaskNotImplementedReasonBusiness _taskNotImplementedReasonBusiness;
        private readonly BranchImageBusiness _branchImageBusiness;
        private readonly ILogger<EquipmentController> _logger;
        private readonly Guid _idAccount;
        private readonly IDataProtector _protector;
        private readonly UserBusiness _userBusiness;
        private readonly Guid _userId;
        private readonly QuestionBusiness _questionBusiness;
        private readonly QuestionDetailBusiness _questionDetailBusiness;
        private readonly IMemoryCache _cache;
        private readonly ServiceBusiness _serviceBusiness;
        private readonly EquipamentImagesBusiness _equipamentImagesBusiness;
        // private readonly Equipament_TimeB  _equipamentImagesBusiness;
        private readonly Equipament_timeBusiness _equipament_timeBusiness;
        private IHostingEnvironment _Env;
        public EquipmentController(UserManager<ApplicationUser> userManager,
                                IHttpContextAccessor httpContextAccessor,
                                MardisContext mardisContext,
                                ILogger<EquipmentController> logger,
                                ILogger<ServicesFilterController> loggeFilter,
                                    IDataProtectionProvider protectorProvider,
                                    IMemoryCache memoryCache,
                                    RedisCache distributedCache, IHostingEnvironment envrnmt)
            : base(userManager, httpContextAccessor, mardisContext, logger)
        {
            _protector = protectorProvider.CreateProtector(GetType().FullName);
            _logger = logger;
            ControllerName = CTask.Controller;
            TableName = CTask.TableName;
            _taskCampaignBusiness = new TaskCampaignBusiness(mardisContext, distributedCache);
            _statusTaskBusiness = new StatusTaskBusiness(mardisContext, distributedCache);
            _taskNotImplementedReasonBusiness = new TaskNotImplementedReasonBusiness(mardisContext);
            _branchImageBusiness = new BranchImageBusiness(mardisContext);
            _userBusiness = new UserBusiness(mardisContext);
            _questionBusiness = new QuestionBusiness(mardisContext);
            _questionDetailBusiness = new QuestionDetailBusiness(mardisContext);
            _cache = memoryCache;
            _serviceBusiness = new ServiceBusiness(mardisContext);
            _equipmentBusiness = new EquipmentBusiness(mardisContext);
            _equipamentImagesBusiness = new EquipamentImagesBusiness(mardisContext);
            _equipament_timeBusiness = new Equipament_timeBusiness(mardisContext);
            _Env = envrnmt;
            if (ApplicationUserCurrent.UserId != null)
            {
                _userId = new Guid(ApplicationUserCurrent.UserId);
                Global.UserID = _userId;
                Global.AccountId = ApplicationUserCurrent.AccountId;
                Global.ProfileId = ApplicationUserCurrent.ProfileId;
                Global.PersonId = ApplicationUserCurrent.PersonId;
            }

            _idAccount = ApplicationUserCurrent.AccountId;
        }
        #endregion

        [Authorize]
        // GET: Equipment
        public IActionResult Index(string filterValues, bool deleteFilter, int pageSize = 15, int pageIndex = 1)
        {
            try
            {
                var filters = GetFilters(filterValues, deleteFilter);

                var equipments = _equipmentBusiness.GetPaginatedEquipments(filters, pageSize, pageIndex, ApplicationUserCurrent.AccountId);

                return View(equipments);
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(0, "Error Index"), e.Message);
                return RedirectToAction("Index", "StatusCode", new { statusCode = 1 });
            }
        }
        public IActionResult Register(int idEQ, string returnUrl = null)
        {
            try
            {

                ViewData["ReturnUrl"] = returnUrl;
                var model = idEQ != 0 ? _equipmentBusiness.GetEquipment(idEQ, ApplicationUserCurrent.AccountId) : null;
                if (model == null)
                {
                    model = new EquipmentRegisterViewModel();
                    model.CreationDate = DateTime.Now;
                }
                else
                {
                    //GetDataSelectOne();
                    //_SaveImages(35);
                }
                GetDataSelectOne();
                model.ReturnUrl = returnUrl;
               

                return View(model);
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(0, "Error Index"), e.Message);
                return RedirectToAction("Index", "StatusCode", new { statusCode = 1 });
            }
        }
        [HttpGet]
        public int _SaveImagesAsync(int code)
        {
            // var result = await Task.Run(() => {
            //var equipos = _equipmentBusiness.GetEquipamentFotos(code);

            //foreach (var mod in equipos)
            //{
                var model = code != 0 ? _equipmentBusiness.GetEquipment(code, ApplicationUserCurrent.AccountId) : null;
                var equipmenturi = _equipament_timeBusiness.GetEquipamentTimeImages(model.Id);
                foreach (var eu in equipmenturi)
                {
                    var FotosEqu = _equipament_timeBusiness.GetEquipamentidtype(eu.Idequipament, Convert.ToInt32(eu.idstatus));
                    if (FotosEqu.Count == 0)
                    {
                        string sql = $@"select _uri Uri, _TOP_LEVEL_AURI, [Table], orden, valor from [dbo].[vw_FotosTopsy_P]  where _TOP_LEVEL_AURI = '{eu.aggregateuri}'";
                        var images = _equipamentImagesBusiness.GetImageEquipamentusri(sql);
                        MemoryStream imageStream = null;
                        foreach (var i in images)
                        {

                            imageStream = new MemoryStream(i.valor.ToArray());
                            AzureStorageUtil.UploadFromStream(imageStream, "implementacion", i.Uri + ".jpg").Wait();
                            var uri = AzureStorageUtil.GetUriFromBlob("implementacion", i.Uri + ".jpg");
                            var equipamentImages = new EquipamentImages()
                            {
                                IdEquipament = model.Id,
                                NameContainer = i.Uri,
                                NameFile = i.Uri + ".jpg",
                                UrlImage = uri,
                                IdAccount = Global.AccountId,
                                ORDER = i.orden,
                                ContentType = eu.idstatus.ToString()

                            };
                            _equipamentImagesBusiness.InsertImageEquipament(equipamentImages);

                        }
                    }
                }
            //}
            return 1;
           /* });
            return result;*/

        }
        [HttpPost]
        public ActionResult Register(EquipmentRegisterViewModel models, string returnUrl = null)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    GetDataSelectOne();
                    return View(models);
                }
                _equipmentBusiness.SaveEquipment(models, ApplicationUserCurrent.AccountId);
                if (!string.IsNullOrEmpty(models.ReturnUrl))
                {
                    return Redirect(models.ReturnUrl);
                }
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(0, "Error Index"), e.Message);
                return RedirectToAction("Index", "StatusCode", new { statusCode = 1 });
            }
        }

        public IActionResult Delete(int idEQ, string returnUrl = null)
        {
            _equipmentBusiness.DeleteEquipment(idEQ);
            return RedirectToAction("Index");

        }
        #region mmetodos del Controlador
        private void GetDataSelectOne()
        {
            ViewBag.TypeEq =
                _equipmentBusiness.GetUserListByType()
                    .Select(c => new SelectListItem() { Text = c.Description, Value = c.Id.ToString() })
                    .ToList();

            ViewBag.Estado =
             _equipmentBusiness.GetUserListBystatus()
                 .Select(c => new SelectListItem() { Text = c.Description, Value = c.Id.ToString() })
                 .ToList();
        }
        #endregion
        #region equipos Visualizador

        public IActionResult ViewEquipment(int idEQ, string returnUrl = null)
        {
            try
            {

                ViewData[CTask.IdEquipment] = idEQ.ToString();


                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(0, "Error Index"), e.Message);
                return RedirectToAction("Index", "StatusCode", new { statusCode = 1 });
            }
        }


        [HttpGet]
        public JsonResult Get(int IdEq)
        {
            try
            {
                var model = IdEq != 0 ? _equipmentBusiness.GetEquipment_Profile(IdEq, ApplicationUserCurrent.AccountId) : null;
              
                JSonConvertUtil.Convert(model);
                if (model != null)
                {
                   // _SaveImagesAsync(IdEq);
                }
                return Json(model);  
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(0, "Error Index"), e.Message);
                return null;
            }
        }
        #endregion


    }
}