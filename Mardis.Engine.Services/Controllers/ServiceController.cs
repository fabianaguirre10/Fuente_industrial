using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mardis.Engine.Business.MardisCore;
using Mardis.Engine.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Mardis.Engine.Services.Controllers
{
    [Produces("application/json")]
    [Route("api/Service")]
    public class ServiceController : Controller
    {
        private readonly ServiceBusiness _serviceBusiness;
        private ILogger _logger;

        public ServiceController(MardisContext mardisContext,
      IMemoryCache distributedCache,
      ILoggerFactory _loggerFactory)
        {
            _serviceBusiness = new ServiceBusiness(mardisContext);
            _logger = _loggerFactory.CreateLogger("Mardis.Engine.Services");
        }
        // GET: api/Service
        [HttpGet]
        public object Get(Guid idAccount)
        {
            return _serviceBusiness.GetServicebyAccount(idAccount);
        }

     
        
        // POST: api/Service
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Service/5
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
