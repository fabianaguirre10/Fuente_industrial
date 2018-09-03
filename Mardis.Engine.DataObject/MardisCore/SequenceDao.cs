using System;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class SequenceDao : ADao
    {
        public SequenceDao(MardisContext mardisContext) 
            : base(mardisContext)
        {

        }
        
        public Sequence GetSequenceByCode(string code, Guid idAccount) {

            var returnValue = Context.Sequences
                                     .FirstOrDefault(tb => tb.Code == code
                                            && tb.IdAccount == idAccount);

            return returnValue;
        }


      
    }
}
