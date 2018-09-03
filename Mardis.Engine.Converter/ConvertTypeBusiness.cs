using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Web.ViewModel;

namespace Mardis.Engine.Converter
{
    public class ConvertTypeBusiness
    {
        public static List<ListCustomerTemporary> ToListCustomerTemporaries(List<TypeBusiness> typeList)
        {
            return typeList
                .Select(t => new ListCustomerTemporary()
                {
                    Id = t.Id.ToString(),
                    Name = t.Name,
                    Action = (t.Id != Guid.Empty && t.StatusRegister != CStatusRegister.Active) ? "BDD" : "NEW"
                })
                .ToList();
        }
    }
}
