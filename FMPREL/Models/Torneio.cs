using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FMPREL.Models
{
    public class Torneio
    {
        public Torneio()
        {
            Times = new List<Time>();
            Rodadas = new List<Rodada>();
        }
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Ano { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }

        public virtual List<Time> Times { get; set; }
        public virtual List<Rodada> Rodadas { get; set; }

    }
}