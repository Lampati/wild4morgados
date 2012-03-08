using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;

namespace WebProgramAR.DataAccess
{
    public class EstadoEjercicioDA 
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


        public static EstadoEjercicio GetEstadoEjercicioByName(string name)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return (from u in db.EstadoEjercicios where u.Descripcion.ToUpper() == name.ToUpper() select u).ToArray()[0];
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
