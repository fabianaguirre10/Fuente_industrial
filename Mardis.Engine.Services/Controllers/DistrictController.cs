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
    [Route("api/District")]
    public class DistrictController : Controller
    {
        private readonly DistrictBusiness _districtBusiness;
        private ILogger _logger;

        public DistrictController(MardisContext mardisContext,
      IMemoryCache distributedCache,
      ILoggerFactory _loggerFactory)
        {
            _districtBusiness = new DistrictBusiness(mardisContext);
            _logger = _loggerFactory.CreateLogger("Mardis.Engine.Services");
        }
        // GET: api/District
        [HttpGet]
        public object Get()
        {
            return _districtBusiness.GetDistrict();
        }

       
        
        // POST: api/District
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/District/5
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
