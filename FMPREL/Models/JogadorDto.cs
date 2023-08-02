using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMPREL.Models
{
    public class JogadorDto
    {
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public bool Goleiro { get; set; }
        public bool Amarelo { get; set; } = false;
        public bool Vermelho { get; set; } = false;
    }
}