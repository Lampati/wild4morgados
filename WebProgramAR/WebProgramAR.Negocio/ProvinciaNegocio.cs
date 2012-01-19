using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using WebProgramAR.DataAccess;

namespace WebProgramAR.Negocio
{
    public class ProvinciaNegocio
    {
        public static Provincia GetProvinciaById(string id)
        {
            return ProvinciaDA.GetProvinciaById(id);
        }

        public static IEnumerable<Provincia> GetProvinciasByPais(string PaisId)
        {
            return ProvinciaDA.GetProvinciasByPais(PaisId);
        }

        public static IEnumerable<Provincia> GetProvincias()
        {
            return ProvinciaDA.GetProvincias();
        }
      
      
    }
}
