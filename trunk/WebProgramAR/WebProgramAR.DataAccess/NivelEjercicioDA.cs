using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;

namespace WebProgramAR.DataAccess
{
    public class NivelEjercicioDA
    {
        public static NivelEjercicio GetNivelEjercicioById(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.NivelEjercicios.Include("TipoNivelEjercicio").Include("NivelEjercicios").Single(u => u.NivelEjercicioId  == id);
            }
        }

        public static void Alta(NivelEjercicio NivelEjercicio)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                db.NivelEjercicios.AddObject(NivelEjercicio);
                db.SaveChanges();
            }
        }

        public static void Modificar(NivelEjercicio NivelEjercicio)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Modificar(NivelEjercicio, db);
            }
        }

        public static void ModificarUltimoLogin(NivelEjercicio NivelEjercicio)
        {
            using (WebProgramAREntities ce = new WebProgramAREntities())
            {
                NivelEjercicio NivelEjercicioOrig = ce.NivelEjercicios.Single(o => o.NivelEjercicioId == NivelEjercicio.NivelEjercicioId);
                ce.SaveChanges();
            }
        }

        private static void Modificar(NivelEjercicio NivelEjercicio, WebProgramAREntities db)
        {
            NivelEjercicio NivelEjercicioOrig = db.NivelEjercicios.Single(u => u.NivelEjercicioId == NivelEjercicio.NivelEjercicioId);

            db.ObjectStateManager.ChangeObjectState(NivelEjercicioOrig, EntityState.Modified);
            db.SaveChanges();
        }

        public static void Eliminar(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                NivelEjercicio NivelEjercicio = db.NivelEjercicios.Single(u => u.NivelEjercicioId == id);
                //NivelEjercicio.Status = false; //baja lógica

                db.NivelEjercicios.DeleteObject(NivelEjercicio);
                db.SaveChanges();
            }
        }

        //static WebProgramAREntities db = new WebProgramAREntities();
        public static int ContarCantidad(int idNivelEjercicio, string apellido)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return GetNivelEjercicios(idNivelEjercicio, apellido, db).Count();
            }
        }

        public static IEnumerable<NivelEjercicio> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, int idNivelEjercicio, string apellido)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                if (paginaActual < 1) paginaActual = 1;


                IQueryable<NivelEjercicio> query = GetNivelEjercicios(idNivelEjercicio, apellido, db);

                if (sortColumns.Contains("TipoNivelEjercicio"))
                {
                    sortColumns = sortColumns.Replace("TipoNivelEjercicio", "TipoNivelEjercicio.Description");
                }


                return query.OrderUsingSortExpression(sortColumns)
                            .Skip((paginaActual - 1) * personasPorPagina)
                            .Take(personasPorPagina)
                            .ToList();
            }
        }

        private static IQueryable<NivelEjercicio> GetNivelEjercicios(int idNivelEjercicio, string apellido, WebProgramAREntities db)
        {
            IQueryable<NivelEjercicio> query = from u in db.NivelEjercicios
                                     //where u.Status == true &&
                                     //(idNivelEjercicio == 0 || u.NivelEjercicioId == idNivelEjercicio) && u.LastName.Contains(apellido)
                                     select u;
            return query;
        }   

      
    }
}
