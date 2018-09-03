using System;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class BulkLoadStatusDao : ADao
    {
        public BulkLoadStatusDao(MardisContext mardisContext) 
               : base(mardisContext)
        {

        }

        /// <summary>
        /// Dame un Registro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BulkLoadStatus GetOne(Guid id)
        {
              var itemReturn = Context.BulksLoadStatus
                                       .FirstOrDefault(tb => tb.Id == id &&
                                                  tb.StatusRegister == CStatusRegister.Active);

            return itemReturn;
        }

        /// <summary>
        /// Dame uno por código
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public BulkLoadStatus GetOneByCode(string code)
        {
            var itemReturn = Context.BulksLoadStatus
                                    .FirstOrDefault(tb => tb.Code == code &&
                                           tb.StatusRegister == CStatusRegister.Active);

            return itemReturn;
        }
    }
}
