using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCommon
{
    public class CountryDao : ADao
    {
        public CountryDao(MardisContext mardisContext)
            : base(mardisContext)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Country> GetCountries()
        {

            var itemsReturn = Context.Countries
                                     .Where(tb => tb.StatusRegister == CStatusRegister.Active)
                                     .OrderBy(tb => tb.Name)
                                     .ToList();

            return itemsReturn;
        }
    }
}
