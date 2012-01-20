using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using WebProgramAR.DataAccess;

namespace WebProgramAR.Negocio
{
    public class EstadoEjercicioNegocio
    {
        public static EstadoEjercicio GetEstadoEjercicioById(int id)
        {
            return EstadoEjercicioDA.GetEstadoEjercicioById(id);
        }


        public static IEnumerable<EstadoEjercicio> GetEstadoEjercicios()
        {
            return EstadoEjercicioDA.GetEstadoEjercicios();
        }
      
    }
}
