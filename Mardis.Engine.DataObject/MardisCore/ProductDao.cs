using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;
using Microsoft.EntityFrameworkCore;

namespace Mardis.Engine.DataObject.MardisCore
{
    public class ProductDao : ADao
    {
        public ProductDao(MardisContext mardisContext) : base(mardisContext)
        {
        }

        /// <summary>
        /// Obtiene un producto por su Identificador
        /// </summary>
        /// <param name="idProduct">Identificadotr del Producto</param>
        /// <returns>Producto</returns>
        public Product GetProductById(Guid idProduct)
        {
            return Context.Products
                .Include(p => p.ProductCategory)
                .FirstOrDefault(p => p.Id == idProduct &&
                                     p.StatusRegister == CStatusRegister.Active);
        }

        /// <summary>
        /// Obtiene el listado de Productos por cliente
        /// </summary>
        /// <param name="idCustomer">Identificador de cliente</param>
        /// <param name="idAccount">Identificador de cuenta</param>
        /// <returns>Listado de Productos por clientes</returns>
        public List<Product> GetProductListByCustomer(Guid idCustomer, Guid idAccount)
        {
            return Context.Products
                .Where(p => p.IdCustomer == idCustomer &&
                            p.StatusRegister == CStatusRegister.Active &&
                            p.IdAccount == idAccount)
                .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idProductCategory"></param>
        /// <param name="idAccount"></param>
        /// <returns></returns>
        public List<Product> GetProductsByCategory(Guid idProductCategory, Guid idAccount)
        {
            return Context.Products
                          .Where(tb => tb.IdAccount == idAccount &&
                                 tb.IdProductCategory == idProductCategory &&
                                 tb.StatusRegister == CStatusRegister.Active)
                          .ToList();
        }
    }
}
