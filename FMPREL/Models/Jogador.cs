using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FMPREL.Models
{
    public class Jogador
    {
        public Jogador()
        {
            Gols = new List<Gol>();
            CartoesVermelhos = new List<CartaoVermelho>();
            CartoesAmarelos = new List<CartaoAmarelo>();
            Melhor = new List<MelhorJogador>();
        }

        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Imagem { get; set; }

        public virtual Time Time { get; set; }

        public virtual List<Gol> Gols { get; set; }
        public virtual List<CartaoVermelho> CartoesVermelhos { get; set; }
        public virtual List<CartaoAmarelo> CartoesAmarelos { get; set; }
        public virtual List<MelhorJogador> Melhor { get; set; }
        public int IdTime { get; set; }

        [NotMapped]
        public int QuantidadeGols { get
            {
                return Gols.Where(x => !x.Contra).Count();
            }
        }
    }
}