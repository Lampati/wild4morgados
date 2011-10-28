using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;

namespace WebProgramAR.DataAccess
{
    public class PaisDA
    {
        public static Pais GetPaisById(string id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.Paises.Include("TipoPais").Include("Paises").Single(u => u.PaisId  == id);
            }
        }

        public static void Alta(Pais Pais)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                db.Paises.AddObject(Pais);
                db.SaveChanges();
            }
        }

        public static void Modificar(Pais Pais)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Modificar(Pais, db);
            }
        }

        public static void ModificarUltimoLogin(Pais Pais)
        {
            using (WebProgramAREntities ce = new WebProgramAREntities())
            {
                Pais PaisOrig = ce.Paises.Single(o => o.PaisId == Pais.PaisId);
                ce.SaveChanges();
            }
        }

        private static void Modificar(Pais Pais, WebProgramAREntities db)
        {
            Pais PaisOrig = db.Paises.Single(u => u.PaisId == Pais.PaisId);

            db.ObjectStateManager.ChangeObjectState(PaisOrig, EntityState.Modified);
            db.SaveChanges();
        }

        public static void Eliminar(string id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Pais Pais = db.Paises.Single(u => u.PaisId == id);
                //Pais.Status = false; //baja lógica

                db.Paises.DeleteObject(Pais);
                db.SaveChanges();
            }
        }

        //static WebProgramAREntities db = new WebProgramAREntities();
        public static int ContarCantidad(string idPais, string apellido)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return GetPaises(idPais, apellido, db).Count();
            }
        }

        public static IEnumerable<Pais> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, string idPais, string apellido)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                if (paginaActual < 1) paginaActual = 1;


                IQueryable<Pais> query = GetPaises(idPais, apellido, db);

                if (sortColumns.Contains("TipoPais"))
                {
                    sortColumns = sortColumns.Replace("TipoPais", "TipoPais.Description");
                }


                return query.OrderUsingSortExpression(sortColumns)
                            .Skip((paginaActual - 1) * personasPorPagina)
                            .Take(personasPorPagina)
                            .ToList();
            }
        }

        private static IQueryable<Pais> GetPaises(string idPais, string apellido, WebProgramAREntities db)
        {
            IQueryable<Pais> query = from u in db.Paises
                                     //where u.Status == true &&
                                     //(idPais == 0 || u.PaisId == idPais) && u.LastName.Contains(apellido)
                                     select u;
            return query;
        }   

      
    }
}
