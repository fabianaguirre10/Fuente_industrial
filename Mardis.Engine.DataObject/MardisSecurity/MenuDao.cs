using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisSecurity;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisSecurity
{
    /// <summary>
    /// Negocio de Menu
    /// </summary>
    public class MenuDao : ADao
    {
        
        public MenuDao(MardisContext context)
            : base(context)
        {

        }
        
        public Menu GetMenuById(Guid idMenu)
        {
            var returnValue = Context.Menus
                .FirstOrDefault(u => u.Id == idMenu &&
                                     u.StatusRegister == CStatusRegister.Active);

            return returnValue;
        }
        
        public List<Menu> GetOnlyParentsByProfile(Guid idProfile)
        {
            var lstItems = Context.Menus
                .Join(Context.AuthorizationProfiles,
                    m => m.Id,
                    ap => ap.IdMenu,
                    (m, ap) => new { m, ap })
                .Join(Context.Profiles,
                    t=>t.ap.IdProfile,
                    p=>p.Id,
                    (t,p)=>new {t,p})
                .Where(tb=>tb.t.ap.IdProfile==idProfile
                           && tb.t.m.StatusRegister == CStatusRegister.Active
                           && tb.p.StatusRegister == CStatusRegister.Active
                           && tb.t.m.IdParent == null)
                .OrderBy(tb=>tb.t.m.OrderMenu)
                .Select(tb => tb.t.m)
                .ToList();

            return lstItems;
        }
        
        public List<Menu> GetChildrens(Guid idProfile,Guid idMenu)
        {
            var lstItems = Context.Menus
                .Join(Context.AuthorizationProfiles,
                    m => m.Id,
                    ap => ap.IdMenu,
                    (m, ap) => new { m, ap })
                .Join(Context.Profiles,
                    t => t.ap.IdProfile,
                    p => p.Id,
                    (t, p) => new { t, p })
                .Where(tb => tb.t.ap.IdProfile == idProfile
                             && tb.t.m.StatusRegister == CStatusRegister.Active
                             && tb.p.StatusRegister == CStatusRegister.Active
                             && tb.t.m.IdParent == idMenu)
                .OrderBy(tb => tb.t.m.OrderMenu)
                .Select(tb => tb.t.m)
                .ToList();

            return lstItems;
        }
        
    }
}
