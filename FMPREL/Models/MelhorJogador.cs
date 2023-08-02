using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FMPREL.Models
{
    public class MelhorJogador
    {
        [Key]
        public int Id { get; set; }
        public int Nota { get; set; }
        public bool Goleiro { get; set; }

        public virtual Jogador Jogador { get; set; }
        public int IdJogador { get; set; }

        public virtual Jogo Jogo { get; set; }
        public int IdJogo { get; set; }
    }
}