using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mardis.Engine.Business.MardisCommon;
using Mardis.Engine.DataAccess;
using Mardis.Engine.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Mardis.Engine.Services.Controllers
{
    [Produces("application/json")]
    [Route("api/Province")]
    public class ProvinceController : Controller
    {
        private readonly ProvinceBusiness _provinceBusiness;
        private ILogger _logger;

        public ProvinceController(MardisContext mardisContext,
      IMemoryCache distributedCache,
      ILoggerFactory _loggerFactory)
        {
            _provinceBusiness = new ProvinceBusiness(mardisContext, distributedCache);
            _logger = _loggerFactory.CreateLogger("Mardis.Engine.Services");
        }
        // GET: api/Common
        // GET: api/Province
        [HttpGet]
        public object Get()
        {
            Guid idCountry = new Guid("BE7CF5FF-296B-464D-82FA-EF0B4F48721B");
            return _provinceBusiness.GetProvincesByCountry(idCountry);
        }

        // GET: api/Province/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Province
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Province/5
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
