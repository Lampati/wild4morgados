using System;
using System.Collections.Generic;
using System.Linq;
using WebProgramAR.DataAccess;
using WebProgramAR.Entidades;

namespace WebProgramAR.Negocio
{
    public class PaisNegocio
    {
        public static Pais GetPaisById(string id)
        {
            return PaisDA.GetPaisById(id);
        }
        public static IEnumerable<Pais> GetPaises()
        {
            return PaisDA.GetPaises();
        }

   

      
    }
}
