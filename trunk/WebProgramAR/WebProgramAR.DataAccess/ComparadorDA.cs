using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;

namespace WebProgramAR.DataAccess
{
    public class ComparadorDA 
    {
        public static string _nombreTabla = "Comparador";

        public static Comparador GetComparadorById(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.Comparadors.Single(u => u.ComparadorId  == id);
            } 
        }      

        public static IEnumerable<Comparador> GetComparadoresByTipo(Tipo tipo)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {

                IQueryable<Comparador> query = from u in db.Comparadors.Include("Tipoes")
                                              select u;
                return query.ToList().FindAll(x => x.Tipoes.Contains(tipo, new MyTipoComparer()));
            }
        }

        public static IEnumerable<Comparador> GetComparadores()
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {

                IQueryable<Comparador> query = from u in db.Comparadors
                                            select u;
                return query.ToList();
            }
        }


        
    }

    public class MyTipoComparer : IEqualityComparer<Tipo>
    {
        public bool Equals(Tipo x, Tipo y)
        {
            return (x.TipoId == y.TipoId);
        }

        public int GetHashCode(Tipo obj)
        {
            return obj.TipoId.ToString().GetHashCode();
        }
    }
}
