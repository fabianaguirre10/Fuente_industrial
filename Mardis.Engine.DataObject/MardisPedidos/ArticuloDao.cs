using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisPedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.DataObject.MardisPedidos
{
    public class ArticuloDao : ADao
    {
        public ArticuloDao(MardisContext mardisContext) : base(mardisContext)
        {

        }
        public List<Articulos> GetArticulos()
        {
            return Context.Articulos.ToList();
        }

        public Articulos GetArticulo(string codigo)
        {
            return Context.Articulos.Where(x => x.idArticulo.ToString().Trim().Equals(codigo.Trim())).FirstOrDefault();
        }

    }
}
