using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using WebProgramAR.DataAccess;

namespace WebProgramAR.Negocio
{
    public class NivelEjercicioNegocio
    {
        public static NivelEjercicio GetNivelEjercicioById(int id)
        {
            return NivelEjercicioDA.GetNivelEjercicioById(id);
        }

        public static IEnumerable<NivelEjercicio> GetNiveles()
        {
            return NivelEjercicioDA.GetNivelEjercicios();
        }
       
      
    }
}
