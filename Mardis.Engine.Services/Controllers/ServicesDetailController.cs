using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Mardis.Engine.Business.MardisCore;
using Mardis.Engine.DataAccess;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Mardis.Engine.Services.Controllers
{
    [Produces("application/json")]
    [Route("api/ServicesDetail")]
    public class ServicesDetailController : Controller
    {
        private readonly ServiceDetailBusiness _serviceDetailBusiness;
        private readonly QuestionBusiness _questionBusiness;
        private readonly QuestionDetailBusiness _qestionDetailBusiness;
        private readonly TaskCampaignBusiness _taskCampaignBusiness;
        private ILogger _logger;

        public ServicesDetailController(MardisContext mardisContext,
      ILoggerFactory _loggerFactory,
        RedisCache distributedCache)
        {
            _serviceDetailBusiness = new ServiceDetailBusiness(mardisContext);
            _questionBusiness = new QuestionBusiness(mardisContext);
            _qestionDetailBusiness = new QuestionDetailBusiness(mardisContext);
            _taskCampaignBusiness= new TaskCampaignBusiness(mardisContext, distributedCache);
            _logger = _loggerFactory.CreateLogger("Mardis.Engine.Services");
        }
        // GET: api/ServicesDetail
        [HttpGet]
        public JsonResult Get(Guid idCampaign, Guid idAccount)
        {
            var model = _taskCampaignBusiness.GetSectionsPollService(idCampaign, idAccount);
            //JSonConvertUtil.Convert(model);
            return Json(model);


         
        }

 
        
      
        
        // PUT: api/ServicesDetail/5
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
