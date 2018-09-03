using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Web.ViewModel.Filter;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class AccountDao : ADao
    {
        public AccountDao(MardisContext mardisContext) : base(mardisContext)
        {
        }

        public Account Get(Guid idAccount)
        {
            return Context.Accounts
                .FirstOrDefault(a => a.Id == idAccount &&
                                    a.StatusRegister == CStatusRegister.Active);
        }

        public List<Account> GetAll(List<FilterValue> filterValues, int pageSize, int pageIndex)
        {
            var strPredicate = $" StatusRegister == \"{CStatusRegister.Active}\" ";

            strPredicate += GetFilterPredicate(filterValues);

            return Context.Accounts
                .Where(strPredicate)
                .OrderBy(b => b.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetCount(List<FilterValue> filterValues)
        {
            var strPredicate = $" StatusRegister == \"{CStatusRegister.Active}\" ";

            strPredicate += GetFilterPredicate(filterValues);

            return Context.Accounts
                .Where(strPredicate)
                .Count();
        }
    }
}
