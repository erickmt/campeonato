using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FMPREL.Models
{
    public class Gol
    {
        [Key]
        public int Id { get; set; }
        public bool Contra { get; set; } = false;

        public virtual Jogo Jogo { get; set; }
        public virtual Jogador Jogador { get; set; }
        public virtual Time Time { get; set; }
        public int IdJogo { get; set; }
        public int IdJogador { get; set; }
        public int IdTime { get; set; }
    }
}