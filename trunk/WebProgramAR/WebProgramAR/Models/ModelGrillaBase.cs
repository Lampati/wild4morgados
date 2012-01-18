using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

using System.Configuration;

namespace WebProgramAR.Sitio.Models
{
    public class ModelGrillaBase
    {
        public ModelGrillaBase()
        {
            CantidadPorPagina = GetCantidadPorPagina();
        }
        public int Cantidad { get; set; }
        public int CantidadPorPagina { get; set; }
        public int PaginaActual { get; set; }
        public int Estado { get; set; }

        protected int GetCantidadPorPagina()
        {
            try
            {
                //string sCant = ConfigurationManager.AppSettings["Cantidad_por_Pagina"].ToString();
                string sCant = "10";
                if (!string.IsNullOrEmpty(sCant))
                    return int.Parse(sCant);

                return 10;
            }
            catch (Exception ex)
            {
                return 10;
            }
        }

    }
    

   
}