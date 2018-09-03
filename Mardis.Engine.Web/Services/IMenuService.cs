using System.Collections.Generic;
using Mardis.Engine.DataAccess.MardisSecurity;

namespace Mardis.Engine.Web.Services
{
    public interface IMenuService
    {
         List<Menu> GetMenuList();
    }
}
