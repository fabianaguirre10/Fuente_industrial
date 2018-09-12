using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisPedidos;
using Mardis.Engine.Web.ViewModel.PedidosViewModels;
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
        public int _saveModelPedido(PedidoModel _model)
        {


            var _modelPedidos = Context.Pedidos.Include(t => t.PedidosItems).Where(x => x._id == _model._id).ToList();
            foreach (var DataPedido in _modelPedidos.First().PedidosItems)
            {
                DataPedido.numero_factura = _model.PedidosItems.Where(z => z._id == DataPedido._id).Select(s => s.numero_factura).First();
            }

            Context.Pedidos.UpdateRange(_modelPedidos);
            Context.SaveChanges();

            var task = Context.TaskCampaigns.Where(x => x.Code == _model._id.ToString()).ToList();
            task.First().IdStatusTask = Guid.Parse("7B0D0269-1AEF-4B73-9089-20E53698FF75");
            task.First().DateModification = DateTime.Now;
            Context.TaskCampaigns.UpdateRange(task);
            Context.SaveChanges();
            return 1;
        }
    }
}
