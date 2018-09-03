using Mardis.Engine.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess.MardisCommon;

namespace Mardis.Engine.DataObject.MardisCommon
{
    public class ProvinceDao : ADao
    {
        public ProvinceDao(MardisContext mardisContext) : base(mardisContext)
        {
        }

        /// <summary>
        /// Obtiene todas las provincias de un Pais dado
        /// </summary>
        /// <param name="idCountry"> Id del Pais </param>
        /// <returns></returns>
        public List<Province> GetProvincesByCountry(Guid idCountry)
        {
            var itemsReturn = Context.Provinces
                .Where(tb => tb.IdCountry == idCountry)
                .OrderBy(p => p.Name)
                .ToList();
            return itemsReturn;
        }

        public List<Province> GetAll()
        {
            return Context.Provinces
                .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Province GetProvinceByCode(string code)
        {
            var itemReturn = Context.Provinces
                                    .FirstOrDefault(tb => tb.Code == code);

            return itemReturn;
        }
    }
}
