using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebProgramAR.DataAccess.Interfases;
using WebProgramAR.Entidades;

namespace WebProgramAR.DataAccess
{
    public class PaisDA : IFiltrablePorSeguridadPorValor
    {
        public static string _nombreTabla = "Pais";

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

        private static IQueryable<Pais> GetPaises(string idPais, string descripcion, WebProgramAREntities db)
        {
            using (db = new WebProgramAREntities())
            {
                IQueryable<Pais> query = from u in db.Paises
                                         where (idPais == "" || u.PaisId == idPais) && u.Descripcion.Contains(descripcion)
                                         select u;
                return query;
            }
            
        }
        public static IEnumerable<Pais> GetPaises()
        {
            try
            {

                using (WebProgramAREntities db = new WebProgramAREntities())
                {

                    foreach (Pais result in db.Paises)
                        Console.WriteLine("Product Name: {0}", result.PaisId);
                        
                    IEnumerable<Pais> query = from u in db.Paises
                                             select u;
                    
                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }


        #region IFiltrablePorSeguridadPorValor Members

        public List<EntidadProgramARBase> Filtrar(List<EntidadProgramARBase> lista, Usuario user, TipoUsuario tipo)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
