using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisPedidos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardis.Engine.DataObject.MardisPedidos
{
    public class PedidosDao : ADao
    {
        public PedidosDao(MardisContext mardisContext) : base(mardisContext)
        {

        }
        public List<Pedidos> GetPedidos()
        {
            return Context.Pedidos.Include(a => a.PedidosItems).ToList();
        }

        public List<PedidosItems>GetPedidosItems(int pedido)
        {
            return Context.PedidosItems.Where(x => x.idPedido == pedido).ToList();
        }
        public Pedidos GetPedido( int codigo)
        {
            return Context.Pedidos.Include(a => a.PedidosItems).Where(x=>x._id==codigo).FirstOrDefault();
        }

    }
}
