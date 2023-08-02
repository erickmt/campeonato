using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FMPREL.Models
{
    public class Time
    {
        public Time()
        {
            Jogadores = new List<Jogador>();
            Jogos = new List<Jogo>();
            Gols = new List<Gol>();
            CartoesAmarelos = new List<CartaoAmarelo>();
            CartoesVermelhos = new List<CartaoVermelho>();
        }

        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Logo { get; set; }
        public string Apelido { get; set; }
        [MaxLength(1)]
        public string Grupo { get; set; }

        public virtual Torneio Torneio { get; set; }
        public virtual List<Jogador> Jogadores { get; set; }
        public virtual List<Jogo> Jogos { get; set; }
        public virtual List<Gol> Gols { get; set; }
        public virtual List<CartaoAmarelo> CartoesAmarelos { get; set; }
        public virtual List<CartaoVermelho> CartoesVermelhos { get; set; }
        public int IdTorneio { get; set; }
    }
}