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
            CursoDA.Modificar(Curso,false);
        }

        public static void Eliminar(int id)
        {
            CursoDA.Eliminar(id);
        }

        public static int ContarCantidad(int idCurso, string apellido)
        {
            return CursoDA.ContarCantidad(idCurso, apellido);
        }

        public static IEnumerable<Curso> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, int idCurso, string apellido)
        {
            return CursoDA.ObtenerPagina(paginaActual, personasPorPagina, sortColumns, idCurso, apellido);
        }


        public static void AsociarCursoEjercicio(Curso curso)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="curso">Curso a modificar</param>
        /// <param name="p">Indica si hay que asignarle los ejercicios</param>
        public static void Modificar(Curso curso, bool p)
        {
            CursoDA.Modificar(curso, p);
        }

        internal static void EliminarCursosDeUsuario(int idUsuario)
        {
            CursoDA.EliminarCursosDeUsuario(idUsuario);
        }
    }
}
