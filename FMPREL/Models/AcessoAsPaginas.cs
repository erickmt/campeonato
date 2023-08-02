using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FMPREL.Models
{
    public class AcessoAsPaginas
    {
        [Key]
        public int Id { get; set; }
        public string Pagina { get; set; }
        public int Acessos { get; set; }
    }

    public enum Paginas
    {
        Inicio = 1,
        Grupos = 2,
        Eliminatorios = 3,
        Partidas = 4,
        Times = 5,
        Artilheiros = 6,
        Melhores = 7,
        Time = 8
    }
}