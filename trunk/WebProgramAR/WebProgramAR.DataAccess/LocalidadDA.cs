using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;
using WebProgramAR.DataAccess.Interfases;

namespace WebProgramAR.DataAccess
{
    public class LocalidadDA : IFiltrablePorSeguridadPorValor
    {
        public static string _nombreTabla = "Localidad";

        public static Localidad GetLocalidadById(string id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.Localidades.Include("TipoLocalidad").Include("Localidades").Single(u => u.LocalidadId  == id);
            }
        }

        public static void Alta(Localidad Localidad)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                db.Localidades.AddObject(Localidad);
                db.SaveChanges();
            }
        }

        public static void Modificar(Localidad Localidad)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Modificar(Localidad, db);
            }
        }

        public static void ModificarUltimoLogin(Localidad Localidad)
        {
            using (WebProgramAREntities ce = new WebProgramAREntities())
            {
                Localidad LocalidadOrig = ce.Localidades.Single(o => o.LocalidadId == Localidad.LocalidadId);
                ce.SaveChanges();
            }
        }

        private static void Modificar(Localidad Localidad, WebProgramAREntities db)
        {
            Localidad LocalidadOrig = db.Localidades.Single(u => u.LocalidadId == Localidad.LocalidadId);

            db.ObjectStateManager.ChangeObjectState(LocalidadOrig, EntityState.Modified);
            db.SaveChanges();
        }

        public static void Eliminar(string id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Localidad Localidad = db.Localidades.Single(u => u.LocalidadId == id);
                //Localidad.Status = false; //baja lógica

                db.Localidades.DeleteObject(Localidad);
                db.SaveChanges();
            }
        }

        //static WebProgramAREntities db = new WebProgramAREntities();
        public static int ContarCantidad(string idLocalidad, string apellido)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return GetLocalidades(idLocalidad, apellido, db).Count();
            }
        }

        public static IEnumerable<Localidad> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, string idLocalidad, string apellido)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                if (paginaActual < 1) paginaActual = 1;


                IQueryable<Localidad> query = GetLocalidades(idLocalidad, apellido, db);

                if (sortColumns.Contains("TipoLocalidad"))
                {
                    sortColumns = sortColumns.Replace("TipoLocalidad", "TipoLocalidad.Description");
                }


                return query.OrderUsingSortExpression(sortColumns)
                            .Skip((paginaActual - 1) * personasPorPagina)
                            .Take(personasPorPagina)
                            .ToList();
            }
        }

        private static IQueryable<Localidad> GetLocalidades(string idLocalidad, string apellido, WebProgramAREntities db)
        {
            IQueryable<Localidad> query = from u in db.Localidades
                                     //where u.Status == true &&
                                     //(idLocalidad == 0 || u.LocalidadId == idLocalidad) && u.LastName.Contains(apellido)
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
