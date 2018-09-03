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

namespace Mardis.Engine.Services.Controllers
{
    [Produces("application/json")]
    [Route("api/Canpania")]
    public class CanpaniaController : Controller
    {
        private readonly TaskCampaignBusiness _taskCampaignBusiness;
        private readonly BranchBusiness _BranchBusiness;
        private readonly CampaignBusiness _campaignBusiness;
        private ILogger _logger;
        public CanpaniaController(MardisContext mardisContext,
    RedisCache distributedCache,
    ILoggerFactory _loggerFactory)
        {
            _taskCampaignBusiness = new TaskCampaignBusiness(mardisContext, distributedCache);
            _BranchBusiness = new BranchBusiness(mardisContext);
            _campaignBusiness = new CampaignBusiness(mardisContext);
            _logger = _loggerFactory.CreateLogger("Mardis.Engine.Services");
        }
        // GET: api/Canpania
        [HttpGet]
        public object Get()
        {
            return _campaignBusiness.GetCampanigAccount();
        }

        //// GET: api/Canpania/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}
        
        // POST: api/Canpania
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Canpania/5
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
