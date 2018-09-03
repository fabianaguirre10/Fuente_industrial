
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisPedidos;
using Mardis.Engine.DataObject.MardisPedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.Business.MardisPedidos
{
   
    public class ArticulosBusiness
    {
        private readonly ArticuloDao _articuloDao;
        public ArticulosBusiness(MardisContext mardisContext)
        {
            _articuloDao = new ArticuloDao(mardisContext);
        }
        public List<Articulos> GetArticulos()
        {
            return _articuloDao.GetArticulos();
        }

        public Articulos GetArticulo(string codigo)
        {
            return _articuloDao.GetArticulo(codigo);
        }

    }
}
