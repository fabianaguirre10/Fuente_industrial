using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mardis.Engine.Business.MardisCommon;
using Mardis.Engine.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Mardis.Engine.Services.Controllers
{
    [Produces("application/json")]
    [Route("api/Parish")]
    public class ParishController : Controller
    {
        private readonly ParishBusiness _parishBusiness;
        private ILogger _logger;

        public ParishController(MardisContext mardisContext,
      IMemoryCache distributedCache,
      ILoggerFactory _loggerFactory)
        {
            _parishBusiness = new ParishBusiness(mardisContext);
            _logger = _loggerFactory.CreateLogger("Mardis.Engine.Services");
        }
        // GET: api/Parish
        [HttpGet]
        public object Get()
        {
            return _parishBusiness.GetParish();
        }

      
        
        // POST: api/Parish
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Parish/5
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
