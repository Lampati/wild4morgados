using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;

namespace WebProgramAR.DataAccess
{
    public class EstadoEjercicioDA
    {
        public static EstadoEjercicio GetEstadoEjercicioById(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.EstadoEjercicios.Include("TipoEstadoEjercicio").Include("EstadoEjercicios").Single(u => u.EstadoEjercicioId  == id);
            }
        }

        public static void Alta(EstadoEjercicio EstadoEjercicio)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                db.EstadoEjercicios.AddObject(EstadoEjercicio);
                db.SaveChanges();
            }
        }

        public static void Modificar(EstadoEjercicio EstadoEjercicio)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Modificar(EstadoEjercicio, db);
            }
        }

        public static void ModificarUltimoLogin(EstadoEjercicio EstadoEjercicio)
        {
            using (WebProgramAREntities ce = new WebProgramAREntities())
            {
                EstadoEjercicio EstadoEjercicioOrig = ce.EstadoEjercicios.Single(o => o.EstadoEjercicioId == EstadoEjercicio.EstadoEjercicioId);
                ce.SaveChanges();
            }
        }

        private static void Modificar(EstadoEjercicio EstadoEjercicio, WebProgramAREntities db)
        {
            EstadoEjercicio EstadoEjercicioOrig = db.EstadoEjercicios.Single(u => u.EstadoEjercicioId == EstadoEjercicio.EstadoEjercicioId);

            db.ObjectStateManager.ChangeObjectState(EstadoEjercicioOrig, EntityState.Modified);
            db.SaveChanges();
        }

        public static void Eliminar(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                EstadoEjercicio EstadoEjercicio = db.EstadoEjercicios.Single(u => u.EstadoEjercicioId == id);
                //EstadoEjercicio.Status = false; //baja lógica

                db.EstadoEjercicios.DeleteObject(EstadoEjercicio);
                db.SaveChanges();
            }
        }

        //static WebProgramAREntities db = new WebProgramAREntities();
        public static int ContarCantidad(int idEstadoEjercicio, string apellido)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return GetEstadoEjercicios(idEstadoEjercicio, apellido, db).Count();
            }
        }

        public static IEnumerable<EstadoEjercicio> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, int idEstadoEjercicio, string apellido)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                if (paginaActual < 1) paginaActual = 1;


                IQueryable<EstadoEjercicio> query = GetEstadoEjercicios(idEstadoEjercicio, apellido, db);

                if (sortColumns.Contains("TipoEstadoEjercicio"))
                {
                    sortColumns = sortColumns.Replace("TipoEstadoEjercicio", "TipoEstadoEjercicio.Description");
                }


                return query.OrderUsingSortExpression(sortColumns)
                            .Skip((paginaActual - 1) * personasPorPagina)
                            .Take(personasPorPagina)
                            .ToList();
            }
        }

        private static IQueryable<EstadoEjercicio> GetEstadoEjercicios(int idEstadoEjercicio, string apellido, WebProgramAREntities db)
        {
            IQueryable<EstadoEjercicio> query = from u in db.EstadoEjercicios
                                     //where u.Status == true &&
                                     //(idEstadoEjercicio == 0 || u.EstadoEjercicioId == idEstadoEjercicio) && u.LastName.Contains(apellido)
                                     select u;
            return query;
        }   

      
    }
}
