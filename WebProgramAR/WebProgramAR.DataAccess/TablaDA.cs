using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;

namespace WebProgramAR.DataAccess
{
    public class TablaDA 
    {
        public static string _nombreTabla = "Tabla";

        public static Tabla GetTablaById(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.Tablas.Single(u => u.TablaId  == id);
            } 
        }

        public static IEnumerable<Tabla> GetTablas()
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {

                IQueryable<Tabla> query = from u in db.Tablas
                                            select u;
                return query.ToList();
            }
        }

        


        
    }
}
