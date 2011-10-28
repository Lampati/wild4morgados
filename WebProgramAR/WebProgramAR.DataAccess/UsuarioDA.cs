using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;

namespace WebProgramAR.DataAccess
{
    public class UsuarioDA
    {
        public static Usuario GetUsuarioById(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.Usuarios.Include("TipoUsuario").Include("Cursos").Single(u => u.UsuarioId  == id);
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

            //usuarioOrig.LastName = usuario.LastName;
            //usuarioOrig.LoginUsuario = usuario.LoginUsuario;
            //usuarioOrig.MarketId = usuario.MarketId;
            //usuarioOrig.Name = usuario.Name;
            //usuarioOrig.Status = usuario.Status;
            //usuarioOrig.UsuarioTypeId = usuario.UsuarioTypeId;

            //usuarioOrig.ProfileUsuarios.Clear();

            //foreach (ProfileUsuario prof in usuario.ProfileUsuarios)
            //{
            //    usuarioOrig.ProfileUsuarios.Add(prof);
            //}


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
        public static int ContarCantidad(int idUsuario, string apellido)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return GetUsuarios(idUsuario, apellido, db).Count();
            }
        }

        public static IEnumerable<Usuario> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, int idUsuario, string apellido)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                if (paginaActual < 1) paginaActual = 1;


                IQueryable<Usuario> query = GetUsuarios(idUsuario, apellido, db);

               
                return query.OrderUsingSortExpression(sortColumns)
                            .Skip((paginaActual - 1) * personasPorPagina)
                            .Take(personasPorPagina)
                            .ToList();
            }
        }

        private static IQueryable<Usuario> GetUsuarios(int idUsuario, string apellido, WebProgramAREntities db)
        {
            IQueryable<Usuario> query = from u in db.Usuarios.Include("Market").Include("UsuarioType")
                                     //where u.Status == true &&
                                     //(idUsuario == 0 || u.UsuarioId == idUsuario) && u.LastName.Contains(apellido)
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
    }
}
