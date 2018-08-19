namespace AuthMiddlewareExample.Controllers
{
    using Microsoft.AspNetCore.Mvc;

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