using System.Web.Mvc;
using TaskPilot.Entidades;
using TaskPilot.LogicaNegocio;
using TaskPilot.Web.Filters;

namespace TaskPilot.Web.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        UsuarioLN ln = new UsuarioLN();

        public ActionResult Index()
        {
            return View(ln.Listar());
        }

        [AdminOnly]
        public ActionResult Create()
        {
            return View();
        }

        [AdminOnly]
        [HttpPost]
        public ActionResult Create(Usuario obj)
        {
            ln.Insertar(obj);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return View(ln.Obtener(id));
        }

        [AdminOnly]
        public ActionResult Edit(int id)
        {
            return View(ln.Obtener(id));
        }

        [AdminOnly]
        [HttpPost]
        public ActionResult Edit(Usuario obj)
        {
            ln.Actualizar(obj);
            return RedirectToAction("Index");
        }

        [AdminOnly]
        public ActionResult Delete(int id)
        {
            return View(ln.Obtener(id));
        }

        [AdminOnly]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            ln.Eliminar(id);
            return RedirectToAction("Index");
        }
    }
}