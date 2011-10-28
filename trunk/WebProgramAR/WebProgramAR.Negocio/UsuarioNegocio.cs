using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using WebProgramAR.DataAccess;

namespace WebProgramAR.Negocio
{
    public class UsuarioNegocio
    {
        public static Usuario GetUsuarioById(int id)
        {
            return UsuarioDA.GetUsuarioById(id);
        }

        public static void Alta(Usuario usuario)
        {
            usuario.FechaAlta = DateTime.Now;
            UsuarioDA.Alta(usuario);
        }

        public static void Modificar(Usuario usuario)
        {
            UsuarioDA.Modificar(usuario);
        }

        public static void Eliminar(int id)
        {
            UsuarioDA.Eliminar(id);
        }

        public static int ContarCantidad(int idUsuario, string apellido)
        {
            return UsuarioDA.ContarCantidad(idUsuario, apellido);
        }

        public static IEnumerable<Usuario> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, int idUsuario, string apellido)
        {
            return UsuarioDA.ObtenerPagina(paginaActual, personasPorPagina, sortColumns, idUsuario, apellido);
        }

        public static Usuario GetUsuarioByLoginUsuario(string loginUsuario)
        {
            return UsuarioDA.GetUsuarioByLoginUsuario(loginUsuario);
        }

        public static void ModificarUltimoLogin(Usuario usuario)
        {
            UsuarioDA.ModificarUltimoLogin(usuario);
        }
    }
}
