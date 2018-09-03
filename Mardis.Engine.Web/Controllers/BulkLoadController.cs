using System;
using System.Net;
using Mardis.Engine.Business.MardisCore;
using Mardis.Engine.DataAccess;
using Mardis.Engine.Framework;
using Mardis.Engine.Framework.Resources;
using Mardis.Engine.Web.Libraries.Security;
using Mardis.Engine.Web.Libraries.Util;
using Mardis.Engine.Web.Model;
using Mardis.Engine.Web.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Mardis.Engine.Web.Controllers
{
    /// <summary>
    /// Controlador Bulk Load
    /// </summary>
    [Authorize]
    public class BulkLoadController : AController<BulkLoadController>
    {
        private readonly BulkLoadBusiness _bulkLoadBusiness;
        private readonly BulkLoadCatalogBusiness _bulkLoadCatalogBusiness;

        public BulkLoadController(UserManager<ApplicationUser> userManager,
                                  IHttpContextAccessor httpContextAccessor,
                                  MardisContext mardisContext,
                                  ILogger<BulkLoadController> logger)
            : base(userManager, httpContextAccessor, mardisContext, logger)
        {
            _bulkLoadBusiness = new BulkLoadBusiness(mardisContext, Startup.Configuration.GetConnectionString("DefaultConnection"));
            _bulkLoadCatalogBusiness = new BulkLoadCatalogBusiness(mardisContext);

        }

        /// <summary>
        /// View de índice
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {

            ViewData[CBulkLoad.Catalog] = _bulkLoadCatalogBusiness.GetLoadCatalog();

            return View();
        }

        /// <summary>
        /// Dame Resultados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetResults()
        {
            Guid idAccount = ApplicationUserCurrent.AccountId;

            var itemsResults = _bulkLoadBusiness.GetDataByAccount(idAccount);

            return JSonConvertUtil.Convert(itemsResults);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetBulkLoadById(string inputProcess)
        {
            Guid idProcess = new Guid(inputProcess);

            return JSonConvertUtil.Convert(_bulkLoadBusiness.GetOne(idProcess));
        }

        /// <summary>
        /// View Cargar Archivo
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult LoadFile(string idBulkCatalog)
        {
            SharedMemory.Remove(CBulkLoad.CSessionFile);

            ViewData[CBulkLoad.SelectCatalog] = _bulkLoadCatalogBusiness.GetOne(new Guid(idBulkCatalog));

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idBulkCatalog"></param>
        /// <param name="nameFile"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ResumeBulkLoad(string idBulkCatalog, string nameFile)
        {
            ViewData[CBulkLoad.SelectCatalog] = idBulkCatalog;
            ViewData[CBulkLoad.CFileName] = nameFile;

            return View();
        }

        /// <summary>
        /// Iniciar proceso
        /// </summary>
        /// <param name="inputBulkCatalog"></param>
        /// <param name="characteristBulk"></param>
        /// <param name="fileName"></param>
        [HttpPost]
        public Guid InitProcess(string inputBulkCatalog, string characteristBulk, string fileName)
        {
            var idAccount = ApplicationUserCurrent.AccountId;
            var idBulkCatalogo = new Guid(inputBulkCatalog);
            var bufferFile = (byte[])SharedMemory.Get(CBulkLoad.CSessionFile);

            var isValidProcess = _bulkLoadBusiness.ProcessFile(idAccount, idBulkCatalogo,
                characteristBulk, fileName,
                bufferFile);

            return isValidProcess;
        }


        /// <summary>
        /// Descargar Archivos
        /// </summary>
        [HttpPost]
        public ContentResult DownloadFile(string idBulkCatalog)
        {
            var oneBulkCatalog = _bulkLoadCatalogBusiness.GetOne(new Guid(idBulkCatalog));
            string returnValue = string.Empty;
            int? statusCode = (int)HttpStatusCode.OK;

            foreach (var fileTemp in Request.Form.Files)
            {
                var fileName = fileTemp.FileName;
                int indexSeparator = fileName.LastIndexOf(".", StringComparison.Ordinal);

                //validar extensión de archivo
                if (0 < indexSeparator)
                {
                    if (!CBulkLoad.Extention.Equals(fileName.Substring(indexSeparator + 1).ToUpper()))
                    {
                        returnValue = "Archivo no válido, extensión no válida";
                        statusCode = (int)HttpStatusCode.NotAcceptable;
                        break;
                    }
                }
                else
                {
                    returnValue = "Archivo no válido, no hay extensión";
                    statusCode = (int)HttpStatusCode.NotAcceptable;
                    break;
                }

                //validacion de contenido
                if (1 < fileTemp.Length)
                {
                    byte[] bufferDocument;
                    returnValue = BulkLoadUtil.ValidateContentBulkFile(fileTemp, oneBulkCatalog.Separator
                                                                       , oneBulkCatalog.ColumnNumber, out bufferDocument);

                    if (string.IsNullOrEmpty(returnValue))
                    {

                        SharedMemory.Set(CBulkLoad.CSessionFile, bufferDocument);

                        returnValue = "Archivo válido";
                        statusCode = (int)HttpStatusCode.OK;
                        break;
                    }

                    statusCode = (int)HttpStatusCode.NotAcceptable;
                    break;
                }

                returnValue = "Error, Archivo Vacio";
                statusCode = (int)HttpStatusCode.NotAcceptable;
            }

            var response = new ContentResult
            {
                Content = returnValue,
                StatusCode = statusCode
            };

            return response;
        }
    }

}
