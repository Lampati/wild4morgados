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

        public static void Alta(EstadoEjercicio EstadoEjercicio)
        {
            EstadoEjercicioDA.Alta(EstadoEjercicio);
        }

        public static void Modificar(EstadoEjercicio EstadoEjercicio)
        {
            EstadoEjercicioDA.Modificar(EstadoEjercicio);
        }

        public static void Eliminar(int id)
        {
            EstadoEjercicioDA.Eliminar(id);
        }

        public static int ContarCantidad(int idEstadoEjercicio, string apellido)
        {
            return EstadoEjercicioDA.ContarCantidad(idEstadoEjercicio, apellido);
        }

        public static IEnumerable<EstadoEjercicio> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, int idEstadoEjercicio, string apellido)
        {
            return EstadoEjercicioDA.ObtenerPagina(paginaActual, personasPorPagina, sortColumns, idEstadoEjercicio, apellido);
        }

      
    }
}
