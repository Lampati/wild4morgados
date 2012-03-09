using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;

namespace WebProgramAR.DataAccess
{
    public class LocalidadDA 
    {
        public static string _nombreTabla = "Localidad";

        public static Localidad GetLocalidadById(string id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.Localidades.Single(u => u.LocalidadId  == id);
            } 
        }

     
        public static IEnumerable<Localidad> GetLocalidadesByProvinciaByPais(string idProvincia, string idPais)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {

                IQueryable<Localidad> query = from u in db.Localidades
                                              where (u.ProvinciaId == idProvincia)
                                              && (u.PaisId == idPais)
                                              select u;
                return query.ToList();
            }
        }
        public static IEnumerable<Localidad> GetLocalidadesByLocalidadByProvinciaByPais(string Localidad, string idProvincia, string idPais)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {

                IQueryable<Localidad> query = from u in db.Localidades
                                              where (u.ProvinciaId == idProvincia)
                                              && (u.PaisId == idPais)
                                              && (u.Descripcion.Contains(Localidad))
                                              select u;
                //return query.ToList();

                List<Localidad> aux = query.ToList();

                float tiempo;

                return Seguridad.SeguridadXValorManager.Filtrar<Localidad>(aux, _nombreTabla, null, null, out tiempo);
            }
        }
        public static IEnumerable<Localidad> GetLocalidades()
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {

                IQueryable<Localidad> query = from u in db.Localidades
                                              select u;
                return query.ToList();
            }
        }


        
    }
}
