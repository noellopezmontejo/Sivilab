using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Registro_Evento.Models;

namespace Registro_Evento.Controllers
{
    public class CurpController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CurpController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Curp
        public IActionResult Index()
        {
            return View(new CurpModel());
        }

        // POST: Curp/ValidarCurp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ValidarCurp(CurpModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            // Validar formato de CURP (tu lógica existente)
            var resultadoValidacion = ValidarFormatoCurp(model.Curp);

            if (!resultadoValidacion.EsValida)
            {
                ViewBag.Mensaje = resultadoValidacion.MensajeError;
                ViewBag.TipoMensaje = "danger";
                return View("Index", model);
            }

            // Buscar si el CURP ya existe en la base de datos
            var usuarioExistente = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Curp == model.Curp);

            if (usuarioExistente != null)
            {
                // Si existe, redirigir al formulario con los datos existentes
                TempData["SuccessMessage"] = "CURP encontrada en el sistema. Puede actualizar sus datos si es necesario.";
                TempData["EsEdicion"] = true;
                return RedirectToAction("Edit", "Usuario", new { id = usuarioExistente.Id, fromCurp = true });
            }
            else
            {
                // Si no existe, redirigir al formulario de creación con la CURP prellenada
                TempData["SuccessMessage"] = "CURP válida. Por favor complete sus datos personales.";
                TempData["EsCreacion"] = true;
                TempData["CurpValidada"] = model.Curp;

                // Pasar los datos extraídos de la CURP para prellenar algunos campos
                var datosExtraidos = ExtraerDatosDeCurp(model.Curp);
                TempData["FechaNacimiento"] = datosExtraidos.FechaNacimiento?.ToString("yyyy-MM-dd");
                TempData["Sexo"] = datosExtraidos.Sexo;

                return RedirectToAction("Create", "Usuario", new { fromCurp = true });
            }
        }

        private CurpModel ValidarFormatoCurp(string curp)
        {
            var modelo = new CurpModel { Curp = curp };

            // Tu lógica de validación existente aquí
            // Ejemplo básico (reemplaza con tu lógica completa):
            if (string.IsNullOrWhiteSpace(curp) || curp.Length != 18)
            {
                modelo.EsValida = false;
                modelo.MensajeError = "La CURP debe tener exactamente 18 caracteres";
                return modelo;
            }

            // Validaciones adicionales de formato...
            // (aquí iría tu lógica completa de validación)

            // Si llega aquí, es válida
            modelo.EsValida = true;
            var datosExtraidos = ExtraerDatosDeCurp(curp);
            modelo.FechaNacimiento = datosExtraidos.FechaNacimiento;
            modelo.Sexo = datosExtraidos.Sexo;
            modelo.EstadoNacimiento = datosExtraidos.EstadoNacimiento;

            return modelo;
        }

        private CurpModel ExtraerDatosDeCurp(string curp)
        {
            var modelo = new CurpModel { Curp = curp };

            try
            {
                // Extraer fecha de nacimiento (posiciones 4-9)
                var year = int.Parse(curp.Substring(4, 2));
                var month = int.Parse(curp.Substring(6, 2));
                var day = int.Parse(curp.Substring(8, 2));

                // Determinar siglo (asumiendo que años 00-29 son 2000-2029, 30-99 son 1930-1999)
                year += year <= 29 ? 2000 : 1900;

                modelo.FechaNacimiento = new DateTime(year, month, day);

                // Extraer sexo (posición 10)
                modelo.Sexo = curp[10] == 'H' ? "Masculino" : "Femenino";

                // Extraer estado (posiciones 11-12)
                modelo.EstadoNacimiento = ObtenerEstadoPorClave(curp.Substring(11, 2));

                modelo.EsValida = true;
            }
            catch
            {
                modelo.EsValida = false;
                modelo.MensajeError = "Error al extraer datos de la CURP";
            }

            return modelo;
        }

        private string ObtenerEstadoPorClave(string clave)
        {
            var estados = new Dictionary<string, string>
            {
                {"AS", "Aguascalientes"}, {"BC", "Baja California"}, {"BS", "Baja California Sur"},
                {"CC", "Campeche"}, {"CS", "Chiapas"}, {"CH", "Chihuahua"}, {"CL", "Coahuila"},
                {"CM", "Colima"}, {"DF", "Ciudad de México"}, {"DG", "Durango"}, {"GT", "Guanajuato"},
                {"GR", "Guerrero"}, {"HG", "Hidalgo"}, {"JC", "Jalisco"}, {"MC", "Estado de México"},
                {"MN", "Michoacán"}, {"MS", "Morelos"}, {"NT", "Nayarit"}, {"NL", "Nuevo León"},
                {"OC", "Oaxaca"}, {"PL", "Puebla"}, {"QT", "Querétaro"}, {"QR", "Quintana Roo"},
                {"SP", "San Luis Potosí"}, {"SL", "Sinaloa"}, {"SR", "Sonora"}, {"TC", "Tabasco"},
                {"TS", "Tamaulipas"}, {"TL", "Tlaxcala"}, {"VZ", "Veracruz"}, {"YN", "Yucatán"},
                {"ZS", "Zacatecas"}, {"NE", "Nacido en el Extranjero"}
            };

            return estados.ContainsKey(clave) ? estados[clave] : "Estado no identificado";
        }
    }
}