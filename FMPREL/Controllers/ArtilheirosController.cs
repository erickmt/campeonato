using FMPREL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FMPREL.Controllers
{
    public class ArtilheirosController : Controller
    {
        DbModel db = new DbModel();
        // GET: Artilheiro
        public ActionResult Index()
        {
            var jogadores = db.Jogador.Where(x => x.Gols.Any(y => !y.Contra)).Select(x => new JogadorDto
            {
                Nome = x.Nome + " - (" + x.Time.Apelido + ")",
                Quantidade = x.Gols.Count(y => !y.Contra)
            });

            new ControleDeAcessos().NovoAcesso(Paginas.Artilheiros);
            return View(jogadores.OrderByDescending(x=>x.Quantidade));
        }
    }

}