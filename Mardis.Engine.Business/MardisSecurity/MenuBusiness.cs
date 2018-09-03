using System;
using System.Collections.Generic;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisSecurity;
using Mardis.Engine.DataObject.MardisSecurity;

namespace Mardis.Engine.Business.MardisSecurity
{
    public class MenuBusiness
    {
        readonly MenuDao _menuDao;
        
        public MenuBusiness(MardisContext context)
        {
            _menuDao = new MenuDao(context);
        }
        
        public Menu GetMenuById(Guid idMenu)
        {
            return _menuDao.GetMenuById(idMenu);
        }
        
        public List<Menu> GetOnlyParentsByProfile(Guid idProfile) {
            return _menuDao.GetOnlyParentsByProfile(idProfile);
        }
        
        public List<Menu> GetChildrens(Guid idProfile,Guid idMenu)
        {
            return _menuDao.GetChildrens(idProfile, idMenu);
        }

    }
}
