using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Web.ViewModel.CustomerViewModels;

namespace Mardis.Engine.Converter
{
    public class ConvertCustomer
    {
        public static CustomerRegisterViewModel ToCustomerRegisterViewModel(Customer customer)
        {
            return new CustomerRegisterViewModel()
            {
                Id = customer.Id,
                Abbreviation = customer.Abbreviation,
                Channels = customer.Channels,
                Code = customer.Code,
                Name = customer.Name,
                Contact = customer.Contact,
                Email = customer.Email,
                DateCreation = customer.DateCreation,
                Phone = customer.Phone,
                IdTypeCustomer = customer.IdTypeCustomer,
                IdStatusCustomer = customer.IdStatusCustomer,
                ProductCategories = customer.ProductCategories,
                TypeBusinessList = customer.TypeBusiness/*,
                NewChannels = customer.Channels.Aggregate(string.Empty, (x, y) => x + (";" + y.Name)),
                NewTypeBusiness = customer.TypeBusiness.Aggregate(string.Empty, (x, y) => x + (";" + y.Name)),
                NewCategories = customer.ProductCategories.Aggregate(string.Empty, (x, y) => x + (";" + y.Name))*/
            };
        }

        public static Customer FromCustomerRegisterViewModel(CustomerRegisterViewModel model)
        {
            return new Customer()
            {
                Id = model.Id,
                Abbreviation = model.Abbreviation,
                Channels = (List<Channel>)model.Channels,
                Code = model.Code,
                Name = model.Name,
                Contact = model.Contact,
                Email = model.Email,
                DateCreation = model.DateCreation,
                Phone = model.Phone,
                StatusRegister = "A",
                IdTypeCustomer = model.IdTypeCustomer,
                IdStatusCustomer = model.IdStatusCustomer,
                ProductCategories = model.ProductCategories,
                TypeBusiness = model.TypeBusinessList
            };
        }
    }
}
