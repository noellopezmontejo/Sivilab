using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Sivilab.Portal.Controllers
{
    public class LoginController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public LoginController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // GET: Login
        public IActionResult Index()
        {
            // Si ya está logueado, redirigir al admin
            if (HttpContext.Session.GetString("IsAdminLoggedIn") == "true")
            {
                return RedirectToAction("Index", "Admin");
            }

            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            // Leer el archivo JSON
            var filePath = Path.Combine(_env.WebRootPath, "data", "adminuser.json");

            if (!System.IO.File.Exists(filePath))
            {
                TempData["Error"] = "Error: Archivo de configuración no encontrado";
                return View("Index");
            }

            var jsonData = System.IO.File.ReadAllText(filePath);
            var adminUser = JsonSerializer.Deserialize<AdminUser>(jsonData);

            // Validar credenciales
            if (adminUser != null &&
                adminUser.Username == username &&
                adminUser.Password == password)
            {
                // Crear sesión
                HttpContext.Session.SetString("IsAdminLoggedIn", "true");
                HttpContext.Session.SetString("AdminUsername", username);

                return RedirectToAction("Index", "Admin");
            }

            TempData["Error"] = "Usuario o contraseña incorrectos";
            return View("Index");
        }

        // GET: Logout
        public IActionResult Logout()
        {
            // Limpiar la sesión
            HttpContext.Session.Clear();
            TempData["Success"] = "Has cerrado sesión correctamente";
            return RedirectToAction("Index");
        }
    }

    // Clase auxiliar para deserializar el JSON
    public class AdminUser
    {
        [System.Text.Json.Serialization.JsonPropertyName("username")]
        public string Username { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("password")]
        public string Password { get; set; }
    }
}