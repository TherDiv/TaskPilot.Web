using System.Web.Mvc;
using System.Web.Security;
using TaskPilot.LogicaNegocio;

namespace TaskPilot.Web.Controllers
{
    public class LoginController : Controller
    {
        UsuarioLN ln = new UsuarioLN();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string usuario, string clave)
        {
            var user = ln.Validar(usuario, clave);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(
                    user.UsuarioLogin,
                    false);
                Session["Usuario"] = user;
                return RedirectToAction(
                    "Index",
                    "Home");
            }
            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            Session.Clear();

            return RedirectToAction(
                "Index",
                "Login");
        }
    }
}