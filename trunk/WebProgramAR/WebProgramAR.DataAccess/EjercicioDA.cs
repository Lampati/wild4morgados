using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;

namespace WebProgramAR.DataAccess
{
    public class EjercicioDA
    {
        public static Ejercicio GetEjercicioById(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.Ejercicios.Include("TipoEjercicio").Include("Cursos").Single(u => u.EjercicioId  == id);
            }
        }

        public static void Alta(Ejercicio Ejercicio)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                db.Ejercicios.AddObject(Ejercicio);
                db.SaveChanges();
            }
        }

        public static void Modificar(Ejercicio Ejercicio)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Modificar(Ejercicio, db);
            }
        }

        public static void ModificarUltimoLogin(Ejercicio Ejercicio)
        {
            using (WebProgramAREntities ce = new WebProgramAREntities())
            {
                Ejercicio EjercicioOrig = ce.Ejercicios.Single(o => o.EjercicioId == Ejercicio.EjercicioId);
                ce.SaveChanges();
            }
        }

        private static void Modificar(Ejercicio Ejercicio, WebProgramAREntities db)
        {
            Ejercicio EjercicioOrig = db.Ejercicios.Single(u => u.EjercicioId == Ejercicio.EjercicioId);

            db.ObjectStateManager.ChangeObjectState(EjercicioOrig, EntityState.Modified);
            db.SaveChanges();
        }

        public static void Eliminar(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Ejercicio Ejercicio = db.Ejercicios.Single(u => u.EjercicioId == id);
                //Ejercicio.Status = false; //baja lógica

                db.Ejercicios.DeleteObject(Ejercicio);
                db.SaveChanges();
            }
        }

        //static WebProgramAREntities db = new WebProgramAREntities();
        public static int ContarCantidad(int idEjercicio, string apellido)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return GetEjercicios(idEjercicio, apellido, db).Count();
            }
        }

        public static IEnumerable<Ejercicio> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, int idEjercicio, string apellido)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                if (paginaActual < 1) paginaActual = 1;


                IQueryable<Ejercicio> query = GetEjercicios(idEjercicio, apellido, db);

                if (sortColumns.Contains("TipoEjercicio"))
                {
                    sortColumns = sortColumns.Replace("TipoEjercicio", "TipoEjercicio.Description");
                }


                return query.OrderUsingSortExpression(sortColumns)
                            .Skip((paginaActual - 1) * personasPorPagina)
                            .Take(personasPorPagina)
                            .ToList();
            }
        }

        private static IQueryable<Ejercicio> GetEjercicios(int idEjercicio, string apellido, WebProgramAREntities db)
        {
            IQueryable<Ejercicio> query = from u in db.Ejercicios
                                     //where u.Status == true &&
                                     //(idEjercicio == 0 || u.EjercicioId == idEjercicio) && u.LastName.Contains(apellido)
                                     select u;
            return query;
        }   

      
    }
}
