using System;
using System.Collections.Generic;
using Mardis.Engine.Business;
using Mardis.Engine.Business.MardisSecurity;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisSecurity;
using Mardis.Engine.Web.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Mardis.Engine.Web.Services
{
    public class MenuService : IMenuService
    {
        private readonly MenuBusiness _menuBusiness;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        protected ApplicationUser ApplicationUserCurrent { get; set; }

        public MenuService(MardisContext mardisContext, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _menuBusiness=new MenuBusiness(mardisContext);
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            SetCurrentUser();
        }

        private async void SetCurrentUser()
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            if (userId != null)
            {
                ApplicationUserCurrent = await _userManager.FindByIdAsync(userId);
            }
        }

        List<Menu> IMenuService.GetMenuList()
        {
            Guid idProfile = ApplicationUserCurrent.ProfileId;
            List<Menu> itemMenu =
            _menuBusiness.GetOnlyParentsByProfile(idProfile);

            foreach (var itemMenuTemp in itemMenu)
            {
                itemMenuTemp.MenuChildrens = _menuBusiness.GetChildrens(idProfile, itemMenuTemp.Id);
            }

            return itemMenu;
        }
    }
}
