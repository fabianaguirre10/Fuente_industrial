using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisPedidos;
using Mardis.Engine.DataObject.MardisPedidos;
using Mardis.Engine.Web.ViewModel.PedidosViewModels;
using AutoMapper;
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

        public int SavePedido(PedidoModel _model,string comment)
        {
            //var auxModel = new PedidoModel();
            //Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<PedidoModel, Pedidos>();
            //});
            //var itemResult = new Pedidos();
            //itemResult.PedidosItems = new HashSet<PedidosItems>();
            //itemResult = Mapper.Map<Pedidos>(_model);
            return _pedidosDao._saveModelPedido(_model, comment);
        }
    }
}
