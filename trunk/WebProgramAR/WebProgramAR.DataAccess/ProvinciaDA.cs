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
                return db.Provincias.Single(u => u.ProvinciaId  == id);
            }
        }
     
        public static IEnumerable<Provincia> GetProvinciasByPais(string idPais)
        {
           try
           {
               using (WebProgramAREntities db = new WebProgramAREntities())
                {
                    IQueryable<Provincia> query = from u in db.Provincias
                                                  where (u.PaisId == idPais)
                                                  select u;
                    return query.ToList();
                }
            }
           catch (Exception ex)
           {
               throw new Exception(ex.Message.ToString());
           }
        }

        public static IEnumerable<Provincia> GetProvincias()
        {
            try
            {
                using (WebProgramAREntities db = new WebProgramAREntities())
                {
                    IQueryable<Provincia> query = from u in db.Provincias
                                                  select u;
                    return query.ToList();
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
