using System;
using System.Collections.Generic;
using Mardis.Engine.Business;
using Mardis.Engine.Business.MardisCore;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework;
using Mardis.Engine.Web.Libraries.Security;
using Mardis.Engine.Web.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;

namespace Mardis.Engine.Web.Services
{
    [Authorize]
    public class ServicesCatalogController : AController<ServicesCatalogController>
    {
        private readonly CatalogBusiness catalogBusiness;
        private readonly CustomerBusiness customerBusiness;
        private readonly TypeBusinessBusiness typeBusinessBusiness;
        private readonly TypeServiceBusiness typeServiceBusiness;
        private readonly ServiceBusiness serviceBusiness;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly StatusCampaignBusiness statusCampaignBusiness;
        private IMemoryCache cache;

        public ServicesCatalogController(UserManager<ApplicationUser> userManager,
                                         IHttpContextAccessor httpContextAccessor,
                                         MardisContext mardisContext,
                                         ILogger<ServicesCatalogController> logger,
                                         IMemoryCache memoryCache)
            : base(userManager, httpContextAccessor, mardisContext, logger)
        {
            cache = memoryCache;
            catalogBusiness = new CatalogBusiness(mardisContext);
            customerBusiness = new CustomerBusiness(mardisContext);
            typeBusinessBusiness = new TypeBusinessBusiness(mardisContext);
            serviceBusiness = new ServiceBusiness(mardisContext);
            statusCampaignBusiness = new StatusCampaignBusiness(mardisContext,memoryCache);
            typeServiceBusiness = new TypeServiceBusiness(mardisContext);
            this.userManager = userManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetStatusCustomers()
        {

            return JsonConvert.SerializeObject(catalogBusiness.GetStatusCustomers());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetTypesCustomers()
        {

            return JsonConvert.SerializeObject(catalogBusiness.GetTypeCustomers());
        }

        

        /// <summary>
        /// Este método devuelve todos los tipos de negocio del cliente
        /// </summary>
        /// <param name="idCustomer">Id del cliente</param>
        /// <returns></returns>
        [HttpGet]
        public List<TypeBusiness> GetAllTypeBusinessByCustomerId(Guid idCustomer)
        {
            return typeBusinessBusiness.GetAllTypesBusinessByIdCustomer(idCustomer,ApplicationUserCurrent.AccountId);
        }

        /// <summary>
        /// Este método obtiene todos los clientes creados con la cuenta del usuario actual
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Customer> GetAllCustomersByCurrentAccount()
        {
            Guid idAccount = base.ApplicationUserCurrent.AccountId;
            return customerBusiness.GetCustomersByAccount(idAccount);
        }

        /// <summary>
        /// Obtiene todos los estados disponibles para las campañas
        /// </summary>
        /// <returns>Listado de Campañas</returns>
        [HttpGet]
        public List<StatusCampaign> GetAllStatusCampaigns()
        {
            return statusCampaignBusiness.GetStatusCampaigns();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetAllTypeService()
        {
            return JSonConvertUtil.Convert(typeServiceBusiness.GetAll());
        }

    }
}
