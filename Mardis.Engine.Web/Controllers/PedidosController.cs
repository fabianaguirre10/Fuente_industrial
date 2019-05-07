using System;
using System.Collections.Generic;
using System.Linq;
using Mardis.Engine.Business;
using Mardis.Engine.Business.MardisCore;
using Mardis.Engine.Business.MardisSecurity;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework;
using Mardis.Engine.Framework.Resources.PagesConstants;
using Mardis.Engine.Web.Libraries.Security;
using Mardis.Engine.Web.Libraries.Services;
using Mardis.Engine.Web.Model;
using Mardis.Engine.Web.ViewModel;
using Mardis.Engine.Web.ViewModel.CampaignViewModels;
using Mardis.Engine.Web.ViewModel.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;
using System.Net.Http.Headers;
using Mardis.Engine.Business.MardisCore;
using Mardis.Engine.Business.MardisPedidos;
using Mardis.Engine.Web.ViewModel.PedidosViewModels;
using System.Configuration;
using Mardis.Engine.Web.Services;



namespace Mardis.Engine.Web.Controllers
{
    [Authorize]
    public class PedidosController : AController<PedidosController>
    {
        private readonly ILogger<PedidosController> _logger;
        private readonly IDataProtector _protector;
        private readonly TaskCampaignBusiness _taskCampaignBusiness;
        private readonly PedidosBusiness _pedidosBusiness;
        private readonly ArticulosBusiness _articulosBusiness;      
        private readonly StatusTaskBusiness _statusTaskBusiness;
        //private readonly EngineDataContext _context = new EngineDataContext();
        private Correos _correo = new Correos();
        public PedidosController(UserManager<ApplicationUser> userManager,
                               IHttpContextAccessor httpContextAccessor,
                               MardisContext mardisContext,
                               ILogger<PedidosController> logger,
                               ILogger<ServicesFilterController> loggeFilter,
                               IDataProtectionProvider protectorProvider,
                               IMemoryCache memoryCache,
                               RedisCache distributedCache, IHostingEnvironment envrnmt)
           : base(userManager, httpContextAccessor, mardisContext, logger)
        {
            _protector = protectorProvider.CreateProtector(GetType().FullName);
            _logger = logger;
            ControllerName = CTask.Controller;
            TableName = CTask.TableName;
            _pedidosBusiness = new PedidosBusiness(mardisContext);
            _articulosBusiness = new ArticulosBusiness(mardisContext);
            _statusTaskBusiness = new StatusTaskBusiness(mardisContext, distributedCache);
            _taskCampaignBusiness = new TaskCampaignBusiness(mardisContext, distributedCache);
        }

        [HttpGet]
        public JsonResult Get(Guid idTask)
        {
            try
            {
                var task = _taskCampaignBusiness.GetSectionsPollPedidos(idTask);
                PedidoModel pedidomodelo = new PedidoModel();
                List<PedidoItemsModels> pedidoItemsModels = new List<PedidoItemsModels>();
                var model1 = _pedidosBusiness.GetPedido(Convert.ToInt32(task.TaskCode));
                pedidomodelo._id = model1._id;
                pedidomodelo.codCliente = model1.codCliente;
                pedidomodelo.fecha = model1.fecha;
                pedidomodelo.idVendedor = model1.idVendedor;
                pedidomodelo.totalNeto = model1.totalNeto;
                pedidomodelo.totalFinal = model1.totalFinal;
                pedidomodelo.transferido = model1.transferido;
                pedidomodelo.gpsX = model1.gpsX;
                pedidomodelo.gpsY = model1.gpsY;
                pedidomodelo.facturar = model1.facturar;
                pedidomodelo.IdStatusTask = task.IdStatusTask.ToString();
                pedidomodelo.comment = task.CommentTaskNotImplemented;
                var detalleitems = _pedidosBusiness.GetPedidosItems(model1._id);
                pedidoItemsModels = new List<PedidoItemsModels>();
                foreach (var ip in detalleitems)
                {
                    PedidoItemsModels nuevo = new PedidoItemsModels();
                    nuevo._id = ip._id;
                    nuevo.idPedido = ip.idPedido;
                    nuevo.idArticulo = ip.idArticulo;
                    nuevo.cantidad = ip.cantidad;
                    nuevo.importeUnitario = ip.importeUnitario;
                    nuevo.porcDescuento = ip.porcDescuento;
                    nuevo.total = ip.total;
                    nuevo.transferido = ip.transferido;
                    nuevo.ppago = ip.ppago;
                    nuevo.nespecial = ip.nespecial;
                    nuevo.articulos = _articulosBusiness.GetArticulo(nuevo.idArticulo);
                    nuevo.numero_factura = ip.numero_factura;
                    nuevo.FormaPago = ip.formapago;
                    nuevo.unidad = ip.unidad;
                    pedidoItemsModels.Add(nuevo);
                }
                pedidomodelo.PedidosItems = pedidoItemsModels;
                pedidomodelo.tarea = task;
                var model = pedidomodelo;
                return Json(model);
            }

            catch (Exception e)
            {
                _logger.LogError(new EventId(0, "Error Index"), e.Message);
                return null;
            }
        }
        [HttpGet]
        public IActionResult Profile(Guid idTask)
        {

            try
            {
                ViewData[CTask.IdRegister] = idTask.ToString();
                LoadSelectItems();
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(0, "Error Index"), e.Message);
                return RedirectToAction("Index", "StatusCode", new { statusCode = 1 });
            }

        }
        public void LoadSelectItems()
        {
            ViewBag.StatusList = _statusTaskBusiness.GetAllStatusTasks(ApplicationUserCurrent.AccountId, Guid.Parse(ApplicationUserCurrent.UserId))
                .Select(s => new SelectListItem() { Text = s.Name, Value = s.Id.ToString() })
                    .ToList();


        }
        [HttpPost]
        public JsonResult Save(string poll, string comment)
        {
            try
            {
                var model = JSonConvertUtil.Deserialize<PedidoModel>(poll);
                var _comment = JSonConvertUtil.Deserialize<string>(comment);
                
                var idStatusTaskPrevio = model.tarea.IdStatusTask.ToString();
                
                _pedidosBusiness.SavePedido(model, _comment);

                if (idStatusTaskPrevio != model.IdStatusTask)
                {
                    _pedidosBusiness.EnvioCorreo(idStatusTaskPrevio.ToUpper(),model.IdStatusTask.ToUpper(),model);
                }

                return Json(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(0, "Error Index"), ex.Message);
                return null;
            }
        }
    }
}
