using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mardis.Engine.Web.ViewModel.TaskViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mardis.Engine.Services.Controllers
{
    [Produces("application/json")]
    [Route("api/TaskV2")]
    public class TaskV2Controller : Controller
    {
        // GET: api/TaskV2
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //// GET: api/TaskV2/5
        //[HttpGet("{id}/{idAccount}", Name = "Get")]
        //public MyTaskViewModel Get(Guid id, Guid idAccount)
        //{
        //    return new MyTaskViewModel();
        //}
        
        // POST: api/TaskV2
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/TaskV2/5
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
