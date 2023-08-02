using FMPREL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FMPREL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            new ControleDeAcessos().NovoAcesso(Paginas.Inicio);
            var a = new DateTime(2023, 09, 30, 15, 00, 00);
            var b = a.Subtract(DateTime.Now);

            ViewBag.Dia = b.Days;
            ViewBag.Hora = b.Hours;
            ViewBag.Minuto = b.Minutes;
            return View();
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

        public ActionResult Breve()
        {
            return View();
        }
    }
}