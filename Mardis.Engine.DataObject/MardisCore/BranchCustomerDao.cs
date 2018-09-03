using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class BranchCustomerDao : ADao
    {
        private readonly ChannelDao _channelDao;
        private readonly TypeBusinessDao _typeBusinessDao;
        private readonly CustomerDao _customerDao;

        public BranchCustomerDao(MardisContext mardisContext)
             : base(mardisContext)
        {
            _channelDao = new ChannelDao(mardisContext);
            _typeBusinessDao = new TypeBusinessDao(mardisContext);
            _customerDao = new CustomerDao(mardisContext);
        }
        
        public List<BranchCustomer> GetBranchCustomerByBranch(Guid idBranch, Guid idAccount)
        {
            var itemsReturn =
                              Context.BranchCustomers.Where(tb => tb.IdBranch == idBranch &&
                              tb.Branch.IdAccount == idAccount)
                                   .ToList();

            foreach (var itemTemp in itemsReturn)
            {
                itemTemp.Channel = _channelDao.GetOne(itemTemp.IdChannel, idAccount);
                itemTemp.TypeBusiness = _typeBusinessDao.GetOne(itemTemp.IdTypeBusiness, idAccount);
                itemTemp.Customer = _customerDao.GetOne(itemTemp.IdCustomer, idAccount);
            }

            return itemsReturn;
        }

        public void DeleteAccounts(string[] results)
        {
            var items = results.Select(Guid.Parse).ToArray();

            Context.BranchCustomers.RemoveRange(Context.BranchCustomers.Where(b => items.Contains(b.Id)));
            Context.SaveChanges();
        }
    }
}
