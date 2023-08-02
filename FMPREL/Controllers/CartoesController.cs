using FMPREL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FMPREL.Controllers
{
    public class CartoesController : Controller
    {
        DbModel db = new DbModel();

        public ActionResult Index()
        {
            var jogadores = db.Jogador.Where(x => x.CartoesAmarelos.Any(y => !y.Cumpriu)).Select(x => new JogadorDto
            {
                Nome = x.Nome + " - (" + x.Time.Apelido + ")",
                Quantidade = x.CartoesAmarelos.Count(y => !y.Cumpriu),
                Amarelo = true
            });

            jogadores = jogadores.Union(db.Jogador.Where(x => x.CartoesVermelhos.Any(y => !y.Cumpriu)).Select(x => new JogadorDto
            {
                Nome = x.Nome + " - (" + x.Time.Apelido + ")",
                Quantidade = x.CartoesVermelhos.Count(y => !y.Cumpriu),
                Amarelo = false
            }));

            //new ControleDeAcessos().NovoAcesso(Paginas.Artilheiros);
            return View(jogadores.OrderByDescending(x => x.Quantidade));
        }
    }
}