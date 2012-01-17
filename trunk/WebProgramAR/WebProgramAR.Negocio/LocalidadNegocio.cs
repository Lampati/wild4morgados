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
        public static IQueryable<Localidad> GetLocalidadesByProvincia(string ProvinciaId)
        {
            return LocalidadDA.GetLocalidadesByProvincia(ProvinciaId);
        }
        public static void Alta(Localidad Localidad)
        {
            LocalidadDA.Alta(Localidad);
        }

        public static void Modificar(Localidad Localidad)
        {
            LocalidadDA.Modificar(Localidad);
        }

        public static void Eliminar(string id)
        {
            LocalidadDA.Eliminar(id);
        }

        public static int ContarCantidad(string idLocalidad, string apellido)
        {
            return LocalidadDA.ContarCantidad(idLocalidad, apellido);
        }

        public static IEnumerable<Localidad> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, string idLocalidad, string apellido)
        {
            return LocalidadDA.ObtenerPagina(paginaActual, personasPorPagina, sortColumns, idLocalidad, apellido);
        }

      
    }
}
