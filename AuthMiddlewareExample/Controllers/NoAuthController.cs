using Microsoft.AspNetCore.Mvc;

namespace AuthMiddlewareExample.Controllers
{
    [Route("[controller]")]
    public class NoAuthController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("I skip the middleware");
        }
    }
}