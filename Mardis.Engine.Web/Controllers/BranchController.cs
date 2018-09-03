using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mardis.Engine.Business.MardisCommon;
using Mardis.Engine.Business.MardisCore;
using Mardis.Engine.Converter;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.Libraries.Security;
using Mardis.Engine.Web.Libraries.Services;
using Mardis.Engine.Web.Libraries.Util;
using Mardis.Engine.Web.Model;
using Mardis.Engine.Web.Util;
using Mardis.Engine.Web.ViewModel.BranchViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;

namespace Mardis.Engine.Web.Controllers
{

    [Authorize]
    public class BranchController : AController<BranchController>
    {
        #region Variables y Constructores

        private readonly BranchBusiness _branchBusiness;
        private readonly BranchImageBusiness _branchImageBusiness;
        private readonly TaskCampaignBusiness _taskCampaignBusiness;
        private readonly CountryBusiness _countryBusiness;
        private readonly ProvinceBusiness _provinceBusiness;
        private readonly DistrictBusiness _districtBusiness;
        private readonly ParishBusiness _parishBusiness;
        private readonly SectorBusiness _sectorBusiness;
        private readonly SmsBusiness _smsBusiness;
        private IMemoryCache _cache;

        public BranchController(UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            MardisContext mardisContext,
            ILogger<BranchController> logger,
            ILogger<ServicesFilterController> loggeFilter,
            IMemoryCache memoryCache,
            RedisCache distributedCache)
            : base(userManager, httpContextAccessor, mardisContext, logger)
        {
            TableName = CBranch.TableName;
            ControllerName = CBranch.Controller;
            _cache = memoryCache;
            _branchBusiness = new BranchBusiness(mardisContext);
            _branchImageBusiness = new BranchImageBusiness(mardisContext);
            _taskCampaignBusiness = new TaskCampaignBusiness(mardisContext, distributedCache);
            _countryBusiness = new CountryBusiness(mardisContext, memoryCache);
            _provinceBusiness = new ProvinceBusiness(mardisContext, memoryCache);
            _districtBusiness = new DistrictBusiness(mardisContext);
            _parishBusiness = new ParishBusiness(mardisContext);
            _sectorBusiness = new SectorBusiness(mardisContext);
            _smsBusiness = new SmsBusiness(mardisContext);
        }

        #endregion

        public IActionResult Profile(Guid idBranch)
        {
            var branch = _branchBusiness.GetBranchCompleteProfile(idBranch, ApplicationUserCurrent.AccountId);
            var smslista = _smsBusiness.GetCampaignByIdSms(ApplicationUserCurrent.AccountId);
            branch.BranchImages = _branchImageBusiness.GetBranchesImagesList(branch.Id, ApplicationUserCurrent.AccountId);

            var itemReturn = ConvertBranch.ConvertBranchToBranchProfileViewModel(branch,smslista);

            return View(itemReturn);
        }

        [HttpGet]
        public IActionResult Register(Guid idBranch, string returnUrl = null)
        {

            ViewData["ReturnUrl"] = returnUrl;
            var modelView = new BranchRegisterViewModel();

            if (idBranch != Guid.Empty)
            {
                modelView = _branchBusiness.GetBranchForRegister(idBranch, ApplicationUserCurrent.AccountId);
                GetLocationSelectTagHelper(modelView);
            }

            ViewData[CBranch.IdRegister] = idBranch;

            return View();
        }

        [HttpGet]
        public JsonResult Get(Guid idBranch, string returnUrl = null)
        {
            var model = _branchBusiness.GetBranchForRegister(idBranch, ApplicationUserCurrent.AccountId);

            return Json(model);
        }

        [HttpGet]
        public JsonResult AddCustomer()
        {
            var model = new BranchCustomersViewModel();

            return Json(model);
        }

