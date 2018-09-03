using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mardis.Engine.Web.ViewModel
{
    /// <summary>
    /// Es un View Model por Cada Clase
    /// </summary>
    [Obsolete]
    public class MenuViewModel
    {
        public IList<MenuItemViewModel> NavigationItems { get; }

        public MenuViewModel(IList<MenuItemViewModel> navigationItems)
        {
            NavigationItems = navigationItems;
        }
    }

    /// <summary>
    /// Es un View Model por Cada Clase
    /// </summary>
    [Obsolete]
    public class MenuItemViewModel
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Icon { get; }
        public string UrlMenu { get; }
        public Guid IdParent { get; }
        public string StatusRegister { get; }

        public static explicit operator Task<object>(MenuItemViewModel v)
        {
            throw new NotImplementedException();
        }

        public MenuItemViewModel(Guid code, string nameMenu, string iconMenu, string urlMenus, Guid codeParent, string status)
        {
            Id = code;
            Name = nameMenu;
            Icon = iconMenu;
            UrlMenu = urlMenus;
            IdParent = codeParent;
            StatusRegister = status;
        }

        public MenuItemViewModel()
        {
        }
    }


    /// <summary>
    /// Es un View Model por Cada Clase
    /// </summary>
    [Obsolete]
    public class AuthorizationProfileViewModel
    {
        public Guid Id { get; }
        public Guid IdProfile { get; }
        public Guid IdMenu { get; }

        public IList<AuthorizationProfileViewModel> NavigationItems { get; }


    }
}
