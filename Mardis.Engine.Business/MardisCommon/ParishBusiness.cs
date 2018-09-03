using System;
using System.Collections.Generic;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataObject.MardisCommon;

namespace Mardis.Engine.Business.MardisCommon
{
    public class ParishBusiness
    {
        readonly ParishDao _parishDao;

        public ParishBusiness(MardisContext mardisContext)
        {
            _parishDao = new ParishDao(mardisContext);
        }


        /// <summary>
        /// Dame Parroquias por Cantones (Distritos)
        /// </summary>
        /// <param name="idDistrict"></param>
        /// <returns></returns>
        public List<Parish> GetParishByDistrict(Guid idDistrict)
        {
            return _parishDao.GetParishByDistrict(idDistrict);
        }
        public List<Parish> GetParish()
        {
            return _parishDao.GetParish();
        }
    }
}
