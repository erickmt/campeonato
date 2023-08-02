using FMPREL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FMPREL.Controllers
{
    public class EliminatoriasController : Controller
    {
        // GET: Eliminatorias
        public ActionResult Index()
        {
            new ControleDeAcessos().NovoAcesso(Paginas.Eliminatorios);
            return View();
        }
    }
}