using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Sivilab.Portal.Filters
{
    public class AdminAuthFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Verificar si hay sesión activa
            var isLoggedIn = context.HttpContext.Session.GetString("IsAdminLoggedIn");

            if (isLoggedIn != "true")
            {
                // Si no está logueado, redirigir al login
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // No necesitamos hacer nada después de la acción
        }
    }
}