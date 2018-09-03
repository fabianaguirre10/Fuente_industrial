using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;

namespace Mardis.Engine.Business.MardisCore
{
    public class SmsBusiness : ABusiness
    {
        private readonly CampaignDao _campaignDao;
        private readonly SmsDao _smsnDao;



        public SmsBusiness(MardisContext mardisContext) : base(mardisContext)
        {
            _campaignDao = new CampaignDao(mardisContext);
            _smsnDao = new SmsDao(mardisContext);
        }
        public List<Sms> GetCampaignByIdSms(Guid idAccount)
        {
            return _smsnDao.GetCampaignAccountSms(idAccount);
        }
    }
}
