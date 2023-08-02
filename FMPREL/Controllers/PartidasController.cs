using FMPREL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FMPREL.Controllers
{
    public class PartidasController : Controller
    {
        DbModel db = new DbModel();
        // GET: Partidas
        public ActionResult Index()
        {
            carregaDados();

            new ControleDeAcessos().NovoAcesso(Paginas.Partidas);
            return View();
        }

        public ActionResult Rodada(int rodada)
        {
            carregaDados(rodada);

            try
            {
                var time = new Time { Nome = "À definir", Logo = "" };

                var partidas = db.Rodada.Find(rodada).Jogos.Select(x => new DtoJogoRodada
                {
                    id = x.Id,
                    logoA = x.Times.DefaultIfEmpty(time).FirstOrDefault().Logo,
                    logoB = x.Times.DefaultIfEmpty(time).LastOrDefault().Logo,
                    data = x.Data.ToString("dd-MM-yy : HH:mm"),
                    timeA = x.Times.DefaultIfEmpty(time).FirstOrDefault().Apelido,
                    timeB = x.Times.DefaultIfEmpty(time).LastOrDefault().Apelido,
                    goleiro = x.Melhores.Any() ? x.Melhores.Where(y => y.Goleiro).FirstOrDefault().Jogador.Nome + " (" + x.Melhores.Where(y => y.Goleiro).FirstOrDefault().Jogador.Time.Apelido + ")" : "",
                    jogador = x.Melhores.Any() ? x.Melhores.Where(y => !y.Goleiro).FirstOrDefault().Jogador.Nome + " (" + x.Melhores.Where(y => !y.Goleiro).FirstOrDefault().Jogador.Time.Apelido + ")" : "",
                    placar = x.Gols.Where(y => y.IdTime == x.Times.FirstOrDefault().Id).Count() + " : " + x.Gols.Where(y => y.IdTime == x.Times.LastOrDefault().Id).Count(),
                    golsA = x.Gols.Where(y => y.IdTime == x.Times.FirstOrDefault().Id).GroupBy(y => y.IdJogador).Select(y => new JogadorDto
                    {
                        Nome = y.FirstOrDefault().Jogador.Nome,
                        Quantidade = y.Count()
                    }).ToList(),
                    golsB = x.Gols.Where(y => y.IdTime == x.Times.LastOrDefault().Id).GroupBy(y => y.IdJogador).Select(y => new JogadorDto
                    {
                        Nome = y.FirstOrDefault().Jogador.Nome,
                        Quantidade = y.Count()
                    }).ToList()
                }).OrderBy(x => x.data).ToList();

                foreach (var item in partidas)
                {
                    item.placar = item.golsA.Sum(x => x.Quantidade) + " : " + item.golsB.Sum(x => x.Quantidade);
                }

                return PartialView(partidas);

            }
            catch (Exception ex)
            {
                return Json(new { erro = "erro" });
            }
        }

        public void carregaDados(int rodada = 0)
        {
            Rodada RodadaAtual;
            if (rodada == 0)
                RodadaAtual = db.Rodada.Where(x => x.Data >= DateTime.Today).OrderBy(x => x.Data).FirstOrDefault();
            else
                RodadaAtual = db.Rodada.Where(x => x.Numero == rodada).FirstOrDefault();

            ViewBag.atual = RodadaAtual.Numero;
            var menorRodada = 1;
            var maiorRodada = 9;

            if (RodadaAtual.Numero != menorRodada)
                ViewBag.anterior = RodadaAtual.Numero - 1;

            if (RodadaAtual.Numero != maiorRodada)
                ViewBag.proximo = RodadaAtual.Numero + 1;
        }
    }

    public class DtoJogoRodada
    {
        public int id { get; set; }
        public string timeA { get; set; }
        public string timeB { get; set; }
        public string data { get; set; }
        public string horario { get; set; }
        public string placar { get; set; }
        public string logoA { get; set; }
        public string logoB { get; set; }
        public string goleiro { get; set; }
        public string jogador { get; set; }
        public DateTime DataEHora { get; set; }
        public List<JogadorDto> golsA { get; set; }
        public List<JogadorDto> golsB { get; set; }
        public List<JogadorDto> melhores { get; set; }
    }
}