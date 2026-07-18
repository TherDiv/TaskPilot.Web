using System.Collections.Generic;
using System.Web.Mvc;
using TaskPilot.Entidades;
using TaskPilot.LogicaNegocio;

namespace TaskPilot.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        DashboardLN ln = new DashboardLN();

        public ActionResult Index()
        {
            List<Tarea> tareas = ln.ObtenerTareas();

            ViewBag.Pendientes = ln.ContarPorEstado(tareas, "Pendiente");
            ViewBag.EnProgreso = ln.ContarPorEstado(tareas, "En progreso");
            ViewBag.Completadas = ln.ContarPorEstado(tareas, "Completada");
            ViewBag.Criticas = ln.TareasCriticas(tareas);

            return View(tareas);
        }
    }
}