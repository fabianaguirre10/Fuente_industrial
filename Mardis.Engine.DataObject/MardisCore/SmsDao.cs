using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.DataObject.MardisCore
{
   public  class SmsDao : ADao
    {
        public SmsDao(MardisContext mardisContext) : base(mardisContext)
        {
        }
        public List<Sms> GetCampaignAccountSms(Guid idAccount)
        {
            return Context.Sms.Where(cs => cs.idAccount == idAccount).ToList();
        }

    }
}
