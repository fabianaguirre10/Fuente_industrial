using Microsoft.AspNetCore.Mvc;

namespace Mardis.Engine.Web.Controllers
{
    public class StatusCodeController:Controller
    {
        [HttpGet("/StatusCode/{statusCode}")]

        public IActionResult Index(int statusCode)

        {

            return View(statusCode);

        }
    }
}
