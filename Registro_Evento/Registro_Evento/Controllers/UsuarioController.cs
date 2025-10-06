using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Registro_Evento.Models;

namespace Registro_Evento.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Usuario.ToListAsync();
            return View(usuarios);
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuario/Create
        public IActionResult Create(bool fromCurp = false)
        {
            var usuario = new Usuario();

            if (fromCurp && TempData["CurpValidada"] != null)
            {
                // Prellenar con datos de la CURP validada
                usuario.Curp = TempData["CurpValidada"].ToString();

                // Si hay fecha de nacimiento extraída de la CURP
                if (TempData["FechaNacimiento"] != null &&
                    DateTime.TryParse(TempData["FechaNacimiento"].ToString(), out DateTime fechaNac))
                {
                    var edad = DateTime.Now.Year - fechaNac.Year;
                    if (DateTime.Now.DayOfYear < fechaNac.DayOfYear) edad--;
                    usuario.Edad = edad;
                }

                ViewBag.FromCurp = true;
                ViewBag.TipoOperacion = "Registro Nuevo";

                // Mantener TempData para el POST
                TempData.Keep("CurpValidada");
                TempData.Keep("FechaNacimiento");
            }

            return View(usuario);
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Curp,Edad,Escolaridad,Email,Telefono")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Usuario registrado exitosamente en el evento";

                    // Redirigir a una página de confirmación o al inicio
                    return RedirectToAction("ConfirmacionRegistro", new { id = usuario.Id });
                }
                catch (DbUpdateException ex)
                {
                    // Manejar errores de duplicado (CURP o Email únicos)
                    if (ex.InnerException?.Message.Contains("IX_Usuario_Curp") == true)
                    {
                        ModelState.AddModelError("Curp", "Ya existe un usuario con este CURP");
                    }
                    else if (ex.InnerException?.Message.Contains("IX_Usuario_Email") == true)
                    {
                        ModelState.AddModelError("Email", "Ya existe un usuario con este email");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error al guardar el usuario. Intente nuevamente.");
                    }
                }
            }

            ViewBag.FromCurp = TempData["CurpValidada"] != null;
            ViewBag.TipoOperacion = "Registro Nuevo";
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id, bool fromCurp = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            if (fromCurp)
            {
                ViewBag.FromCurp = true;
                ViewBag.TipoOperacion = "Actualizar Datos";
                ViewBag.MensajeInfo = "Sus datos están registrados. Puede actualizarlos si es necesario.";
            }

            return View(usuario);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Curp,Edad,Escolaridad,Email,Telefono")] Usuario usuario, bool fromCurp = false)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();

                    if (fromCurp)
                    {
                        TempData["SuccessMessage"] = "Sus datos han sido actualizados exitosamente";
                        return RedirectToAction("ConfirmacionRegistro", new { id = usuario.Id });
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Usuario actualizado exitosamente";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException?.Message.Contains("IX_Usuario_Curp") == true)
                    {
                        ModelState.AddModelError("Curp", "Ya existe un usuario con este CURP");
                    }
                    else if (ex.InnerException?.Message.Contains("IX_Usuario_Email") == true)
                    {
                        ModelState.AddModelError("Email", "Ya existe un usuario con este email");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error al actualizar el usuario. Intente nuevamente.");
                    }

                    ViewBag.FromCurp = fromCurp;
                    ViewBag.TipoOperacion = fromCurp ? "Actualizar Datos" : "Editar Usuario";
                    return View(usuario);
                }
            }

            ViewBag.FromCurp = fromCurp;
            ViewBag.TipoOperacion = fromCurp ? "Actualizar Datos" : "Editar Usuario";
            return View(usuario);
        }

        // GET: Usuario/ConfirmacionRegistro/5
        public async Task<IActionResult> ConfirmacionRegistro(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuario.Remove(usuario);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Usuario eliminado exitosamente";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }

        // Método para validar CURP único vía AJAX
        [HttpPost]
        public async Task<IActionResult> ValidarCurp(string curp, int? id)
        {
            var existe = await _context.Usuario
                .AnyAsync(u => u.Curp == curp && (id == null || u.Id != id));

            return Json(!existe);
        }

        // Método para validar Email único vía AJAX
        [HttpPost]
        public async Task<IActionResult> ValidarEmail(string email, int? id)
        {
            var existe = await _context.Usuario
                .AnyAsync(u => u.Email == email && (id == null || u.Id != id));

            return Json(!existe);
        }
    }
}