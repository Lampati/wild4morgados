using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;
using WebProgramAR.DataAccess.Interfases;

namespace WebProgramAR.DataAccess
{
    public class CursoDA : IFiltrablePorSeguridadPorValor
    {
        public static string _nombreTabla = "Curso";

        public static Curso GetCursoById(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.Cursos.Single(u => u.CursoId  == id);
            }
        }

        public static void Alta(Curso Curso)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                db.Cursos.AddObject(Curso);
                db.SaveChanges();
            }
        }

        public static void Modificar(Curso Curso)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Modificar(Curso, db);
            }
        }

   

        private static void Modificar(Curso cursoModif, WebProgramAREntities db)
        {
            Curso cursoOrig = db.Cursos.Single(u => u.CursoId == cursoModif.CursoId);

            cursoOrig.Nombre = cursoModif.Nombre;

            db.ObjectStateManager.ChangeObjectState(cursoOrig, EntityState.Modified);
            db.SaveChanges();
        }

        public static void Eliminar(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Curso Curso = db.Cursos.Single(u => u.CursoId == id);
                //Curso.Status = false; //baja lógica

                db.Cursos.DeleteObject(Curso);
                db.SaveChanges();
            }
        }

        //static WebProgramAREntities db = new WebProgramAREntities();
        public static int ContarCantidad(int idCurso, string apellido)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return GetCursos(idCurso, apellido, db).Count();
            }
        }

        public static IEnumerable<Curso> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, int idCurso, string apellido)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                if (paginaActual < 1) paginaActual = 1;


                IQueryable<Curso> query = GetCursos(idCurso, apellido, db);

           
                return query.OrderUsingSortExpression(sortColumns)
                            .Skip((paginaActual - 1) * personasPorPagina)
                            .Take(personasPorPagina)
                            .ToList();
            }
        }

        private static IQueryable<Curso> GetCursos(int idCurso, string nom, WebProgramAREntities db)
        {
            IQueryable<Curso> query = from u in db.Cursos
                                      where (idCurso == -1 || u.CursoId == idCurso)
                                      && ( nom.Equals(string.Empty) ||u.Nombre.ToUpper().Contains(nom.ToUpper()))
                                     select u;
            return query;
        }



        #region IFiltrarPorSeguridadXValor Members

        public List<EntidadProgramARBase> Filtrar(List<EntidadProgramARBase> lista, Usuario user, TipoUsuario tipo)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
