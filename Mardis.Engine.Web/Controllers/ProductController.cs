using System;
using System.Collections.Generic;
using Mardis.Engine.Business;
using Mardis.Engine.Business.MardisCore;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.Libraries.Security;
using Mardis.Engine.Web.Libraries.Services;
using Mardis.Engine.Web.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mardis.Engine.Web.Controllers
{
    public class ProductController : AController<ProductController>
    {

        #region Variables y Constructores 

        private readonly ProductBusiness productBusiness;
        private readonly ProductCategoryBusiness productCategoryBusiness;
        private readonly Guid idAccount;
        private readonly SequenceBusiness sequenceBusiness;

        public ProductController(
                                UserManager<ApplicationUser> userManager,
                                IHttpContextAccessor httpContextAccessor,
                                MardisContext mardisContext,
                                ILogger<ProductController> logger,
                                ILogger<ServicesFilterController> loggeFilter)
            : base(userManager, httpContextAccessor, mardisContext, logger)
        {
            TableName = CProduct.TableName;
            ControllerName = CProduct.Controller;
            //ServicesFilterController = new ServicesFilterController(userManager, httpContextAccessor, mardisContext, loggeFilter);
            productBusiness = new ProductBusiness(mardisContext);
            productCategoryBusiness=new ProductCategoryBusiness(mardisContext);
            idAccount = ApplicationUserCurrent.AccountId;
            sequenceBusiness = new SequenceBusiness(mardisContext);
        }

        #endregion
        
        [HttpGet]
        public string GetProductById(string idProduct)
        {
            var itemReturn = new Product();

            if (!string.IsNullOrEmpty(idProduct))
            {
                itemReturn = productBusiness.GetProductById(
                    new Guid(idProduct));
            }

            var result = JSonConvertUtil.Convert(itemReturn);

            return result;
        }
        
        [HttpGet]
        public string GetCustomerProductList(string idCustomer)
        {
            idCustomer = idCustomer.Replace("\"", string.Empty);

            return JSonConvertUtil.Convert(
                    productBusiness.GetProductListByCustomer(
                    new Guid(idCustomer), 
                    idAccount));
        }
        
        [HttpGet]
        public List<ProductCategory> GetProductCategoryList(string idCustomer)
        {
            return productCategoryBusiness.GetProductCategoriesByCustomer(
                    new Guid(idCustomer),ApplicationUserCurrent.AccountId);
        }
        
        [HttpGet]
        public IActionResult Register(string idProduct)
        {
            ViewData[CProduct.IdRegister] = idProduct;

            return View();
        }

        #region Guardar
        
        [HttpPost]
        public string SaveProduct(Product product)
        {
            product = productBusiness.SaveProduct(product, ApplicationUserCurrent.AccountId);

            return JSonConvertUtil.Convert(product);
        }

        #endregion

    }
}
