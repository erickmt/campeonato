using FMPREL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FMPREL.Controllers
{
    public class TimesController : Controller
    {
        DbModel db = new DbModel();
        // GET: Times
        public ActionResult Index()
        {
            new ControleDeAcessos().NovoAcesso(Paginas.Times);
            return View(db.Time);
        }

        public ActionResult Time(int id)
        {
            new ControleDeAcessos().NovoAcesso(Paginas.Time);
            return View(db.Time.Find(id));
        }

        [HttpPost]
        public ActionResult Partidas(int id)
        {
            var time = new Time { Nome = "À definir", Logo = "" };
            var partidas = db.Time.Find(id).Jogos.Select(x => new DtoJogoRodada
            {
                DataEHora = x.Data,
                logoA = x.Times.DefaultIfEmpty(time).FirstOrDefault().Logo,
                logoB = x.Times.DefaultIfEmpty(time).LastOrDefault().Logo,
                data = x.Data.ToString("dd-MM-yy : HH:mm"),
                timeA = x.Times.DefaultIfEmpty(time).FirstOrDefault().Apelido,
                timeB = x.Times.DefaultIfEmpty(time).LastOrDefault().Apelido,
                placar = x.Gols.Where(y => y.IdTime == x.Times.FirstOrDefault().Id).Count() + " : " + x.Gols.Where(y => y.IdTime == x.Times.LastOrDefault().Id).Count()
            }).OrderBy(x => x.DataEHora).ToList();
            return PartialView(partidas);

        }
    }
}