using Microsoft.AspNetCore.Mvc;

namespace Sivilab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DemoController : ControllerBase
    {
        [HttpGet("Protected")]
        public IActionResult ProtectedEndpoint()
        {
            var userId = HttpContext.Items["User"];
            return Ok($"Este endpoint está protegido. Usuario ID: {userId}");
        }
    }
}