        private void GetLocationSelectTagHelper(BranchRegisterViewModel model)
        {
            ViewBag.Countries =
                    _countryBusiness.GetCountries()
                        .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() })
                        .ToList();
            ViewBag.Provinces = _provinceBusiness.GetProvincesByCountry(Guid.Parse(model.IdCountry))
                 .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() })
                    .ToList();
            ViewBag.Districts = _districtBusiness.GetDistrictByProvince(Guid.Parse(model.IdProvince))
                 .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() })
                    .ToList();
            ViewBag.Parishes = _parishBusiness.GetParishByDistrict(Guid.Parse(model.IdDistrict))
                .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() })
                    .ToList();
            ViewBag.Sectors = _sectorBusiness.GetSectorByDistrict(Guid.Parse(model.IdDistrict))
                .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() })
                    .ToList();
        }

        [HttpPost]
        public IActionResult SaveRegister(string branch, string returnUrl = null)
        {
            try
            {
                var model = JSonConvertUtil.Deserialize<BranchRegisterViewModel>(branch);
                TryValidateModel(model);

                if (!ModelState.IsValid) return null;

                _branchBusiness.Save(model, ApplicationUserCurrent.AccountId);
                if (string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return RedirectToAction("Index");
                }
                return Redirect(model.ReturnUrl);
            }
            catch (Exception e)
            {
                _logger.LogError("Error al guardar datos de local: " + e.Message, null);
                return null;
            }

        }

        [Authorize]
        public IActionResult Index(string filterValues, bool deleteFilter, int pageSize = 20, int pageIndex = 1)
        {
            try
            {
                var filters = GetFilters(filterValues, deleteFilter);
                var branches = _branchBusiness.GetPaginatedBranches(filters, pageSize, pageIndex, ApplicationUserCurrent.AccountId);

                return View(branches);
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(0, "Error Index"), e.Message);
                return RedirectToAction("Index", "StatusCode", new { statusCode = 1 });
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAccount(BranchRegisterViewModel model)
        {
            try
            {
                if (model.Id == Guid.Empty)
                {
                    ModelState.AddModelError("Unsaved", "Para Agregar Cuentas debe primero guardar el Local");
                }

                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Register", new { idBranch = model.Id });
                }

                _branchBusiness.AddAccount(model, ApplicationUserCurrent.AccountId);

                return RedirectToAction("Register", new { idBranch = model.Id });
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(0, "Error Index"), e.Message);
                return RedirectToAction("Index", "StatusCode", new { statusCode = 1 });
            }
        }
        
        [HttpPost]
        public JsonResult DeleteAccounts(string selectedAccounts)
        {
            var results = JSonConvertUtil.Deserialize<string[]>(selectedAccounts);

            _branchBusiness.DeleteAccounts(results);

            return Json("ok");
        }
        
        [HttpGet]
        public IActionResult Localization(string input)
        {
            if (null != input)
            {
                string[] itemIds = JsonConvert.DeserializeObject<string[]>(input);
                var locales = _branchBusiness.GetBranchesList(itemIds, ApplicationUserCurrent.AccountId);

                ViewData[CBranch.ViewDataLocalizacion] = JSonConvertUtil.Convert(locales);
            }

            return View();
        }
        
        [HttpGet]
        public async Task<string> GetBranchById(string idBranch)
        {
            var itemReturn = new Branch();

            if (!string.IsNullOrEmpty(idBranch))
            {
                itemReturn = _branchBusiness.GetOne(new Guid(idBranch), ApplicationUserCurrent.AccountId);
                //inserto las imagenes en base 64 en el objeto
                itemReturn.BranchImages = _branchImageBusiness.GetBranchesImagesList(Guid.Parse(idBranch), ApplicationUserCurrent.AccountId);
            }

            return JSonConvertUtil.Convert(itemReturn);
        }

        [HttpGet]
        public async Task<string> GetBranchProfile(string idBranch)
        {
            var branch = _branchBusiness.GetBranchCompleteProfile(new Guid(idBranch), ApplicationUserCurrent.AccountId);
            var smslista = _smsBusiness.GetCampaignByIdSms(ApplicationUserCurrent.AccountId);
            branch.BranchImages = await GetBranchImagesInBase64(branch.Id);

            var itemReturn = ConvertBranch.ConvertBranchToBranchProfileViewModel(branch,smslista);

            return JSonConvertUtil.Convert(itemReturn);
        }

        [HttpPost]
        public string SaveBranch(string input)
        {
            Guid idAccount = ApplicationUserCurrent.AccountId;
            Branch branch = JsonConvert.DeserializeObject<Branch>(input);

            branch = _branchBusiness.SaveBranch(branch, idAccount);

            return JSonConvertUtil.Convert(branch);
        }

        private async Task<List<BranchImages>> GetBranchImagesInBase64(Guid idBranch)
        {
            //Éste método se encuentra a nivel de controlador debido a que la clase 
            //de manejo de azurestorage se encuentra en proyecto Web
            var listResult = _branchImageBusiness.GetBranchesImagesList(idBranch, ApplicationUserCurrent.AccountId);
            foreach (var branchImage in listResult)
            {
                var imageB64 = await AzureStorageUtil.GetBase64FileFromBlob(CBranch.ImagesContainer, branchImage.NameFile);
                branchImage.UrlImage = $"data:image/gif;base64,{imageB64}";
            }
            return listResult;
        }

        [HttpGet]
        public List<Branch> SearchBranches(Guid idCountry, Guid idProvince,
            Guid idDistrict, string documentType,
            string document, string nameBranch,
            string ownerName, string codeBranch)
        {
            return _branchBusiness.SearchBranches(idCountry, idProvince,
                idDistrict, documentType,
                document, nameBranch,
                ownerName, codeBranch, ApplicationUserCurrent.AccountId);
        }

        [HttpGet]
        public List<Branch> GetAllBranches()
        {
            return _branchBusiness.GetBranchesList(ApplicationUserCurrent.AccountId);
        }

        [HttpPost]
        public async Task<string> UploadBranchImage(string branch)
        {
            List<BranchImages> itemResult;

            try
            {
                if (new Guid(branch) == Guid.Empty)
                {
                    throw new ExceptionMardis();
                }

                var files = Request.Form.Files;

                await UploadFilesToAzure(branch, files);

                itemResult = await GetBranchImagesInBase64(new Guid(branch));

            }
            catch (Exception ex)
            {
                itemResult = new List<BranchImages>();
            }

            return JSonConvertUtil.Convert(itemResult);
        }

        private async Task UploadFilesToAzure(string branch, IFormFileCollection files)
        {
            foreach (var file in files)
            {
                var name = Guid.NewGuid().ToString();

                byte[] bytes;
                using (var stream = file.OpenReadStream())
                {
                    bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);
                }

                var fileStream = (new MemoryStream(bytes));

                await AzureStorageUtil.UploadFromStream(fileStream, CBranch.ImagesContainer, name);
                var uri = AzureStorageUtil.GetUriFromBlob(CBranch.ImagesContainer, name);
                _branchImageBusiness.SaveBranchImages(new BranchImages
                {
                    IdBranch = new Guid(branch),
                    NameContainer = CBranch.ImagesContainer,
                    NameFile = name,
                    UrlImage = uri
                });
            }
        }

        [HttpGet]
        public async Task<string> DeleteImageBranch(Guid idImageBranch)
        {
            var imageBranch = _branchImageBusiness.GetBranchImageById(idImageBranch, ApplicationUserCurrent.AccountId);
            AzureStorageUtil.DeleteBlob(CBranch.ImagesContainer, imageBranch.NameFile);
            _branchImageBusiness.DeleteBranchImage(idImageBranch, ApplicationUserCurrent.AccountId);

            return await GetBranchById(imageBranch.Id.ToString());
        }


        [HttpGet]
        public string GetBranchesByCampaign(Guid idCampaign)
        {
            var taskList = _taskCampaignBusiness.GetTasksBranchesByCampaign(idCampaign, ApplicationUserCurrent.AccountId);

            var resultList = ConvertTask.ConvertTaskListToCampaignBranchesViewModelList(taskList);

            return JSonConvertUtil.Convert(resultList);
        }
    }
}