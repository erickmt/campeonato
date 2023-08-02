using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FMPREL.Models
{
    public class Rodada
    {
        public Rodada()
        {
            Jogos = new List<Jogo>();
        }
        [Key]
        public int Id { get; set; }
        public int Numero { get; set; }
        public DateTime Data { get; set; }

        public virtual Torneio Torneio { get; set; }
        public virtual List<Jogo> Jogos { get; set; }
        public int IdTorneio { get; set; }
    }
}