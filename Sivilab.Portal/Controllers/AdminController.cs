using Microsoft.AspNetCore.Mvc;
using Sivilab.Portal.Models;
using Sivilab.Portal.Services;
using Sivilab.Portal.Filters; // ⭐ NUEVO

namespace Sivilab.Portal.Controllers
{
    [ServiceFilter(typeof(AdminAuthFilter))] // ⭐ NUEVO: Protege todo el controlador
    public class AdminController : Controller
    {
        private readonly EventoService _eventoService;
        private readonly IWebHostEnvironment _env;

        public AdminController(EventoService eventoService, IWebHostEnvironment env)
        {
            _eventoService = eventoService;
            _env = env;
        }

        // GET: Admin
        public IActionResult Index()
        {
            var eventos = _eventoService.ObtenerTodos();
            return View(eventos);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Evento evento, IFormFile? imagenFile)
        {
            if (ModelState.IsValid)
            {
                // Subir imagen si existe
                if (imagenFile != null && imagenFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_env.WebRootPath, "Img");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + imagenFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imagenFile.CopyToAsync(fileStream);
                    }

                    evento.imagen = "/Img/" + uniqueFileName;
                }

                _eventoService.Agregar(evento);
                TempData["Success"] = "Evento creado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            return View(evento);
        }

        // GET: Admin/Edit/5
        public IActionResult Edit(int id)
        {
            var evento = _eventoService.ObtenerPorIndice(id);
            if (evento == null)
                return NotFound();

            ViewBag.IndiceEvento = id;
            return View(evento);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Evento evento, IFormFile? imagenFile)
        {
            if (ModelState.IsValid)
            {
                var eventoExistente = _eventoService.ObtenerPorIndice(id);
                if (eventoExistente == null)
                    return NotFound();

                // Si se sube nueva imagen
                if (imagenFile != null && imagenFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_env.WebRootPath, "Img");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + imagenFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imagenFile.CopyToAsync(fileStream);
                    }

                    evento.imagen = "/Img/" + uniqueFileName;
                }
                else
                {
                    // Mantener la imagen existente
                    evento.imagen = eventoExistente.imagen;
                }

                _eventoService.Actualizar(id, evento);
                TempData["Success"] = "Evento actualizado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.IndiceEvento = id;
            return View(evento);
        }

        // GET: Admin/Delete/5
        public IActionResult Delete(int id)
        {
            var evento = _eventoService.ObtenerPorIndice(id);
            if (evento == null)
                return NotFound();

            ViewBag.IndiceEvento = id;
            return View(evento);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _eventoService.Eliminar(id);
            TempData["Success"] = "Evento eliminado exitosamente";
            return RedirectToAction(nameof(Index));
        }
    }
}