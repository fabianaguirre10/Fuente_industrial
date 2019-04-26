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

namespace Mardis.Engine.Web.Controllers
{
    [Authorize]
    public class PagosController : AController<PagosController>
    {
        private readonly ILogger<PagosController> _logger;
        private readonly IDataProtector _protector;
        private readonly TaskCampaignBusiness _taskCampaignBusiness;
        private readonly PedidosBusiness _pedidosBusiness;
        private readonly PagosBusiness _pagosBusiness;
        private readonly ArticulosBusiness _articulosBusiness;

        public PagosController(UserManager<ApplicationUser> userManager,
                               IHttpContextAccessor httpContextAccessor,
                               MardisContext mardisContext,
                               ILogger<PagosController> logger,
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
            _pagosBusiness = new PagosBusiness(mardisContext);
            _articulosBusiness = new ArticulosBusiness(mardisContext);
            _taskCampaignBusiness = new TaskCampaignBusiness(mardisContext, distributedCache);
        }

        [Authorize]
        // GET: Equipment
        public IActionResult Index(string filterValues, bool deleteFilter, int pageSize = 15, int pageIndex = 1)
        {
            try
            {
                var filters = GetFilters(filterValues, deleteFilter);

                var equipments = _pagosBusiness.GetPaginatedPagosBranch(filters, pageSize, pageIndex, ApplicationUserCurrent.AccountId);

                return View(equipments);
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(0, "Error Index"), e.Message);
                return RedirectToAction("Index", "StatusCode", new { statusCode = 1 });
            }
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
                    nuevo.numero_factura =ip.numero_factura;
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
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(0, "Error Index"), e.Message);
                return RedirectToAction("Index", "StatusCode", new { statusCode = 1 });
            }

        }

        [HttpPost]
        public JsonResult Save(string poll,string Comment)
        {
            try
            {
                var model = JSonConvertUtil.Deserialize<PedidoModel>(poll);
                _pedidosBusiness.SavePedido(model,"");
                return Json(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(0, "Error Index"), ex.Message);
                return null;
            }
        }
        #region equipos Visualizador

        public IActionResult ViewPagos(int idPago, string returnUrl = null)
        {
            try
            {

                ViewData[CTask.IdPago] = idPago.ToString();


                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(0, "Error Index"), e.Message);
                return RedirectToAction("Index", "StatusCode", new { statusCode = 1 });
            }
        }


        [HttpGet]
        public JsonResult GetProfile(String IdPago)
        {
            try
            {
                var model = IdPago.Equals("") == false ? _pagosBusiness.GetPago_Profile(IdPago, ApplicationUserCurrent.AccountId) : null;

                JSonConvertUtil.Convert(model);
                if (model != null)
                {
                    // _SaveImagesAsync(IdEq);
                }
                return Json(model);
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(0, "Error Index"), e.Message);
                return null;
            }
        }
        #endregion

    }
}
