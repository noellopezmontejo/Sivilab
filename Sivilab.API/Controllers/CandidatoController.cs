using Microsoft.AspNetCore.Mvc;

namespace Sivilab.API.Controllers
{
    public class CandidatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
