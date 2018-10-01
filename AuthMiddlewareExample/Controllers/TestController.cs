using Microsoft.AspNetCore.Mvc;

namespace AuthMiddlewareExample.Controllers
{
    [Route("[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("I Hit the middleware");
        }
    }
}