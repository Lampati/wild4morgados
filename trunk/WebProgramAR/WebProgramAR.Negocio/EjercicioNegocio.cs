using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using WebProgramAR.DataAccess;

namespace WebProgramAR.Negocio
{
    public class EjercicioNegocio
    {
        public static Ejercicio GetEjercicioById(int id)
        {
            return EjercicioDA.GetEjercicioById(id);
        }

        public static void Alta(Ejercicio Ejercicio)
        {
            Ejercicio.FechaAlta = DateTime.Now;
            EjercicioDA.Alta(Ejercicio);
        }

        public static void Modificar(Ejercicio Ejercicio)
        {
            EjercicioDA.Modificar(Ejercicio);
        }

        public static void Eliminar(int id)
        {
            EjercicioDA.Eliminar(id);
        }

        public static int ContarCantidad(int idEjercicio, string apellido)
        {
            return EjercicioDA.ContarCantidad(idEjercicio, apellido);
        }

        public static IEnumerable<Ejercicio> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, int idEjercicio, string apellido)
        {
            return EjercicioDA.ObtenerPagina(paginaActual, personasPorPagina, sortColumns, idEjercicio, apellido);
        }

      
    }
}
