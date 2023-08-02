using FMPREL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FMPREL.Controllers
{
    public class CadastroController : Controller
    {
        DbModel db = new DbModel();

        public ActionResult Index()
        {
            var time = new Time { Nome = "À definir", Logo = "" };
            var dtoJogo = db.Jogo.Where(x => !x.Encerrado).ToList()
                .Select(x=> new DtoJogo
                {
                    Id = x.Id,
                    Descricao = x.Data.ToString() + " - " + x.Times.DefaultIfEmpty(time).FirstOrDefault().Apelido + " x " + x.Times.DefaultIfEmpty(time).LastOrDefault().Apelido
                });

            ViewBag.Jogos = new SelectList(dtoJogo, "Id", "Descricao", 1);
            return View();
        }

        public ActionResult NovoJogo()
        {
            ViewBag.Times1 = ViewBag.Times2 = new SelectList(db.Time, "Id", "Nome", 1);
            ViewBag.Rodada = new SelectList(db.Rodada.Select(x => new DtoRodada { Id = x.Id, Data = x.Id + " - " + x.Data.ToString().Substring(0, 10) }), "Id", "Data", 1);

            return View();
        }

        public ActionResult Jogadores(int jogo_id)
        {
            int[] times = db.Jogo.Find(jogo_id).Times.Select(x => x.Id).ToArray();
            var jogadores = db.Jogador.ToList().Where(x => times.Contains(x.IdTime))
                .Select(x => new Jogador
                {
                    Id = x.Id,
                    Nome = $"({x.Time.Apelido}) - {x.Nome}"
                })
                .ToList();

            ViewBag.Jogadores = new SelectList(jogadores, "Id", "Nome", 1);
            return PartialView();
        }

        public ActionResult Gol(int jogo_id, int jogador, int quantidade, bool contra)
        {
            bool golContra = contra;
            var jogo = db.Jogo.Find(jogo_id);
            var Jogador = db.Jogador.Find(jogador);
            for (int i = 0; i < quantidade; i++)
            {
                jogo.Gols.Add(new Models.Gol
                {
                    IdJogo = jogo_id,
                    IdJogador = jogador,
                    Contra = golContra,
                    IdTime = golContra ? jogo.Times.Where(x => x.Id != Jogador.IdTime).FirstOrDefault().Id : Jogador.IdTime
                });
            }

            db.SaveChanges();
            return Json(new { resultado = "ok", mensagem = Jogador.Nome + " - " + quantidade + " gols" });
        }

        public ActionResult InserirCartoes(int jogo_id, int jogadorId, int quantidade, bool vermelho)
        {
            var jogador = db.Jogador.Find(jogadorId);
            var cartaoVermelho = new Models.CartaoVermelho(jogo_id, jogadorId, jogador.IdTime, false);

            if (!vermelho)
            {
                var amarelo = new Models.CartaoAmarelo(jogo_id, jogadorId, jogador.IdTime, false);

                if (quantidade == 2)
                {
                    amarelo.Cumpriu = true;
                    jogador.CartoesAmarelos.Add(amarelo);

                    var amarelo2 = new Models.CartaoAmarelo(jogo_id, jogadorId, jogador.IdTime, true);
                    jogador.CartoesAmarelos.Add(amarelo2);
                    jogador.CartoesVermelhos.Add(cartaoVermelho);
                }

                else
                {
                    if (jogador.CartoesAmarelos.Count(x => !x.Cumpriu) == 2)
                    {
                        amarelo.Cumpriu = true;
                        cartaoVermelho.Acumulado = true;

                        foreach (var item in jogador.CartoesAmarelos.Where(x => !x.Cumpriu))
                            item.Cumpriu = true;

                        jogador.CartoesVermelhos.Add(cartaoVermelho);
                    }

                    jogador.CartoesAmarelos.Add(amarelo);
                }
            }
            else
                jogador.CartoesVermelhos.Add(cartaoVermelho);

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { resultado = "Erro", mensagem = "Erro ao salvar cartão" });
            }

            string cor = vermelho ? "vermelho" : "amarelo";
            return Json(new { resultado = "ok", mensagem = jogador.Nome + " - " + cor });
        }

        public void Encerrar(int jogo_id)
        {
            var jogo = db.Jogo.Find(jogo_id);
            var time1Id = jogo.Times.FirstOrDefault().Id;
            var time2Id = jogo.Times.Where(x => x.Id != time1Id).FirstOrDefault().Id;
            var time1Gols = jogo.Gols.Where(x => x.IdTime == time1Id).Count();
            var time2Gols = jogo.Gols.Where(x => x.IdTime == time2Id).Count();

            jogo.Encerrado = true;
            if (time1Gols == time2Gols)
                jogo.Empate = true;
            else
            {
                if (time1Gols > time2Gols)
                {
                    jogo.IdVencedor = time1Id;
                    jogo.IdPerdedor = time2Id;
                }
                else
                {
                    jogo.IdPerdedor = time1Id;
                    jogo.IdVencedor = time2Id;
                }
            }

            foreach (var item in db.Jogador.Include("CartoesVermelhos").Where(x => x.IdTime == time1Id || x.IdTime == time2Id))
            {
                foreach (var cv in item.CartoesVermelhos.Where(x => x.IdJogo != jogo_id))
                {
                    cv.Cumpriu = true;
                }
            }

            db.SaveChanges();
            //return Index();
        }

        public ActionResult InserirNovoJogo(string time1, string time2, string rodada, string horario)
        {
            try
            {
                var jogo = new Jogo
                {
                    IdRodada = int.Parse(rodada.Split('-')[0].Trim())
                };

                var timeA = db.Time.Find(int.Parse(time1));
                var timeB = db.Time.Find(int.Parse(time2));

                jogo.Times.Add(timeA);
                jogo.Times.Add(timeB);


                var dataRodada = db.Rodada.Find(int.Parse(rodada)).Data;
                switch (horario)
                {
                    case "1":
                        {
                            jogo.Data = dataRodada.AddHours(13);
                            break;
                        }
                    case "2":
                        {
                            jogo.Data = dataRodada.AddHours(14);
                            break;
                        }
                    case "3":
                        {
                            jogo.Data = dataRodada.AddHours(15);
                            break;
                        }
                    case "4":
                        {
                            jogo.Data = dataRodada.AddHours(16);
                            break;
                        }
                    default:
                        break;
                }

                db.Jogo.Add(jogo);
                db.SaveChanges();
                return Json(new { resultado = "ok", mensagem = "Inserido com sucesso " + timeA.Apelido + " x " + timeB.Apelido });
            }
            catch (Exception ex)
            {
                return Json(new { resultado = "ok", mensagem = "Erro ao inserir: " + ex.Message });
            }

        }

        [HttpPost]
        public JsonResult Horarios(string rodada)
        {
            int id = int.Parse(rodada.Split('-')[0].Trim());
            var horarios = db.Jogo.Where(x => !x.Times.Any() && x.IdRodada == id).Select(x => new
            {
                Hora = x.Data.ToString().Substring(11, 4),
                x.Id
            }).ToArray();
            var serializer = new JavaScriptSerializer();
            var horariosJson = serializer.Serialize(horarios);
            return Json(horariosJson);
        }

        public ActionResult Melhores()
        {
            var time = new Time { Nome = "À definir", Logo = "" };
            var dtoJogo = db.Jogo.Where(x => !x.Encerrado && x.Melhores.Count() < 2).ToList().Select(
                    x=> new Jogador
                    {
                        Id = x.Id,
                        Nome = x.Data.ToString() + " - " + x.Times.DefaultIfEmpty(time).FirstOrDefault().Apelido + " x " + x.Times.DefaultIfEmpty(time).LastOrDefault().Apelido
                    }
                );

            ViewBag.Jogos = new SelectList(dtoJogo, "Id", "Nome", 1);
            return View();
        }

        public ActionResult Cartoes()
        {
            var time = new Time { Nome = "À definir", Logo = "" };
            var dtoJogo = db.Jogo.Where(x => !x.Encerrado).ToList().Select(x => new Jogador
            {
                Id = x.Id,
                Nome = x.Data.ToString() + " - " + x.Times.DefaultIfEmpty(time).FirstOrDefault().Apelido + " x " +
                       x.Times.DefaultIfEmpty(time).LastOrDefault().Apelido
            }).ToList();

            ViewBag.Jogos = new SelectList(dtoJogo, "Id", "Nome", 1);
            return View();
        }

        public ActionResult CadastrarMelhor(int jogo_id, int jogador, bool goleiro)
        {
            if (db.MelhorJogador.Where(x => x.IdJogo == jogo_id && x.Goleiro == goleiro).Any())
                return Json(new { resultado = "erro", mensagem = "Melhor jogador da posição já cadastro" });

            db.MelhorJogador.Add(new MelhorJogador
            {
                IdJogador = jogador,
                Nota = 1,
                IdJogo = jogo_id,
                Goleiro = goleiro
            });

            string nome = db.Jogador.Find(jogador).Nome;
            string posicao = goleiro ? "goleiro" : "jogador";
            db.SaveChanges();
            return Json(new { resultado = "erro", mensagem = $"Sucesso ao cadastrar melhor {posicao} {nome} " });
        }
    }

    public class DtoRodada
    {
        public int Id { get; set; }
        public string Data { get; set; }
    }

    public class DtoJogo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
    }
}