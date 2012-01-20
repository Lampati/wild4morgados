using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;
using WebProgramAR.DataAccess.Interfases;

namespace WebProgramAR.DataAccess
{
    public class EstadoEjercicioDA : IFiltrablePorSeguridadPorValor
    {
        public static string _nombreTabla = "EstadoEjercicio";

        public static EstadoEjercicio GetEstadoEjercicioById(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.EstadoEjercicios.Single(u => u.EstadoEjercicioId  == id);
            }
        }

        public static IEnumerable<EstadoEjercicio> GetEstadoEjercicios()
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.EstadoEjercicios.ToList();
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
