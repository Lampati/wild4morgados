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
        public static IEnumerable<Pais> GetPaises()
        {
            try
            {
                using (WebProgramAREntities db = new WebProgramAREntities())
                {
                    IQueryable<Pais> query = from u in db.Paises
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
