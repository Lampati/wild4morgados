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
            CursoDA.Modificar(Curso);
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

    }
}
