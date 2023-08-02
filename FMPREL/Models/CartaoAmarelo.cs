using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FMPREL.Models
{
    public class CartaoAmarelo
    {
        public CartaoAmarelo(){}
        public CartaoAmarelo(int jogo_id, int jogadorId, int idTime, bool cumpriu)
        {
            IdJogo = jogo_id;
            IdJogador = jogadorId;
            IdTime = idTime;
            Cumpriu = cumpriu;
        }

        [Key]
        public int Id { get; set; }
        public bool Cumpriu { get; set; } = false;

        public virtual Jogo Jogo { get; set; }
        public virtual Jogador Jogador { get; set; }
        public virtual Time Time { get; set; }
        public int IdJogo { get; set; }
        public int IdJogador { get; set; }
        public int IdTime { get; set; }
    }
}