namespace AuthMiddlewareExample.Controllers
{
    using Microsoft.AspNetCore.Mvc;

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