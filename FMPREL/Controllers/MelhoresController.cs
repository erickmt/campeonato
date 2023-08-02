using FMPREL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FMPREL.Controllers
{
    public class MelhoresController : Controller
    {
        DbModel db = new DbModel();

        public ActionResult Index()
        {
            var jogadores = db.MelhorJogador.Where(x => !x.Goleiro).GroupBy(x => x.IdJogador).Select(x => new JogadorDto
            {
                Nome = x.FirstOrDefault().Jogador.Nome + " - (" + x.FirstOrDefault().Jogador.Time.Nome + ")",
                Quantidade = x.Count(),
                Goleiro = false
            });

            jogadores = jogadores.Union(db.MelhorJogador.Where(x => x.Goleiro).GroupBy(x => x.IdJogador).Select(x => new JogadorDto
            {
                Nome = x.FirstOrDefault().Jogador.Nome + " - (" + x.FirstOrDefault().Jogador.Time.Nome + ")",
                Quantidade = x.Count(),
                Goleiro = true
            }));

            new ControleDeAcessos().NovoAcesso(Paginas.Melhores);
            return View(jogadores.OrderByDescending(x => x.Quantidade));
        }
    }
}