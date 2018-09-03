using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class TypeServiceDao : ADao
    {
        public TypeServiceDao(MardisContext mardisContext) 
               : base(mardisContext)
        {

        }

        /// <summary>
        /// Dame Tipo de Servicios
        /// </summary>
        /// <returns></returns>
        public List<TypeService> GetAll() {

            var itemsReturn = Context.TypeServices
                .Where(tb=>tb.StatusRegister == CStatusRegister.Active)
                                     .ToList();

            return itemsReturn;
        }
    }
}
