using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMPREL.Models
{
    public class GrupoDto
    {
        public string Time { get; set; }
        public int Jogos { get; set; }
        public int Vitorias { get; set; }
        public int Derrotas { get; set; }
        public int Empates { get; set; }
        public int GF { get; set; }
        public int GS { get; set; }
        public int Saldo { get; set; }
        public int Pontos { get; set; }
        public int Posicao { get; set; }
        public string Grupo { get; set; }
        public string Logo { get; set; }
        public int IdTime { get; set; }
        public int CA { get; set; }
        public int CV { get; set; }
    }
}