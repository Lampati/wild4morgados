using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;
using WebProgramAR.DataAccess.Interfases;

namespace WebProgramAR.DataAccess
{
    public class NivelEjercicioDA : IFiltrablePorSeguridadPorValor
    {
        public static string _nombreTabla = "NivelEjercicio";

        public static NivelEjercicio GetNivelEjercicioById(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.NivelEjercicios.Single(u => u.NivelEjercicioId  == id);
            }
        }

        public static IEnumerable<NivelEjercicio> GetNivelEjercicios()
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                IQueryable<NivelEjercicio> query = from u in db.NivelEjercicios
                                                   select u;

                return query.ToList();
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
