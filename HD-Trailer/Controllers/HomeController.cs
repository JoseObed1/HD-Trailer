using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HD_Trailer.Controllers
{
    public class HomeController : Controller
    {
        CodigoDB testeo = new CodigoDB();
        DBEntity db = new DBEntity();

        public ActionResult Index()
        {
            return View(db.youtube.ToList());
        }

        public ActionResult ForosPublicados()
        {
            return View(db.youtube.ToList());
        }

        public ActionResult sobrenosotros()
        {
            return View();
        }

        public ActionResult LoginRegister()
        {
            ViewBag.Notification = TempData["error2"];
            ViewBag.ErrorLogin = TempData["error"];
            return View();
        }

        [HttpPost]
        public ActionResult Registro(usuarios dato)
        {
            if (db.usuarios.Any(x => x.username == dato.username))
            { //Si existe en la db, dale patra
                TempData["error2"] = "Ya existe esta cuenta";
                return RedirectToAction("LoginRegister", "Home", TempData["error2"]);
            }

            else
            { //Si el usuario no existe en la DB, lo creamos xD                
                testeo.NuevoUsuario(dato);
                Session["UserName"] = dato.username.ToString();
                FormsAuthentication.SetAuthCookie(dato.username, true);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InicioSesion(usuarios datoss)
        {
            var existe = db.usuarios.Where(y => y.username.Equals(datoss.username) && y.password.Equals(datoss.password)).FirstOrDefault();

            if (existe != null)
            {
                Session["UserName"] = existe.username.ToString();
                FormsAuthentication.SetAuthCookie(datoss.username, true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //LoginError = "Email o contraseña incorrecta";
                TempData["error"] = "Email o contraseña incorrecta";
                return RedirectToAction("LoginRegister", "Home", TempData["error"]);
            }

        }

        public ActionResult LogOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}