using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;
using WebProgramAR.DataAccess.Interfases;

namespace WebProgramAR.DataAccess
{
    public class UsuarioDA : IFiltrablePorSeguridadPorValor
    {
        public static string _nombreTabla = "Usuario";

        public static Usuario GetUsuarioById(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.Usuarios.Include("TipoUsuario").Include("Pais").Include("Provincia").Include("Localidad").Single(u => u.UsuarioId == id);
            }
        }
     
        public static void Alta(Usuario usuario)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                db.Usuarios.AddObject(usuario);
                db.SaveChanges();
            }
        }

        public static void Modificar(Usuario usuario)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Modificar(usuario, db);
            }
        }

        public static void ModificarUltimoLogin(Usuario usuario)
        {
            using (WebProgramAREntities ce = new WebProgramAREntities())
            {
                Usuario usuarioOrig = ce.Usuarios.Single(o => o.UsuarioId == usuario.UsuarioId);
                //usuarioOrig.Attempt = 0;
                //usuarioOrig.LastLoginDate = DateTime.Now;
                ce.SaveChanges();
            }
        }

        private static void Modificar(Usuario usuario, WebProgramAREntities db)
        {
            //db.Usuarios.Attach(usuario);
            Usuario usuarioOrig = db.Usuarios.Single(u => u.UsuarioId == usuario.UsuarioId);

            usuario.Nombre = usuario.Nombre;
            usuario.Apellido = usuario.Apellido;            

            usuarioOrig.PaisId = usuario.PaisId;
            usuarioOrig.ProvinciaId = usuario.ProvinciaId;
            usuarioOrig.LocalidadId = usuario.LocalidadId;

            db.ObjectStateManager.ChangeObjectState(usuarioOrig, EntityState.Modified);
            db.SaveChanges();
        }

        public static void Eliminar(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Usuario usuario = db.Usuarios.Single(u => u.UsuarioId == id);
                //usuario.Status = false; //baja lógica
                //db.ObjectStateManager.ChangeObjectState(usuario, EntityState.Modified);
                //db.SaveChanges();
                db.Usuarios.DeleteObject(usuario);
                db.SaveChanges();
            }
        }

        //static WebProgramAREntities db = new WebProgramAREntities();
        public static int ContarCantidad(string nombre, string apellido, string usuarioNombre, int tipoUsuarioId, string pais, string provincia, string localidad)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return GetUsuarios(nombre, apellido, usuarioNombre, tipoUsuarioId, pais, provincia, localidad, db).Count();
            }
        }

        public static IEnumerable<Usuario> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, string nombre, string apellido, string usuarioNombre, int tipoUsuarioId, string pais, string provincia, string localidad)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                if (paginaActual < 1) paginaActual = 1;


                IQueryable<Usuario> query = GetUsuarios(nombre, apellido, usuarioNombre, tipoUsuarioId, pais, provincia, localidad, db);

                if (sortColumns.Contains("TipoUsuario"))
                {
                    sortColumns = sortColumns.Replace("TipoUsuario", "TipoUsuario.TipoUsuarioId");
                }
                if (sortColumns.Contains("Pais"))
                {
                    sortColumns = sortColumns.Replace("Pais", "Pais.PaisId");
                }
                if (sortColumns.Contains("Provincia"))
                {
                    sortColumns = sortColumns.Replace("Provincia", "Provincia.ProvinciaId");
                }
                if (sortColumns.Contains("Localidad"))
                {
                    sortColumns = sortColumns.Replace("Localidad", "Localidad.LocalidadId");
                }
               
                return query.OrderUsingSortExpression(sortColumns)
                            .Skip((paginaActual - 1) * personasPorPagina)
                            .Take(personasPorPagina)
                            .ToList();
            }
        }

        private static IQueryable<Usuario> GetUsuarios(string nombre, string apellido, string usuarioNombre, int tipoUsuarioId, string pais, string provincia, string localidad, WebProgramAREntities db)
        {
            IQueryable<Usuario> query = from u in db.Usuarios.Include("TipoUsuario").Include("Pais").Include("Provincia").Include("Localidad")
                                          where (nombre == "" || u.Nombre.Contains(nombre))
                                          && (apellido == "" || u.Apellido.Contains(apellido))
                                          && (usuarioNombre == "" || u.UsuarioNombre.Contains(usuarioNombre))                                          
                                          && (tipoUsuarioId == -1 || u.TipoUsuarioId == tipoUsuarioId)
                                          && (pais == "" || u.PaisId.Contains(pais))
                                          && (provincia == "" || u.ProvinciaId.Contains(provincia))
                                          && (localidad == "" || u.LocalidadId.Contains(localidad))                                         
                                          
                                          select u;
            return query;
        }

        //public static ChangePassword VerifyLoginUsuarioAndPassword(int Usuarioid)
        //{
        //    using (WebProgramAREntities ce = new WebProgramAREntities())
        //    {
        //        var list = (from o in ce.Usuarios
        //                    join p in ce.Passwords on o.UsuarioId equals p.UsuarioId
        //                    where o.UsuarioId == Usuarioid
        //                    orderby p.Date descending
        //                    select new
        //                    {
        //                        UsuarioId = o.UsuarioId,
        //                        LoginUsuario = o.LoginUsuario,
        //                        LastLoginDate = o.LastLoginDate,
        //                        Attempt = o.Attempt,
        //                        Status = o.Status,
        //                        PasswordId = p.PasswordId,
        //                        PassName = p.Password1,
        //                        Date = p.Date
        //                    }).Take(1);

        //        ChangePassword newChange = new ChangePassword();
        //        Usuario usuario = new Usuario();
        //        Password password = new Password();
        //        foreach (var passInfo in list)
        //        {
        //            usuario.UsuarioId = passInfo.UsuarioId;
        //            usuario.LoginUsuario = passInfo.LoginUsuario;
        //            usuario.LastLoginDate = passInfo.LastLoginDate;
        //            usuario.Attempt = passInfo.Attempt;
        //            usuario.Status = passInfo.Status;
        //            password.PasswordId = passInfo.PasswordId;
        //            password.Password1 = passInfo.PassName;
        //            password.Date = passInfo.Date;
        //        }

        //        newChange.Usuario = usuario;
        //        newChange.Password = password;

        //        return newChange;
        //    }
        //}

        public static Usuario GetUsuarioByLoginUsuario(string loginUsuario)
        {
            try
            {
                using (WebProgramAREntities ce = new WebProgramAREntities())
                {
                    return ce.Usuarios.Single(o => o.UsuarioNombre.Equals(loginUsuario));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public static void IntentosNoValidos(int idUsuario)
        //{
        //    using (WebProgramAREntities ce = new WebProgramAREntities())
        //    {

        //        Usuario usuarioOrig = ce.Usuarios.Single(u => u.UsuarioId == idUsuario);

        //        if (usuarioOrig.Attempt < 5)
        //        {
        //            usuarioOrig.Attempt++;
        //        }
        //        else
        //        {
        //            usuarioOrig.Attempt = 0;
        //            usuarioOrig.Status = false;
        //        }

        //        ce.ObjectStateManager.ChangeObjectState(usuarioOrig, EntityState.Modified);
        //        ce.SaveChanges();
        //    }
        //}

        #region IFiltrablePorSeguridadPorValor Members

        public List<EntidadProgramARBase> Filtrar(List<EntidadProgramARBase> lista, Usuario user, TipoUsuario tipo)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
