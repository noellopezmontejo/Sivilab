using Microsoft.AspNetCore.Mvc;

namespace Sivilab.Portal.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
