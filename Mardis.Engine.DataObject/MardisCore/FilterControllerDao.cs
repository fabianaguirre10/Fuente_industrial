using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class FilterControllerDao : ADao
    {
        public FilterControllerDao(MardisContext mardisContext) 
              : base(mardisContext)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public FilterController GetControllerById(string controller)
        {
            var returnValue = Context.FilterControllers
                .FirstOrDefault(tb => tb.NameController == controller);

            return returnValue;
        }
    }
}
