using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mardis.Engine.Business.MardisCommon;
using Mardis.Engine.Business.MardisCore;
using Mardis.Engine.DataAccess;
using Mardis.Engine.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataObject.MardisCommon;

namespace Mardis.Engine.Services.Controllers
{
    [Produces("application/json")]
    [Route("api/Codigos")]

    public class CodigosController : Controller
    {
        private readonly TaskCampaignBusiness _taskCampaignBusiness;
        private readonly BranchBusiness _BranchBusiness;
        private readonly CodigoReservadosBusiness _CodigoReservadosBusiness;
        private ILogger _logger;

        public CodigosController(MardisContext mardisContext,
      RedisCache distributedCache,
      ILoggerFactory _loggerFactory)
        {
            _taskCampaignBusiness = new TaskCampaignBusiness(mardisContext, distributedCache);
            _BranchBusiness = new BranchBusiness(mardisContext);
            _CodigoReservadosBusiness = new CodigoReservadosBusiness(mardisContext);
            _logger = _loggerFactory.CreateLogger("Mardis.Engine.Services");
        }

        // GET: api/Codigos/123456
        [HttpGet()]
        public object Get(Guid idAccounut, String imail)
        {
           
            var result= _CodigoReservadosBusiness.GetCodigoReservado(idAccounut);
            List<CodigoReservados> Lnuevo = new List<CodigoReservados>();
            if (result != null)
            {
                if (result.Count == 0)
                {
                    var resultado = _BranchBusiness.GetCountBranch(idAccounut);


                    for (int i = 1; i <= 50; i++)
                    {
                        CodigoReservados nuevo = new CodigoReservados();
                        nuevo.idAccount = idAccounut;
                        nuevo.estado = "R";
                        nuevo.Code = resultado + i;
                        nuevo.imei_id = imail;
                        Lnuevo.Add(nuevo);
                    }
                    _CodigoReservadosBusiness.SaveReservados(Lnuevo);


                }
                else
                {
                    int a = _CodigoReservadosBusiness.GetAllcodigo(idAccounut);


                    for (int i = 1; i <= 50; i++)
                    {
                        CodigoReservados nuevo = new CodigoReservados();
                        nuevo.idAccount = idAccounut;
                        nuevo.estado = "R";
                        nuevo.Code = a + i;
                        nuevo.imei_id = imail;
                        Lnuevo.Add(nuevo);
                    }
                    _CodigoReservadosBusiness.SaveReservados(Lnuevo);

                }
            }
            return Lnuevo;
          
        }

        //// GET: api/Codigos/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}
        
        // POST: api/Codigos
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Codigos/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
