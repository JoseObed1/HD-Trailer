using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        // GET: Video/Edit/5
        // GET: youtubes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            youtube youtube = db.youtube.Find(id);
            if (youtube == null)
            {
                return HttpNotFound();
            }
            return View(youtube);
        }

        // POST: youtubes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,descripcion,url")] youtube youtube)
        {
            if (ModelState.IsValid)
            {
                db.Entry(youtube).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(youtube);
        }

        // GET: youtubes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            youtube youtube = db.youtube.Find(id);
            if (youtube == null)
            {
                return HttpNotFound();
            }
            return View(youtube);
        }

        // POST: youtubes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            youtube youtube = db.youtube.Find(id);
            db.youtube.Remove(youtube);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult NuevoPost()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult GuardarPost(youtube datos)
        {
            var res = Request.Form["selec"];

            if (db.youtube.Any(x => x.nombre == datos.nombre))
            { //Si existe en la db, dale patra
                ViewBag.Notification = "Ya existe ese foro";
                return RedirectToAction("nuevoPost", "Home");
            }
            else
            { //Si el foro no existe en la DB, lo creamos xD
                testeo.NuevoVideo(datos);
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