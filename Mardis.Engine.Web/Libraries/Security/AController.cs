using System;
using System.Collections.Generic;
using Mardis.Engine.Business;
using Mardis.Engine.DataAccess;
using Mardis.Engine.Framework;
using Mardis.Engine.Web.Model;
using Mardis.Engine.Web.ViewModel.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Mardis.Engine.Web.Libraries.Security
{
    public class AController<T> : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CommonBusiness _commonBusiness;
        protected ILogger<T> _logger;
        protected string TableName;
        protected string ControllerName;
        protected ISession Session => _httpContextAccessor.HttpContext.Session;

        public AController(UserManager<ApplicationUser> userManager,
                           IHttpContextAccessor httpContextAccessor,
                           MardisContext mardisContext,
                           ILogger<T> logger)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _commonBusiness = new CommonBusiness(mardisContext);
            Context = mardisContext;
            _logger = logger;

            SetCurrentUser();
        }

        protected void SetSessionVariable(string key, string value)
        {
            if (value != null)
            {
                Session.SetString(key, value);
            }
        }

        protected string GetSessionVariable(string key)
        {
            return Session.GetString(key);
        }

        protected MardisContext Context { get; }

        protected ApplicationUser ApplicationUserCurrent { get; set; }

        private async void SetCurrentUser()
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);

            ApplicationUserCurrent = await _userManager.FindByIdAsync(userId);
        }

        protected List<FilterValue> GetFilters(string filterValues, bool deleteFilter)
        {
            List<FilterValue> filters;

            if (string.IsNullOrEmpty(filterValues))
            {
                SetSessionVariable("filter", "");
            }

            if (deleteFilter)
            {
                filters = JSonConvertUtil.Deserialize<List<FilterValue>>(filterValues);
            }
            else
            {
                var filter = JSonConvertUtil.Deserialize<FilterValue>(filterValues);

                filters = JSonConvertUtil.Deserialize<List<FilterValue>>(GetSessionVariable("filter"));

                if (filters == null)
                {
                    filters = new List<FilterValue>();
                }

                if (filter != null)
                {
                    filters.Add(filter);
                }
            }

            SetSessionVariable("filter", JSonConvertUtil.Convert(filters));

            return filters;
        }

        #region Metodos Generales para todos los Controladores

        [HttpPost]
        public virtual bool Delete(string input)
        {
            var returnIsValid = true;

            try
            {
                var itemIds = JsonConvert.DeserializeObject<string[]>(input);

                _commonBusiness.DeleteId(TableName, itemIds);
            }
            catch (Exception)
            {
                returnIsValid = false;
            }

            return returnIsValid;
        }

        #endregion

    }
}
