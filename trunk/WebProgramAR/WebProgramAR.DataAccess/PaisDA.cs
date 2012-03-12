using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebProgramAR.Entidades;

namespace WebProgramAR.DataAccess
{
    public class PaisDA 
    {
        public static string _nombreTabla = "Pais";

        public static Pais GetPaisById(string id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.Paises.Single(u => u.PaisId  == id);
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
        public static IEnumerable<Pais> GetPaises(Usuario userLogueado)
        {
            try
            {
                using (WebProgramAREntities db = new WebProgramAREntities())
                {
                    IQueryable<Pais> query = from u in db.Paises
                                                       select u;
                    

                    List<Pais> aux = query.ToList();

                    float tiempo;

                    return Seguridad.SeguridadXValorManager.Filtrar<Pais>(aux, _nombreTabla, userLogueado, out tiempo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
     
    }
}
