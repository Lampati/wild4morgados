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
        public static IEnumerable<Ejercicio> GetEjercicioNotUsuario(int usuarioId, int cursoId, int nivelEjercicio, int estadoEjercicio, Usuario userLogueado)
        {
            return EjercicioDA.GetEjercicioNotUsuario(usuarioId, cursoId, estadoEjercicio, nivelEjercicio, userLogueado);
        }
        //public static IEnumerable<Ejercicio> GetEjerciciosNotCurso(string nombre, int usuarioId, int cursoId, int nivelEjercicio, int estadoEjercicio, bool global, Usuario userLogueado)
        //{
        //    return EjercicioDA.GetEjerciciosNotCurso(nombre,usuarioId, cursoId, estadoEjercicio, nivelEjercicio,global, userLogueado);
        //}

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

        public static int ContarCantidad(string nombre, int usuarioId, int cursoId, int estadoEjercicio, int nivelEjercicio, bool global, Usuario userLogueado)
        {
            return EjercicioDA.ContarCantidad(nombre, usuarioId, cursoId, estadoEjercicio, nivelEjercicio, global,userLogueado);
        }
        public static int ContarCantidadNotCurso(string nombre, int usuarioId, int cursoId, int estadoEjercicio, int nivelEjercicio, bool global, Usuario userLogueado)
        {
            return EjercicioDA.ContarCantidadNotCurso(nombre, usuarioId, cursoId, estadoEjercicio, nivelEjercicio, global, userLogueado);
        }
        public static IEnumerable<Ejercicio> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, string nombre, int usuarioId, int cursoId, int estadoEjercicio, int nivelEjercicio, bool global, Usuario userLogueado)
        {
            return EjercicioDA.ObtenerPagina(paginaActual, personasPorPagina, sortColumns, nombre, usuarioId, cursoId, estadoEjercicio, nivelEjercicio, global,userLogueado).ToList();
        }
        public static IEnumerable<Ejercicio> ObtenerPaginaNotCurso(int paginaActual, int personasPorPagina, string sortColumns, string nombre, int usuarioId, int cursoId, int estadoEjercicio, int nivelEjercicio, bool global, Usuario userLogueado)
        {
            return EjercicioDA.ObtenerPaginaNotCurso(paginaActual, personasPorPagina, sortColumns, nombre, usuarioId, cursoId, estadoEjercicio, nivelEjercicio, global, userLogueado).ToList();
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
