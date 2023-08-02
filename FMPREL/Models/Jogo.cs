using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FMPREL.Models
{
    public class Jogo
    {
        public Jogo()
        {
            Gols = new List<Gol>();
            CartoesAmarelos = new List<CartaoAmarelo>();
            CartoesVermelhos = new List<CartaoVermelho>();
            Melhores = new List<MelhorJogador>();
            Times = new List<Time>();
        }

        [Key]
        public int Id { get; set; }
        public DateTime Data { get; set; }
        
        public virtual Rodada Rodada { get; set; }
        public virtual List<Gol> Gols { get; set; }
        public virtual List<CartaoAmarelo> CartoesAmarelos { get; set; }
        public virtual List<CartaoVermelho> CartoesVermelhos { get; set; }
        public virtual List<Time> Times { get; set; }
        public virtual List<MelhorJogador> Melhores { get; set; }
        public int IdRodada { get; set; }
        public bool Encerrado { get; set; }
        public int? IdVencedor { get; set; }
        public int? IdPerdedor { get; set; }
        public bool Empate { get; set; }
    }
}