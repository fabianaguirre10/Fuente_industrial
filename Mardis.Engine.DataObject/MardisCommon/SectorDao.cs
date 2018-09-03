using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCommon
{
    public class SectorDao : ADao
    {
        public SectorDao(MardisContext mardisContext) 
               : base(mardisContext)
        {

        }
        
        public List<Sector> GetSectorByDistrict(Guid idDistrict) {

            var itemReturn = Context.Sectors
                                    .Where(tb => tb.IdDistrict == idDistrict &&
                                           tb.StatusRegister == CStatusRegister.Active)
                                    .OrderBy(tb => tb.Name)
                                    .ToList();


            return itemReturn;
        }
        
        public Sector GetOne(Guid id)
        {
            return Context.Sectors
                                  .FirstOrDefault(tb => tb.Id == id &&
                                         tb.StatusRegister == CStatusRegister.Active);
        }
        
        public Sector GetByCode(string code)
        {
            return Context.Sectors
                                  .FirstOrDefault(tb => tb.Code == code && tb.StatusRegister == CStatusRegister.Active);
        }
    }
}
