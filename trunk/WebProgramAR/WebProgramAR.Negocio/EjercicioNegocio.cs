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
        public static IEnumerable<Ejercicio> GetEjerciciosByCursoByUsuarioByNivelByEstado(int usuarioId, int cursoId, int nivelEjercicio,int estadoEjercicio)
        {
            return EjercicioDA.GetEjerciciosByCursoByUsuarioByNivelByEstado(usuarioId, cursoId, estadoEjercicio, nivelEjercicio);
        }
        public static IEnumerable<Ejercicio> GetEjercicioNotUsuario(int usuarioId, int cursoId, int nivelEjercicio, int estadoEjercicio)
        {
            return EjercicioDA.GetEjercicioNotUsuario(usuarioId, cursoId, estadoEjercicio, nivelEjercicio);
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

        public static int ContarCantidad(string nombre, int usuarioId , int cursoId , int estadoEjercicio, int nivelEjercicio, bool global )
        {
            return EjercicioDA.ContarCantidad(nombre, usuarioId, cursoId, estadoEjercicio, nivelEjercicio, global);
        }

        public static IEnumerable<Ejercicio> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, string nombre, int usuarioId, int cursoId, int estadoEjercicio, int nivelEjercicio, bool global)
        {
            return EjercicioDA.ObtenerPagina(paginaActual, personasPorPagina, sortColumns, nombre, usuarioId, cursoId, estadoEjercicio, nivelEjercicio, global).ToList();
        }



        public static Ejercicio GetEjercicioByIdOnlyUser(int id)
        {
            return EjercicioDA.GetEjercicioByIdOnlyUser(id);
        }

        public static void ModificarEstado(Ejercicio ejercicio)
        {
            EjercicioDA.ModificarEstado(ejercicio);
        }
    }
}
