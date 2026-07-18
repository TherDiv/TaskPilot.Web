using System.Web.Mvc;
using System.Web.Routing;
using TaskPilot.Entidades;

namespace TaskPilot.Web.Filters
{
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var usuario = filterContext.HttpContext.Session["Usuario"] as Usuario;

            // No hay sesión -> al login
            if (usuario == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "controller", "Login" },
                        { "action", "Index" }
                    });
                return;
            }

            // Sesión válida pero no es ADMIN -> acceso denegado
            if (usuario.Rol != "ADMIN")
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "controller", "Home" },
                        { "action", "AccesoDenegado" }
                    });
            }
        }
    }
}