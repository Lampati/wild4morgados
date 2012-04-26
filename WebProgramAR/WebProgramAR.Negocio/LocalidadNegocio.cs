using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using WebProgramAR.DataAccess;

namespace WebProgramAR.Negocio
{
    public class LocalidadNegocio
    {
        public static Localidad GetLocalidadById(string id)
        {
            return LocalidadDA.GetLocalidadById(id);
        }

        
        public static IEnumerable<Localidad> GetLocalidadesByLocalidadByProvinciaByPais(string Localidad, string provinciaId, string paisId, Usuario userLogueado)
        {
            return LocalidadDA.GetLocalidadesByLocalidadByProvinciaByPais(Localidad, provinciaId, paisId, userLogueado);
        }

        public static IEnumerable<Localidad> GetLocalidadesByProvinciaByPais(string provinciaId, string paisId)
        {
            return LocalidadDA.GetLocalidadesByProvinciaByPais(provinciaId, paisId);
        }

        //no se usa
        public static IEnumerable<Localidad> GetLocalidades(Usuario userLogueado)
        {
            return LocalidadDA.GetLocalidades(userLogueado);
        }
        

     

      
    }
}
