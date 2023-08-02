using FMPREL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FMPREL.Controllers
{
    public class GruposController : Controller
    {
        DbModel db = new DbModel();
        // GET: Grupos
        public ActionResult Index()
        {
            var times = db.Time.Select(x => new GrupoDto
            {
                Time = x.Apelido,
                Derrotas = x.Jogos.Where(y => y.IdPerdedor == x.Id).Count(),
                Vitorias = x.Jogos.Where(y => y.IdVencedor == x.Id).Count(),
                Empates = x.Jogos.Where(y => y.Empate).Count(),
                GF = x.Gols.Count(),
                GS = 0,
                Saldo = 0,
                CA = x.CartoesAmarelos.Count(),
                CV = x.CartoesVermelhos.Where(y => !y.Acumulado).Count(),
                Jogos = x.Jogos.Where(z => z.Encerrado).Count(),
                Pontos = x.Jogos.Where(z => z.IdVencedor == x.Id).Count() * 3 + x.Jogos.Where(z => z.Empate).Count(),
                Grupo = x.Grupo,
                Logo = x.Logo,
                IdTime = x.Id
            }).ToList();

            int gs;
            foreach (var item in times)
            {
                gs = db.Gol.Where(x => x.IdTime != item.IdTime).Count();
                item.Saldo = item.GF - gs;
                item.GS = gs;
            }
            times.Sort(new ClassificaoComparer());

            int i = 1;
            foreach (var item in times.Where(x => x.Grupo == "A"))
            {
                item.Posicao = i;
                i++;
            }
            i = 1;
            foreach (var item in times.Where(x => x.Grupo == "B"))
            {
                item.Posicao = i;
                i++;
            }
            new ControleDeAcessos().NovoAcesso(Paginas.Grupos);
            return View(times);
        }
    }

    internal class ClassificaoComparer : IComparer<GrupoDto>
    {
        public int Compare(GrupoDto x, GrupoDto y)
        {
            int comparacao = y.Pontos.CompareTo(x.Pontos);
            if (comparacao == 0)
                comparacao = y.Saldo.CompareTo(x.Saldo);
            if (comparacao == 0)
                comparacao = y.GF.CompareTo(x.GF);
            //if (comparacao == 0)
            //    comparacao = x.Saldo.CompareTo(y.Saldo);

            return comparacao;
        }
    }
}