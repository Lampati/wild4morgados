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

        public static int ContarCantidad(string nombre, string apellido, string usuarioNombre, int tipoUsuarioId, string pais, string provincia, string localidad)
        {
            return UsuarioDA.ContarCantidad(nombre, apellido, usuarioNombre, tipoUsuarioId, pais, provincia, localidad);
        }

        public static IEnumerable<Usuario> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, string nombre, string apellido, string usuarioNombre, int tipoUsuarioId, string pais, string provincia, string localidad)
        {
            return UsuarioDA.ObtenerPagina(paginaActual, personasPorPagina, sortColumns,  nombre,  apellido,  usuarioNombre,  tipoUsuarioId,  pais,  provincia,  localidad);
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
