using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using WebProgramAR.DataAccess;
using System.Web.Security;

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
            //AgregarRolAUsuario(usuario);
            usuario.FechaAlta = DateTime.Now;
            UsuarioDA.Alta(usuario);
        }

        public static void Modificar(Usuario usuario)
        {
            UsuarioDA.Modificar(usuario);
        }

        public static void Eliminar(Usuario u)
        {
            Usuario usuarioAEliminar = GetUsuarioById(u.UsuarioId);
            CursoNegocio.EliminarCursosDeUsuario(usuarioAEliminar.UsuarioId);



            UsuarioNegocio.QuitarRolesUsuario(usuarioAEliminar);

            UsuarioDA.Eliminar(u.UsuarioId);
        }

        public static int ContarCantidad(string nombre, string apellido, string usuarioNombre, int tipoUsuarioId, string pais, string provincia, string localidad, Usuario userLogueado)
        {
            return UsuarioDA.ContarCantidad(nombre, apellido, usuarioNombre, tipoUsuarioId, pais, provincia, localidad, userLogueado);
        }

        public static IEnumerable<Usuario> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, string nombre, string apellido, string usuarioNombre, int tipoUsuarioId, string pais, string provincia, string localidad, Usuario userLogueado)
        {
            return UsuarioDA.ObtenerPagina(paginaActual, personasPorPagina, sortColumns, nombre, apellido, usuarioNombre, tipoUsuarioId, pais, provincia, localidad, userLogueado);
        }

        public static Usuario GetUsuarioByLoginUsuario(string loginUsuario)
        {
            return UsuarioDA.GetUsuarioByLoginUsuario(loginUsuario);
        }

        public static IEnumerable<Usuario> GetUsuarioByLoginUsuarioAutocomplete(string loginUsuario, Usuario userLogueado)
        {
            return UsuarioDA.GetUsuarioByLoginUsuarioAutocomplete(loginUsuario, userLogueado);
        }

        public static void ModificarUltimoLogin(Usuario usuario)
        {
            UsuarioDA.ModificarUltimoLogin(usuario);
        }

        public static void QuitarRolesUsuario(Usuario userActual)
        {
            if (userActual.TipoUsuario.Descripcion.ToLower() == Globales.Globals.TiposRoles.profesor.ToString())
            {
                Roles.RemoveUserFromRole(userActual.UsuarioNombre, Globales.Globals.TiposRoles.profesor.ToString());
            }
            else if (userActual.TipoUsuario.Descripcion.ToLower() == Globales.Globals.TiposRoles.moderador.ToString())
            {
                Roles.RemoveUserFromRole(userActual.UsuarioNombre, Globales.Globals.TiposRoles.moderador.ToString());
            }
            else if (userActual.TipoUsuario.Descripcion.ToLower() == Globales.Globals.TiposRoles.administrador.ToString())
            {
                Roles.RemoveUserFromRole(userActual.UsuarioNombre, Globales.Globals.TiposRoles.administrador.ToString());
            }
        }

        public static void AgregarRolAUsuario(Usuario usuario)
        {
            if (usuario.TipoUsuario.Descripcion.ToLower() == Globales.Globals.TiposRoles.profesor.ToString())
            {
                Roles.AddUserToRole(usuario.UsuarioNombre, Globales.Globals.TiposRoles.profesor.ToString());
            }
            else if (usuario.TipoUsuario.Descripcion.ToLower() == Globales.Globals.TiposRoles.moderador.ToString())
            {
                Roles.AddUserToRole(usuario.UsuarioNombre, Globales.Globals.TiposRoles.moderador.ToString());
            }
            else if (usuario.TipoUsuario.Descripcion.ToLower() == Globales.Globals.TiposRoles.administrador.ToString())
            {
                Roles.AddUserToRole(usuario.UsuarioNombre, Globales.Globals.TiposRoles.administrador.ToString());
            }
        }

        public static bool ExisteUsuarioById(int id)
        {
            return UsuarioDA.ExisteUsuarioById(id);
        }
    }
}
