using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HD_Trailer.Controllers
{
    public class HomeController : Controller
    {
        DBEntity db = new DBEntity();

        public ActionResult Index()
        {
            return View(db.youtube.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult sobrenosotros()
        {
            return View();
        }
    }
}