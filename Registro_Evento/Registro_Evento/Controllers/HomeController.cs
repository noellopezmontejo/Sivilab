using Microsoft.AspNetCore.Mvc;
using Registro_Evento.Models;
using Registro_Evento.Services;

namespace Registro_Evento.Controllers
{
    public class HomeController : Controller
    {
        private readonly CurpValidatorService _curpValidator;

        public HomeController()
        {
            _curpValidator = new CurpValidatorService();
        }

        public IActionResult Index()
        {
            return View(new CurpModel());
        }

        [HttpPost]
        public IActionResult ValidarCurp(CurpModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var resultado = _curpValidator.ValidarCurp(model.Curp);

            model.EsValida = resultado.esValida;
            model.MensajeError = resultado.mensaje;

            if (resultado.esValida && resultado.datos.Any())
            {
                // Construir información adicional
                model.Sexo = resultado.datos["sexo"];
                model.EstadoNacimiento = resultado.datos.ContainsKey("estadoNombre")
                    ? resultado.datos["estadoNombre"]
                    : "Desconocido";

                // Construir fecha de nacimiento
                if (int.TryParse(resultado.datos["año"], out int año) &&
                    int.TryParse(resultado.datos["mes"], out int mes) &&
                    int.TryParse(resultado.datos["dia"], out int dia))
                {
                    int añoCompleto = año <= DateTime.Now.Year - 2000 ? 2000 + año : 1900 + año;
                    try
                    {
                        model.FechaNacimiento = new DateTime(añoCompleto, mes, dia);
                    }
                    catch
                    {
                        model.FechaNacimiento = null;
                    }
                }

                ViewBag.Mensaje = "¡CURP válida correctamente!";
                ViewBag.TipoMensaje = "success";
            }
            else
            {
                ViewBag.Mensaje = model.MensajeError;
                ViewBag.TipoMensaje = "error";
            }

            return View("Index", model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}