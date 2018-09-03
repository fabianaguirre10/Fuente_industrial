using System;
using System.Collections.Generic;
using Mardis.Engine.Business.MardisSecurity;
using Mardis.Engine.Converter;
using Mardis.Engine.DataAccess;
using Mardis.Engine.Framework;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.Libraries.Security;
using Mardis.Engine.Web.Model;
using Mardis.Engine.Web.ViewModel;
using Mardis.Engine.Web.ViewModel.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Mardis.Engine.Web.Controllers
{
    [Authorize]
    public class UserController : AController<UserController>
    {
        private readonly UserBusiness _userBusiness;
        private readonly IDataProtector _protector;
        private readonly TypeUserBusiness _typeUserBusiness;

        public UserController(
                                UserManager<ApplicationUser> userManager,
                                IHttpContextAccessor httpContextAccessor,
                                MardisContext mardisContext,
                                ILogger<UserController> logger,
                                IDataProtectionProvider protectorProvider,
                                IMemoryCache memoryCache,
                                RedisCache distributedCache)
            : base(userManager, httpContextAccessor, mardisContext, logger)
        {
            _protector = protectorProvider.CreateProtector(GetType().FullName);
            _userBusiness = new UserBusiness(mardisContext);
            _typeUserBusiness = new TypeUserBusiness(mardisContext, distributedCache);
            TableName = "MardisSecurity.[User]";
        }

        [HttpGet]
        public List<SelectViewModel> GetSupervisorList()
        {
            var idAccount = ApplicationUserCurrent.AccountId;

            var userList = _userBusiness.GetUserListByType(
                    CTypePerson.PersonSupervisor,
                    idAccount);

            var resultList = UserConverter.ConvertUserListToSelectViewModelList(userList);

            return resultList;
        }

        [HttpGet]
        public List<SelectViewModel> GetMerchantsList()
        {
            var idAccount = ApplicationUserCurrent.AccountId;
            var userList = _userBusiness.GetUserListByType(
                    CTypePerson.PersonMerchant,
                    idAccount);

            var resultList = UserConverter.ConvertUserListToSelectViewModelList(userList);

            return resultList;
        }

        [HttpGet]
        public string GetMerchantsByCampaign(Guid idCampaign)
        {
            var merchantsList = _userBusiness.GetMerchantsByCampaign(idCampaign, ApplicationUserCurrent.AccountId);

            var resultList = ConvertDashBoard.ConvertUserListToDashBoardMerchantViewModelList(merchantsList);

            return JSonConvertUtil.Convert(resultList);
        }

        [HttpGet]
        public IActionResult Index(string filterValues, bool deleteFilter, int pageIndex = 1, int pageSize = 10)
        {
            var filters = GetFilters(filterValues, deleteFilter);
            var model = _userBusiness.GetIndex(filters, pageIndex, pageSize, ApplicationUserCurrent.AccountId, _protector);

            return View(model);
        }

        [HttpGet]
        public IActionResult Register(string user)
        {
            var model = _userBusiness.GetUser(user, _protector, ApplicationUserCurrent.AccountId);

            LoadSelectData();

            return View(model);
        }

        [HttpPost]
        public IActionResult Register(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                LoadSelectData();
                return View(model);
            }

            _userBusiness.Save(model, ApplicationUserCurrent.AccountId);

            return RedirectToAction("Index");
        }

        private void LoadSelectData()
        {
            ViewBag.UserTypes = _typeUserBusiness.GetAllListItems();
        }

        [HttpPost]
        public override bool Delete(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            var itemIds = JsonConvert.DeserializeObject<string[]>(input);

            for (var i = 0; i < itemIds.Length; i++)
            {
                itemIds[i] = _protector.Unprotect(itemIds[i]);
            }

            return base.Delete(JsonConvert.SerializeObject(itemIds));
        }
    }
}
