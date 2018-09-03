using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class TypePollDao : ADao
    {
        public TypePollDao(MardisContext mardisContext) 
               : base(mardisContext)
        {

        }


        /// <summary>
        /// Dame Todos los Tipos de Encuestas
        /// </summary>
        /// <returns></returns>
        public List<TypePoll> GetAll() {

            var itemReturns = Context.TypePolls
                                     .Where(tb=>tb.StatusRegister == CStatusRegister.Active)
                                     .ToList();

            return itemReturns;
        }

        /// <summary>
        /// Dame la encuesta por código
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public TypePoll GetByCode(string code)
        {
            var itemReturn = Context.TypePolls
                                    .FirstOrDefault(tb => tb.Code == code &&
                                                 tb.StatusRegister == CStatusRegister.Active);

            return itemReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TypePoll GeTypePollById(Guid id)
        {
            return Context.TypePolls
                .FirstOrDefault(tp => tp.StatusRegister == CStatusRegister.Active &&
                                      tp.Id == id);
        }
    }
}
