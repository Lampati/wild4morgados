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
        public static void Alta(Provincia Provincia)
        {
            ProvinciaDA.Alta(Provincia);
        }

        public static void Modificar(Provincia Provincia)
        {
            ProvinciaDA.Modificar(Provincia);
        }

        public static void Eliminar(string id)
        {
            ProvinciaDA.Eliminar(id);
        }

        public static int ContarCantidad(string idProvincia, string apellido)
        {
            return ProvinciaDA.ContarCantidad(idProvincia, apellido);
        }

        public static IEnumerable<Provincia> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, string idProvincia, string apellido)
        {
            return ProvinciaDA.ObtenerPagina(paginaActual, personasPorPagina, sortColumns, idProvincia, apellido);
        }

      
    }
}
