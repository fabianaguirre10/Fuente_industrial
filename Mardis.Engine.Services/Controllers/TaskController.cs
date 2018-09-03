using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mardis.Engine.Business.MardisCore;
using Mardis.Engine.Business.MardisCommon;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mardis.Engine.DataAccess;
using Mardis.Engine.Framework;
using Mardis.Engine.Web.ViewModel.TaskViewModels;
using Mardis.Engine.DataAccess.MardisCore;
using clases;

using Microsoft.Extensions.Logging;
using Mardis.Engine.Services.Models.EngieWEB;

namespace Mardis.Engine.Services.Controllers
{
   [Route("api/Task")]
    public class TaskController : Controller
    {
        
        private readonly TaskCampaignBusiness _taskCampaignBusiness;
        private readonly BranchBusiness _BranchBusiness;
        private readonly CodigoReservadosBusiness _CodigoReservadosBusiness;

        private ILogger _logger;
        
        

        public TaskController(MardisContext mardisContext,
            RedisCache distributedCache,
            ILoggerFactory _loggerFactory)
        {
            _taskCampaignBusiness = new TaskCampaignBusiness(mardisContext, distributedCache);
            _BranchBusiness = new BranchBusiness(mardisContext);
            _CodigoReservadosBusiness = new CodigoReservadosBusiness(mardisContext);
            _logger = _loggerFactory.CreateLogger("Mardis.Engine.Services");
        }

        // GET: api/Task

        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    _logger.LogInformation(new EventId(0,"Get"),"Get");
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Task/5
        [HttpGet()]
        public object Get(Guid idAccount, string Imeid)

        {

            if (Imeid == null)
            {
                Imeid = "";
            }
           
            return _BranchBusiness.GetBranchesListAndroid(idAccount, Imeid);
          
        }
        [Route("api/getCtaCte")]
        [HttpGet()]
        public List<ItemCtaCte> getCtaCte(string cliente ,string fechaDesde ,string fechaHasta)
        {
            return null;
        }

        //[HttpGet]
        //public object Get()
        //  {
        //      Guid idAccount = new Guid("22BFD943-8047-445A-BB20-533D69D52C92");
        //  return _taskCampaignBusiness.GetBranch(idAccount);

        //  }

        // POST: api/Task
        [HttpPost]
        public void Post([FromBody]MyTaskViewModel value)
        {
        }

        // PUT: api/Task/5
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
