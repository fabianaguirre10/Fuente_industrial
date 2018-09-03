using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCommon
{
    public class ParishDao : ADao
    {
        public ParishDao(MardisContext mardisContext)
            : base(mardisContext)
        {

        }
        
        public List<Parish> GetParishByDistrict(Guid idDistrict)
        {

            var itemReturn = Context.Parishes
                                    .Where(tb => tb.IdDistrict == idDistrict &&
                                           tb.StatusRegister == CStatusRegister.Active)
                                    .OrderBy(tb => tb.Name)
                                    .ToList();

            return itemReturn;
        }

        public Parish GetOne(Guid id)
        {
            return Context.Parishes
                                   .FirstOrDefault(tb => tb.Id == id &&
                                              tb.StatusRegister == CStatusRegister.Active);
        }
        public List<Parish> GetParish()
        {
            return Context.Parishes.Where(tb => tb.StatusRegister == CStatusRegister.Active).ToList();
        }


        public Parish GetParishByCode(string code)
        {
            return Context.Parishes
                                   .FirstOrDefault(tb => tb.Code == code &&
                                              tb.StatusRegister == CStatusRegister.Active);
        }
    }
}
