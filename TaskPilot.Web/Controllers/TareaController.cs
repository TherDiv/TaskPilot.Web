using System.Web.Mvc;
using TaskPilot.Entidades;
using TaskPilot.LogicaNegocio;

namespace TaskPilot.Web.Controllers
{
    public class TareaController : Controller
    {
        TareaLN ln = new TareaLN();
        UsuarioLN usuarioLN = new UsuarioLN();

        // LISTAR
        public ActionResult Index()
        {
            return View(ln.Listar());
        }

        // CREAR
        public ActionResult Create()
        {
            ViewBag.Usuarios = new MultiSelectList(
                usuarioLN.Listar(),
                "IdUsuario",
                "NombreCompleto");

            return View();
        }

        [HttpPost]
        public ActionResult Create(Tarea obj)
        {
            if (ModelState.IsValid)
            {
                ln.Insertar(obj);
                return RedirectToAction("Index");
            }

            ViewBag.Usuarios = new MultiSelectList(
                usuarioLN.Listar(),
                "IdUsuario",
                "NombreCompleto",
                obj.UsuariosSeleccionados);

            return View(obj);
        }

        // DETALLE
        public ActionResult Details(int id)
        {
            return View(ln.Obtener(id));
        }

        // EDITAR
        public ActionResult Edit(int id)
        {
            Tarea tarea = ln.Obtener(id);

            ViewBag.Usuarios = new MultiSelectList(
                usuarioLN.Listar(),
                "IdUsuario",
                "NombreCompleto",
                tarea.UsuariosSeleccionados);

            return View(tarea);
        }

        [HttpPost]
        public ActionResult Edit(Tarea obj)
        {
            if (ModelState.IsValid)
            {
                ln.Actualizar(obj);
                return RedirectToAction("Index");
            }

            ViewBag.Usuarios = new MultiSelectList(
                usuarioLN.Listar(),
                "IdUsuario",
                "NombreCompleto",
                obj.UsuariosSeleccionados);

            return View(obj);
        }

        // ELIMINAR
        public ActionResult Delete(int id)
        {
            return View(ln.Obtener(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            ln.Eliminar(id);
            return RedirectToAction("Index");
        }
    }
}