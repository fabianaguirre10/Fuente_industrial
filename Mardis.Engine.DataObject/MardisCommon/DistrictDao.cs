using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCommon
{
    public class DistrictDao : ADao
    {
        public DistrictDao(MardisContext mardisContext)
               : base(mardisContext)
        {

        }
        
        public List<District> GetDistrictByProvince(Guid idProvince)
        {

            var itemReturn = Context.Districts
                                    .Where(tb => tb.IdProvince == idProvince)
                                    .OrderBy(tb => tb.Name)
                                    .ToList();

            return itemReturn;
        }
        
        public District GetOne(Guid id)
        {
            return Context.Districts
                                    .FirstOrDefault(tb => tb.Id == id &&
                                           tb.StatusRegister == CStatusRegister.Active);
        }
        
        public District GetDistrinctByCode(string code)
        {
            return Context.Districts
                                    .FirstOrDefault(tb => tb.Code == code &&
                                           tb.StatusRegister == CStatusRegister.Active);
        }
        public List<District> GetDistrinct()
        {
            return Context.Districts.Where(tb=> tb.StatusRegister == CStatusRegister.Active).ToList();
        }
    }
}
