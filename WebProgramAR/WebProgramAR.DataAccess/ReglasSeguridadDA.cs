using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;

namespace WebProgramAR.DataAccess
{
    public class ReglasSeguridadDA
    {     
        public static IEnumerable<ReglasSeguridad> GetReglasByTablaByUsuarioByTipoUsuario(string tablaNombre, int? userId, int? tipoUserId)
        {
           try
           {
               using (WebProgramAREntities db = new WebProgramAREntities())
                {
                    IQueryable<ReglasSeguridad> query = from u in db.ReglasSeguridads.Include("Tabla").Include("Columna").Include("Comparador").Include("Columna.Tipo")
                                                        where (   
                                                            u.Tabla.Nombre.ToUpper() == tablaNombre.ToUpper() && 
                                                            u.UsuarioId == userId &&  
                                                            u.TipoUsuarioId == tipoUserId 
                                                        )
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

      
    }
}
