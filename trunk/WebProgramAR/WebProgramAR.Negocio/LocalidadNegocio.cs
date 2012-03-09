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

        
        public static IEnumerable<Localidad> GetLocalidadesByLocalidadByProvinciaByPais(string Localidad, string provinciaId, string paisId)
        {
            return LocalidadDA.GetLocalidadesByLocalidadByProvinciaByPais(Localidad, provinciaId, paisId);
        }

        //no se usa
        public static IEnumerable<Localidad> GetLocalidades()
        {
            return LocalidadDA.GetLocalidades();
        }
        

     

      
    }
}
