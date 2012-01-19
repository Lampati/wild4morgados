using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using WebProgramAR.DataAccess;

namespace WebProgramAR.Negocio
{
    public class TipoUsuarioNegocio
    {
        public static TipoUsuario GetTipoUsuarioById(int id)
        {
            return TipoUsuarioDA.GetTipoUsuarioById(id);
        }

        public static void Alta(TipoUsuario TipoUsuario)
        {
            TipoUsuarioDA.Alta(TipoUsuario);
        }

        public static void Modificar(TipoUsuario TipoUsuario)
        {
            TipoUsuarioDA.Modificar(TipoUsuario);
        }

        public static void Eliminar(int id)
        {
            TipoUsuarioDA.Eliminar(id);
        }

        public static int ContarCantidad(int idTipoUsuario, string apellido)
        {
            return TipoUsuarioDA.ContarCantidad(idTipoUsuario, apellido);
        }

        public static IEnumerable<TipoUsuario> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, int idTipoUsuario, string apellido)
        {
            return TipoUsuarioDA.ObtenerPagina(paginaActual, personasPorPagina, sortColumns, idTipoUsuario, apellido);
        }
        public static IEnumerable<TipoUsuario> GetTiposUsuario()
        {
            return TipoUsuarioDA.GetTiposUsuario();
        }
      
    }
}
