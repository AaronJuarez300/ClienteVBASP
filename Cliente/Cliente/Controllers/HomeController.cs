using Cliente.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cliente.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Agregar()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Tabla()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult InsertClient(string item)
        {
            string res = Cliente.Classes.Cliente.InsertCliente(item);
            return this.Content(res, "application/json", System.Text.Encoding.UTF8);
        }

        [HttpGet]
        public ActionResult getClient(string item)
        {
            string res = Cliente.Classes.Cliente.getClient(item);
            return this.Content(res, "application/json", System.Text.Encoding.UTF8);
        }
    }
}