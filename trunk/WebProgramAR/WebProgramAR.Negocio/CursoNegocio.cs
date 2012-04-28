using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using WebProgramAR.DataAccess;

namespace WebProgramAR.Negocio
{
    public class CursoNegocio
    {
        public static Curso GetCursoById(int id)
        {
            return CursoDA.GetCursoById(id);
        }

        public static void Alta(Curso Curso)
        {
            Curso.FechaAlta = DateTime.Now;
            
            CursoDA.Alta(Curso);
        }

        public static void Modificar(Curso Curso)
        {
            CursoDA.Modificar(Curso, new int[] {},true);
        }

        public static void Eliminar(int id)
        {
            CursoDA.Eliminar(id);
        }

        public static int ContarCantidad(int idCurso, string nombre, int usuarioId, Usuario usuarioLogueado)
        {
            return CursoDA.ContarCantidad(idCurso, nombre, usuarioId,usuarioLogueado);
        }

        public static IEnumerable<Curso> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, int idCurso, string nombre, int usuarioId, Usuario usuarioLogueado)
        {
            return CursoDA.ObtenerPagina(paginaActual, personasPorPagina, sortColumns, idCurso, nombre, usuarioId,usuarioLogueado);
        }


        public static void AsociarCursoEjercicio(Curso curso,Ejercicio ejercicio)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="curso">Curso a modificar</param>
        /// <param name="idEjerciciosAgregar">Ids de los ejercicios a agregar</param>
        /// <param name="agregar">True indica agregar / False indica quitar los ejercicios</param>
        public static void Modificar(Curso curso, int[] idEjerciciosAgregar, bool agregar)
        {
            CursoDA.Modificar(curso, idEjerciciosAgregar, agregar);
        }

        internal static void EliminarCursosDeUsuario(int idUsuario)
        {
            CursoDA.EliminarCursosDeUsuario(idUsuario);
        }
    }
}
