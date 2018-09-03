using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.Converter;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.ViewModel.CustomerViewModels;
using Mardis.Engine.Web.ViewModel.Filter;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mardis.Engine.Business
{
    /// <summary>
    /// Negocio de Clientes
    /// </summary>
    public class CustomerBusiness : ABusiness
    {

        private readonly CustomerDao _customerDao;
        private readonly SequenceBusiness _sequenceBusiness;
        private readonly ChannelDao _channelDao;
        private readonly TypeBusinessDao _typeBusinessDao;
        private readonly ProductCategoryDao _productCategoryDao;
        private readonly StatusCustomerDao _statusCustomerDao;
        private readonly TypeCustomerDao _typeCustomerDao;

        public CustomerBusiness(MardisContext mardisContext) : base(mardisContext)
        {
            _customerDao = new CustomerDao(mardisContext);
            _sequenceBusiness = new SequenceBusiness(mardisContext);
            _channelDao = new ChannelDao(mardisContext);
            _typeBusinessDao = new TypeBusinessDao(mardisContext);
            _productCategoryDao = new ProductCategoryDao(mardisContext);
            _statusCustomerDao = new StatusCustomerDao(mardisContext);
            _typeCustomerDao = new TypeCustomerDao(mardisContext);
        }

        public Customer GetOne(Guid id, Guid idAccount)
        {
            return _customerDao.GetOne(id, idAccount);
        }

        public Customer GetCustomerById(Guid id, Guid idAccount)
        {
            return _customerDao.GetCustomerById(id, idAccount);
        }

        public CustomerRegisterViewModel GetNewCustomerRegisterViewModel()
        {
            var status = _statusCustomerDao.GetAllActive()
                .Select(s => new SelectListItem() { Text = s.Name, Value = s.Id.ToString() })
                .ToList();

            var types = _typeCustomerDao.GetAllActiveList()
                .Select(s => new SelectListItem() { Text = s.Name, Value = s.Id.ToString() })
                .ToList();

            return new CustomerRegisterViewModel()
            {
                StatusCustomers = status,
                Types = types
            };
        }

        public List<Customer> GetCustomersByAccount(Guid idAccount)
        {
            return _customerDao.GetCustomersByAccount(idAccount);
        }

        public List<SelectListItem> GetCustomerList(Guid idAccount)
        {
            return GetCustomersByAccount(idAccount)
                .Select(t => new SelectListItem()
                {
                    Text = t.Name,
                    Value = t.Id.ToString()
                }).ToList();
        }

        public Customer Save(CustomerRegisterViewModel model, Guid idAccount)
        {
            var customer = ConvertCustomer.FromCustomerRegisterViewModel(model);
            if (string.IsNullOrEmpty(customer.Code))
            {
                customer.Code = _sequenceBusiness.NextSequence(CCustomer.SequenceCode, idAccount).ToString();
            }
            customer.IdAccount = idAccount;
            return _customerDao.InsertOrUpdate(customer);
        }

        public Customer AddChannel(CustomerRegisterViewModel model, Guid idAccount)
        {
            var customer = Save(model, idAccount);
            var channel = new Channel()
            {
                IdAccount = idAccount,
                IdCustomer = customer.Id,
                Name = model.NewChannels,
                StatusRegister = CStatusRegister.Active
            };
            _channelDao.InsertOrUpdate(channel);
            return customer;
        }

        public void DeleteChannel(CustomerRegisterViewModel model, Guid idAccount)
        {
            Save(model, idAccount);
            var channel = _channelDao.GetOne(Guid.Parse(model.DeletedChannels), idAccount);
            channel.StatusRegister = CStatusRegister.Delete;
            _channelDao.InsertOrUpdate(channel);
        }

        public Customer AddTypeBusiness(CustomerRegisterViewModel model, Guid idAccount)
        {
            var customer = Save(model, idAccount);
            var typeBusiness = new TypeBusiness()
            {
                IdAccount = idAccount,
                IdCustomer = customer.Id,
                Name = model.NewTypeBusiness,
                StatusRegister = CStatusRegister.Active
            };

            _typeBusinessDao.InsertOrUpdate(typeBusiness);
            return customer;
        }

        public void DeleteTypeBusiness(CustomerRegisterViewModel model, Guid idAccount)
        {
            Save(model, idAccount);
            var typeBusiness = _typeBusinessDao.GetOne(Guid.Parse(model.DeletedTypeBusiness), idAccount);
            typeBusiness.StatusRegister = CStatusRegister.Delete;
            _typeBusinessDao.InsertOrUpdate(typeBusiness);
        }

        public Customer AddProductCategory(CustomerRegisterViewModel model, Guid idAccount)
        {
            var customer = Save(model, idAccount);
            var productCategory = new ProductCategory()
            {
               Name = model.NewCategories,
               Code = _sequenceBusiness.NextSequence(CProductCategory.SequenceCode,idAccount).ToString(),
               IdCustomer = customer.Id,
               StatusRegister = CStatusRegister.Active
            };

            _productCategoryDao.InsertOrUpdate(productCategory);
            return customer;
        }

        public void DeleteProductCategory(CustomerRegisterViewModel model, Guid idAccount)
        {
            Save(model, idAccount);
            var productCategory = _productCategoryDao.GetOne(Guid.Parse(model.DeletedCategories), idAccount);
            productCategory.StatusRegister = CStatusRegister.Delete;
            _typeBusinessDao.InsertOrUpdate(productCategory);
        }

        public List<Customer> GetCustomerByTypeService(Guid idTypeService, Guid idAccount)
        {
            return _customerDao.GetCustomerByTypeService(idTypeService, idAccount);
        }

        public CustomerListViewModel GetPaginatedCustomers(List<FilterValue> filters, int pageSize, int pageIndex, Guid idAccount)
        {
            var itemResult = new CustomerListViewModel();
            var customers = _customerDao.GetPaginatedCustomerList(filters, pageSize, pageIndex, idAccount);
            var countCustomers = _customerDao.GetPaginatedCustomersCount(filters, pageSize, pageIndex, idAccount);

            foreach (var customer in customers)
            {
                var cvm = new CustomerItemViewModel
                {
                    Code = customer.Code,
                    Id = customer.Id,
                    Contact = customer.Contact,
                    Email = customer.Email,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    Type = customer.TypeCustomer.Name
                };
                itemResult.ItemsList.Add(cvm);
            }

            return ConfigurePagination(itemResult, pageIndex, pageSize, filters, countCustomers);

        }

        public CustomerRegisterViewModel GetCustomer(Guid idCustomer, Guid idAccount)
        {
            var model = GetNewCustomerRegisterViewModel();
            var customer = _customerDao.GetCustomerById(idCustomer, idAccount);
            customer.Channels = _channelDao.GetChannelsByCustomerId(idCustomer, idAccount);
            customer.ProductCategories = _productCategoryDao.GetProductCategoriesByCustomer(idCustomer, idAccount);
            customer.TypeBusiness = _typeBusinessDao.GetAllTypesBusinessByIdCustomer(idCustomer, idAccount);
            var itemResult = ConvertCustomer.ToCustomerRegisterViewModel(customer);
            itemResult.Types = model.Types;
            itemResult.StatusCustomers = model.StatusCustomers;
            return itemResult;
        }
    }
}
