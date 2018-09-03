using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class ChannelDao : ADao
    {
        public ChannelDao(MardisContext mardisContext) : base(mardisContext)
        {
        }
        
        public List<Channel> GetChannelsByCustomerId(Guid idCustomer, Guid idAccount)
        {
            return Context.Channels
                .Where(ch => ch.IdCustomer == idCustomer
                        && ch.StatusRegister == CStatusRegister.Active &&
                        ch.IdAccount == idAccount)
                .ToList();
        }


        public Channel GetOne(Guid id, Guid idAccount)
        {
            return Context.Channels
                                   .FirstOrDefault(tb => tb.Id == id &&
                                   tb.IdAccount == idAccount);
        }
    }
}
