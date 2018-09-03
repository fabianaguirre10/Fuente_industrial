using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Web.ViewModel;

namespace Mardis.Engine.Converter
{
    public class ConvertChannel
    {
        public static List<ListCustomerTemporary> ToListCustomerTemporaries(List<Channel> channels)
        {
            return channels
                .Select(c => new ListCustomerTemporary()
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    Action = (c.Id != Guid.Empty && c.StatusRegister != CStatusRegister.Active) ? "BDD" : "NEW"
                })
                .ToList();
        }
    }
}
