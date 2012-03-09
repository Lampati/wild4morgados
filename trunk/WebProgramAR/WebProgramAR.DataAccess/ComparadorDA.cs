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

        public static IEnumerable<Comparador> GetComparadoresByTipo(int tipoId)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {

                IQueryable<Comparador> query = from u in db.Comparadors
                                              where (u.TipoId == tipoId)
                                              select u;
                return query.ToList();
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
}
