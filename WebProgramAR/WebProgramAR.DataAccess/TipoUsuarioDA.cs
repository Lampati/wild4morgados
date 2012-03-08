using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;

namespace WebProgramAR.DataAccess
{
    public class TipoUsuarioDA 
    {
        public static string _nombreTabla = "Curso";

        public static TipoUsuario GetTipoUsuarioById(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.TipoUsuarios.Single(u => u.TipoUsuarioId  == id);
            }
        }

    
        public static IEnumerable<TipoUsuario> GetTiposUsuario()
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                IQueryable<TipoUsuario> query = from u in db.TipoUsuarios
                                                select u;
                return query.ToList();
            }
        }

        public static TipoUsuario GetTipoUsuarioByName(string p)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.TipoUsuarios.Single(u => u.Descripcion.ToUpper() == p.ToUpper());
            }
        }


        #region IFiltrablePorSeguridadPorValor Members

        public static List<EntidadProgramARBase> Filtrar(List<EntidadProgramARBase> lista, Usuario user, TipoUsuario tipo)
        {
            throw new NotImplementedException();
        }

        #endregion

      
    }
}
