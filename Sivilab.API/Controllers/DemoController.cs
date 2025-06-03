using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sivilab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DemoController : ControllerBase
    {
        [Authorize(Policy = "Administrador")]
        [HttpGet("AdminEndpoint")]
        public IActionResult AdminEndpoint()
        {
            return Ok(new { Message = "Este endpoint solo es accesible para administradores." });
        }

        [Authorize(Policy = "Usuario")]
        [HttpGet("UserEndpoint")]
        public IActionResult UserEndpoint()
        {
            return Ok(new { Message = "Este endpoint es accesible para usuarios y administradores." });
        }
    }
}
