using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Web.ViewModel.Filter;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class CustomerDao : ADao
    {
        private readonly ChannelDao _channelBusiness;
        private readonly TypeBusinessDao _typeBusinessBusiness;
        private readonly ProductCategoryDao _productCategoryBusiness;

        public CustomerDao(MardisContext mardisContext)
              : base(mardisContext)
        {
            _channelBusiness = new ChannelDao(mardisContext);
            _typeBusinessBusiness = new TypeBusinessDao(mardisContext);
            _productCategoryBusiness = new ProductCategoryDao(mardisContext);
        }
        
        public Customer GetOne(Guid id, Guid idAccount)
        {
            var itemReturn = Context.Customers
                .FirstOrDefault(tb => tb.Id == id
                            && tb.StatusRegister == CStatusRegister.Active);

            if (null != itemReturn)
            {
                itemReturn.Channels = _channelBusiness.GetChannelsByCustomerId(id, idAccount);
                itemReturn.TypeBusiness = _typeBusinessBusiness.GetAllTypesBusinessByIdCustomer(id,idAccount);
                itemReturn.ProductCategories = _productCategoryBusiness.GetProductCategories(id,idAccount);
            }

            return itemReturn;
        }
        
        public Customer GetCustomerById(Guid id, Guid idAccount)
        {
            return Context.Customers
                          .FirstOrDefault(e => e.Id == id && e.IdAccount == idAccount);
        }
        
        public List<Customer> GetCustomersByAccount(Guid idAccount)
        {
            return Context.Customers
                .Where(c => c.IdAccount == idAccount
                        && c.StatusRegister == CStatusRegister.Active).
                ToList();
        }
        
        public List<Customer> GetCustomerByTypeService(Guid idTypeService, Guid idAccount)
        {
            var itemsReturn = Context.Customers
                                     .Join(Context.Services,
                                        tb => tb.Id,
                                        se => se.IdCustomer,
                                        (tb, se) => new { tb, se })
                                     .Join(Context.TypeServices,
                                        tb => tb.se.IdTypeService,
                                        ts => ts.Id,
                                        (tb, ts) => new { tb, ts })
                                     .Where(tb =>
                                                tb.tb.se.IdTypeService == idTypeService &&
                                                tb.tb.tb.IdAccount == idAccount &&
                                                tb.tb.se.StatusRegister == CStatusRegister.Active &&
                                                tb.tb.tb.StatusRegister == CStatusRegister.Active &&
                                                tb.ts.StatusRegister == CStatusRegister.Active)
                                     .Select(tb => tb.tb.tb)
                                     .Distinct()
                                     .ToList();

            return itemsReturn;
        }

        public List<Customer> GetPaginatedCustomerList(List<FilterValue> filters, int pageSize, int pageIndex, Guid idAccount)
        {
            var strPredicate = $" StatusRegister == \"{CStatusRegister.Active}\" && IdAccount == \"{idAccount.ToString()}\" ";

            strPredicate += GetFilterPredicate(filters);

            var resultList = Context.Customers
                .Include(c => c.TypeCustomer)
                .Where(strPredicate)
                .OrderBy(b => b.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return resultList;
        }

        public int GetPaginatedCustomersCount(List<FilterValue> filters, int pageSize, int pageIndex, Guid idAccount)
        {
            var strPredicate = $" StatusRegister == \"{CStatusRegister.Active}\" && IdAccount == \"{idAccount.ToString()}\" ";

            strPredicate += GetFilterPredicate(filters);

            var resultList = Context.Customers
                .Count(strPredicate);

            return resultList;
        }
    }
}
