using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisPedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.DataObject.MardisPedidos
{
   public class ChequePagosDao : ADao
    {
        public ChequePagosDao(MardisContext mardisContext) : base(mardisContext)
        {
           
        }
        public List<ChequePagos> GetPagos(int idpago)
        {
            return Context.ChequePagos.Where(x=> x.idpago.Equals(idpago)).ToList();
        }
    }
}
