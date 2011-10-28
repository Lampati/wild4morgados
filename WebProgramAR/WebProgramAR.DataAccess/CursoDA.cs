using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;

namespace WebProgramAR.DataAccess
{
    public class CursoDA
    {
        public static Curso GetCursoById(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.Cursos.Include("TipoCurso").Include("Cursos").Single(u => u.CursoId  == id);
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

        public static void ModificarUltimoLogin(Curso Curso)
        {
            using (WebProgramAREntities ce = new WebProgramAREntities())
            {
                Curso CursoOrig = ce.Cursos.Single(o => o.CursoId == Curso.CursoId);
                ce.SaveChanges();
            }
        }

        private static void Modificar(Curso Curso, WebProgramAREntities db)
        {
            Curso CursoOrig = db.Cursos.Single(u => u.CursoId == Curso.CursoId);

            db.ObjectStateManager.ChangeObjectState(CursoOrig, EntityState.Modified);
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

                if (sortColumns.Contains("TipoCurso"))
                {
                    sortColumns = sortColumns.Replace("TipoCurso", "TipoCurso.Description");
                }


                return query.OrderUsingSortExpression(sortColumns)
                            .Skip((paginaActual - 1) * personasPorPagina)
                            .Take(personasPorPagina)
                            .ToList();
            }
        }

        private static IQueryable<Curso> GetCursos(int idCurso, string apellido, WebProgramAREntities db)
        {
            IQueryable<Curso> query = from u in db.Cursos
                                     //where u.Status == true &&
                                     //(idCurso == 0 || u.CursoId == idCurso) && u.LastName.Contains(apellido)
                                     select u;
            return query;
        }   

      
    }
}
