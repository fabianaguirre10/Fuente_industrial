using System;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisSecurity;
using Mardis.Engine.DataObject.MardisSecurity;
using Microsoft.Extensions.Caching.Memory;

namespace Mardis.Engine.Business.MardisSecurity
{
    /// <summary>
    /// Business de Perfiles
    /// </summary>
    public class ProfileBusiness
    {
        readonly ProfileDao _profileDao;

        public ProfileBusiness(MardisContext context)
        {
            
            _profileDao = new ProfileDao(context);
        }


        /// <summary>
        /// Dame Rol por Id
        /// </summary>
        /// <param name="idProfile"></param>
        /// <returns></returns>
        public Profile GetById(Guid idProfile)
        {
            return _profileDao.GetById(idProfile);
        }


        /// <summary>
        /// Dame Perfil por Código
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Profile GetByCode(string code)
        {
            return _profileDao.GetByCode(code);
        }
    }
}
