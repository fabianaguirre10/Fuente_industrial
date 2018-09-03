using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisPedidos;
using Mardis.Engine.DataObject.MardisPedidos;
namespace Mardis.Engine.Business.MardisPedidos
{
    public class PedidosBusiness
    {
        private readonly PedidosDao _pedidosDao;
        public PedidosBusiness(MardisContext mardisContext) 
        {
            _pedidosDao = new PedidosDao(mardisContext);
        }
        public List<Pedidos> GetPedidos()
        {
            return _pedidosDao.GetPedidos();
        }

        public List<PedidosItems>GetPedidosItems(int codigo)
        {
            return _pedidosDao.GetPedidosItems(codigo);
        }
        public Pedidos GetPedido( int codigo)
        {
            return _pedidosDao.GetPedido(codigo);
        }


    }
}
