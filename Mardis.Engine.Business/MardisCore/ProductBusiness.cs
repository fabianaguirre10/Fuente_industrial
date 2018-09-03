using System;
using System.Collections.Generic;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataObject.MardisCore;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.Business.MardisCore
{
    public class ProductBusiness : ABusiness
    {
        readonly ProductDao _productDao;
        readonly SequenceBusiness _sequenceBusiness;

        public ProductBusiness(MardisContext mardisContext) : base(mardisContext)
        {
            _productDao = new ProductDao(mardisContext);
            _sequenceBusiness = new SequenceBusiness(mardisContext);
        }

        /// <summary>
        /// Obtiene un producto por su Identificador
        /// </summary>
        /// <param name="idProduct">Identificadotr del Producto</param>
        /// <returns>Producto</returns>
        public Product GetProductById(Guid idProduct)
        {
            return _productDao.GetProductById(idProduct);
        }

        /// <summary>
        /// Obtiene el listado de Productos por cliente
        /// </summary>
        /// <param name="idCustomer">Identificador de cliente</param>
        /// <param name="idAccount">Identificador de cuenta</param>
        /// <returns>Listado de Productos por clientes</returns>
        public List<Product> GetProductListByCustomer(Guid idCustomer, Guid idAccount)
        {
            return _productDao.GetProductListByCustomer(idCustomer, idAccount);
        }

        /// <summary>
        /// Dame Productos por Categoria
        /// </summary>
        /// <param name="idProductCategory"></param>
        /// <param name="idAccount"></param>
        /// <returns></returns>
        public List<Product> GetProductsByCategory(Guid idProductCategory, Guid idAccount)
        {
            return _productDao.GetProductsByCategory(idProductCategory, idAccount);
        }

        /// <summary>
        /// Guardar Productos
        /// </summary>
        /// <param name="product"></param>
        /// <param name="idAccount"></param>
        public Product SaveProduct(Product product, Guid idAccount)
        {
            product.ProductCategory = null;
            product.Account = null;
            product.Customer = null;

            using (var transaction = Context.Database.BeginTransaction())
            {

                try
                {
                    if (string.IsNullOrEmpty(product.Code))
                    {
                        var nextSequence = _sequenceBusiness.NextSequence(CProduct.SequenceCode, idAccount);

                        product.Code = nextSequence.ToString();
                    }

                    product.IdAccount = idAccount;
                    product.StatusRegister = CStatusRegister.Active;

                    var stateRegsiter = EntityState.Added;

                    if (Guid.Empty != product.Id)
                    {
                        stateRegsiter = EntityState.Modified;
                    }

                    Context.Products.Add(product);
                    Context.Entry(product).State = stateRegsiter;

                    Context.SaveChanges();

                    transaction.Commit();

                    product = _productDao.GetProductById(product.Id);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    product = null;
                }
            }

            return product;
        }
    }
}
