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
            return NivelEjercicioDA.getNiveles();
        }
        public static void Alta(NivelEjercicio NivelEjercicio)
        {
            NivelEjercicioDA.Alta(NivelEjercicio);
        }

        public static void Modificar(NivelEjercicio NivelEjercicio)
        {
            NivelEjercicioDA.Modificar(NivelEjercicio);
        }

        public static void Eliminar(int id)
        {
            NivelEjercicioDA.Eliminar(id);
        }

        public static int ContarCantidad(int idNivelEjercicio, string apellido)
        {
            return NivelEjercicioDA.ContarCantidad(idNivelEjercicio, apellido);
        }

        public static IEnumerable<NivelEjercicio> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, int idNivelEjercicio, string apellido)
        {
            return NivelEjercicioDA.ObtenerPagina(paginaActual, personasPorPagina, sortColumns, idNivelEjercicio, apellido);
        }

      
    }
}
