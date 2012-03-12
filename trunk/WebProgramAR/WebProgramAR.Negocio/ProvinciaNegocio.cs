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

        public static IEnumerable<Provincia> GetProvinciasByPais(string PaisId, Usuario userLogueado)
        {
            return ProvinciaDA.GetProvinciasByPais(PaisId, userLogueado);
        }

        public static IEnumerable<Provincia> GetProvincias(Usuario userLogueado)
        {
            return ProvinciaDA.GetProvincias(userLogueado);
        }
      
      
    }
}
