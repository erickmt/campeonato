using FMPREL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMPREL.Controllers
{
    public class ControleDeAcessos
    {
        DbModel db = new DbModel();
        public void NovoAcesso(Paginas pagina){
            string erro = "";
            try
            {
                db.AcessoAsPaginas.Find((int)pagina).Acessos++;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
        }
    }
}