using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mardis.Engine.Business.MardisCore
{
    public class ChannelBusiness
    {
        readonly ChannelDao _channelDao;

        public ChannelBusiness(MardisContext mardisContext)
        {
            _channelDao = new ChannelDao(mardisContext);
        }
        
        public List<Channel> GetChannelsByCustomerId(Guid idCustomer, Guid idAccount)
        {
            return _channelDao.GetChannelsByCustomerId(idCustomer, idAccount);
        }

        public List<SelectListItem> GetChanelListByCustomer(Guid idCustomer, Guid idAccount)
        {
            return GetChannelsByCustomerId(idCustomer, idAccount)
                    .Select(t => new SelectListItem() { Text = t.Name, Value = t.Id.ToString() })
                    .ToList();
        }
        
        public Channel GetOne(Guid id, Guid idAccount)
        {
            return _channelDao.GetOne(id, idAccount);
        }
    }
}
