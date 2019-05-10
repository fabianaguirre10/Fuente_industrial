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
using System.Configuration;
using Mardis.Engine.Web;

namespace Mardis.Engine.Business.MardisPedidos
{
    public class PedidosBusiness
    {
        private readonly PedidosDao _pedidosDao;
        private Correos _correo = new Correos();
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

        public void EnvioCorreo(string estadoAnterior, string estadoActual, PedidoModel modelo)
        {
            var appSettings = ConfigurationSettings.AppSettings;

            string contenido = "";
            string estadoCambiado = "";
            string subject = "Actualización de pedido";
            string des = appSettings["correos"]; //acorrales@mardisresearch.com;
            string desFin = "";


            string pie = "<br/><br/><br/><p>Saludos Cordiales,</p>" +
                         "<img src=\'cid:imagen\' alt=\'Mardis.com\' width=\'200\' height=\'150\'>" +
                         "<p>Este correo electrónico contiene información confidencial y para uso exclusivo de la(s) persona(s) a quien(es) se dirige. Si el lector del presente correo electrónico no es el destinatario, se le notifica que cualquier distribución o copia de la misma está estrictamente prohibida. Si ha recibido este correo por error le solicitamos notificar inmediatamente a la persona que lo envió y borrarlo definitivamente de su sistema.</p> ";

            if (estadoAnterior == "D0723711-B3AB-4736-BFA9-18E0D4A9824C")//Pendiente 
            {
                if (estadoActual == "0FF1A786-A332-4252-AAEC-8AD3F2DB7BC9")//->AprobadoFacturación
                {
                    estadoCambiado = "APROBADO PARA FACTURACIÓN";
                }
                if (estadoActual == "37694A60-8499-4DC6-9A53-950814381690")//->Rechazado
                {
                    estadoCambiado = "RECHAZADO";
                }
            }

            if (estadoAnterior == "37694A60-8499-4DC6-9A53-950814381690")//Rechazado 
            {
                if (estadoActual == "0FF1A786-A332-4252-AAEC-8AD3F2DB7BC9")//-> Aprobado
                {
                    estadoCambiado = "APROBADO PARA FACTURACIÓN";
                }
            }

            if (estadoAnterior == "0FF1A786-A332-4252-AAEC-8AD3F2DB7BC9")//Aprobado Facturación
            {
                if (estadoActual == "7B0D0269-1AEF-4B73-9089-20E53698FF75")//-> Facturado
                {
                    estadoCambiado = "FACTURADO";
                }
            }

            contenido += "<h3><strong>Actualización de pedido</strong></h3><p>Ponemos en su conocimiento que el pedido No. <strong>" + modelo._id + "</strong> ha sido <strong>" + estadoCambiado + "</strong> de acuerdo al siguiente detalle:</p>" +
                    "<table border='1' style='height: 147px; width: 100 %; border - collapse: collapse; border - style: solid;'>" +
                    "<tbody><tr style='height: 21px; '>" +
                    "<td style='width: 25%; height: 21px; background-color: #B2BEB5; text-align: right;'><strong>Cliente:</strong></td>" +
                    "<td style='width: 75%; height: 21px;' colspan='3'>" + modelo.tarea.BranchName + "</td></tr><tr style='height: 21px;'>" +
                    "<td style='width: 25%; height: 21px; background-color: #B2BEB5; text-align: right;'><strong>Fecha:</strong></td>" +
                    "<td style='width: 75%; height: 21px;' colspan='3'>" + modelo.tarea.DateCreation.ToShortDateString() + "</td></tr><tr style='height: 21px;'>" +
                    "<td style='width: 25%; height: 21px; background-color: #B2BEB5; text-align: right;'><strong>Estado:</strong></td>" +
                    "<td style='width: 75%; height: 21px;' colspan='3'>" + estadoCambiado + "</td></tr><tr style='height: 21px;'>" +
                    "<td style='width: 100%; height: 21px;' colspan='4'></td></tr><tr style='height: 21px;'>" +
                    "<td style='width: 100%; background-color: #B2BEB5; height: 21px; text-align: center;' colspan='4'><strong>Detalle de valores</strong></td></tr>" +
                    "<tr style='height: 21px;'><td style='width: 25%; height: 21px; text-align: center;'><b>Código</b>" +
                    "</td><td style='width: 25%; height: 21px; text-align: center;'>" +
                    "<strong>Descripción</strong></td><td style='width: 25%; height: 21px; text-align: center;'><strong>No. Factura</strong></td>" +
                    "<td style='width: 25%; height: 21px; text-align: center;'><strong>Valor</strong></td></tr>";
            foreach (var val in modelo.PedidosItems)
            {
                contenido += "<tr style='height: 21px;'>" +
                            "<td style='width: 25%; height: 21px; text-align: center;'>" + val._id + "</td><td style='width: 25%; height: 21px; text-align: center;'>" + val.articulos.descripcion + "</td>" +
                            "<td style='width: 25%; height: 21px; text-align: center;'>" + val.numero_factura + "</td><td style='width: 25%; height: 21px; text-align: center;'>" + val.total + "</td></tr>";
            }

            contenido += "</tbody></table>";

            if (!(String.IsNullOrEmpty(modelo.comment)))
            {
                contenido += "<p><strong>Observaciones:</strong> " + modelo.comment + " </p>";
            }

            contenido += pie;


            string[] destinatarios = des.Split(',');

            foreach (string destinatario in destinatarios)
            {
                if (modelo.phone == destinatario)
                {
                    desFin += modelo.phone;
                }
            }

            
            //anloor@gnoboa.com
            //fanchundia @gnoboa.com

            //wyagual@gnoboa.com
            //dreinoso @gnoboa.com
            //rcarvajal @gnoboa.com

            _correo.enviar(subject, desFin, contenido);
        }
    }
}
