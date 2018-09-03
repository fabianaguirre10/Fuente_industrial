using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;

namespace Mardis.Engine.Business.MardisCore
{
    public class FilterControllerBusiness 
    {
        readonly FilterControllerDao _filterControllerDao;

        public FilterControllerBusiness(MardisContext mardisContext)
        {
            _filterControllerDao = new FilterControllerDao(mardisContext);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public FilterController GetControllerById(string controller) {

            return _filterControllerDao.GetControllerById(controller);
        }
    }
}
