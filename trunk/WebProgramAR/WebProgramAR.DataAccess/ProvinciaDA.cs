using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;
using WebProgramAR.DataAccess.Interfases;

namespace WebProgramAR.DataAccess
{
    public class ProvinciaDA : IFiltrablePorSeguridadPorValor
    {
        public static string _nombreTabla = "Provincia";

        public static Provincia GetProvinciaById(string id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.Provincias.Include("TipoProvincia").Include("Provincias").Single(u => u.ProvinciaId  == id);
            }
        }

        public static void Alta(Provincia Provincia)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                db.Provincias.AddObject(Provincia);
                db.SaveChanges();
            }
        }

        public static void Modificar(Provincia Provincia)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Modificar(Provincia, db);
            }
        }

        public static void ModificarUltimoLogin(Provincia Provincia)
        {
            using (WebProgramAREntities ce = new WebProgramAREntities())
            {
                Provincia ProvinciaOrig = ce.Provincias.Single(o => o.ProvinciaId == Provincia.ProvinciaId);
                ce.SaveChanges();
            }
        }

        private static void Modificar(Provincia Provincia, WebProgramAREntities db)
        {
            Provincia ProvinciaOrig = db.Provincias.Single(u => u.ProvinciaId == Provincia.ProvinciaId);

            db.ObjectStateManager.ChangeObjectState(ProvinciaOrig, EntityState.Modified);
            db.SaveChanges();
        }

        public static void Eliminar(string id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Provincia Provincia = db.Provincias.Single(u => u.ProvinciaId == id);
                //Provincia.Status = false; //baja lógica

                db.Provincias.DeleteObject(Provincia);
                db.SaveChanges();
            }
        }

        //static WebProgramAREntities db = new WebProgramAREntities();
        public static int ContarCantidad(string idProvincia, string apellido)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return GetProvincias(idProvincia, apellido, db).Count();
            }
        }

        public static IEnumerable<Provincia> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, string idProvincia, string apellido)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                if (paginaActual < 1) paginaActual = 1;


                IQueryable<Provincia> query = GetProvincias(idProvincia, apellido, db);

                if (sortColumns.Contains("TipoProvincia"))
                {
                    sortColumns = sortColumns.Replace("TipoProvincia", "TipoProvincia.Description");
                }


                return query.OrderUsingSortExpression(sortColumns)
                            .Skip((paginaActual - 1) * personasPorPagina)
                            .Take(personasPorPagina)
                            .ToList();
            }
        }

        private static IQueryable<Provincia> GetProvincias(string idProvincia, string apellido, WebProgramAREntities db)
        {
            IQueryable<Provincia> query = from u in db.Provincias
                                     //where u.Status == true &&
                                     //(idProvincia == 0 || u.ProvinciaId == idProvincia) && u.LastName.Contains(apellido)
                                     select u;
            return query;
        }



        #region IFiltrablePorSeguridadPorValor Members

        public List<EntidadProgramARBase> Filtrar(List<EntidadProgramARBase> lista, Usuario user, TipoUsuario tipo)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
