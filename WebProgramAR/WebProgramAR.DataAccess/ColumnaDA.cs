using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;

namespace WebProgramAR.DataAccess
{
    public class ColumnaDA 
    {
        public static string _nombreTabla = "Columna";

        public static Columna GetColumnaById(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.Columnas.Include("Tipo").Single(u => u.ColumnaId  == id);
            } 
        }      

        public static IEnumerable<Columna> GetColumnasByTabla(int tablaId)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {

                IQueryable<Columna> query = from u in db.Columnas.Include("Tipo")
                                              where (u.TablaId == tablaId)
                                              select u;
                return query.ToList();
            }
        }

        public static IEnumerable<Columna> GetColumnas()
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {

                IQueryable<Columna> query = from u in db.Columnas
                                            select u;
                return query.ToList();
            }
        }


        
    }
}
