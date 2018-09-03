using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mardis.Engine.Business.MardisCore
{
    public class TypePollBusiness
    {
        readonly TypePollDao _typePollDao;

        public TypePollBusiness(MardisContext mardisContext) 
        {
            _typePollDao = new TypePollDao(mardisContext);
        }


        /// <summary>
        /// Dame Todos los Tipos de Encuestas
        /// </summary>
        /// <returns></returns>
        public List<TypePoll> GetAll()
        {
            return _typePollDao.GetAll();
        }

        public List<SelectListItem> GetTypePollList()
        {
            return GetAll()
                .Select(t => new SelectListItem() {Text = t.Name, Value = t.Id.ToString()})
                .ToList();
        } 

        /// <summary>
        /// Dame la encuesta por código
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public TypePoll GetByCode(string code)
        {
            return _typePollDao.GetByCode(code);
        }
    }
}
