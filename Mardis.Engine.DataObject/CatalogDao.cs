using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework.Resources;

namespace Mardis.Engine.DataObject
{
    public class CatalogDao : ADao
    {
        public CatalogDao(MardisContext mardisContext) 
            : base(mardisContext)
        {

        }

        /// <summary>
        /// Dame todos los estados de Clientes
        /// </summary>
        /// <returns></returns>
        public List<StatusCustomer> GetStatusCustomers() {

            var itemReturn = Context.StatusCustomers
                                    .Where(tb => tb.StatusRegister == CStatusRegister.Active)
                                    .OrderBy(tb => tb.Name)
                                    .ToList();

            return itemReturn;
        }

        /// <summary>
        /// Dame tipo de clientes
        /// </summary>
        /// <returns></returns>
        public List<TypeCustomer> GetTypeCustomers()
        {

            var itemReturn = Context.TypesCustomers
                                    .Where(tb => tb.StatusRegister == CStatusRegister.Active)
                                    .OrderBy(tb => tb.Name)
                                    .ToList();

            return itemReturn;
        }
    }
}
