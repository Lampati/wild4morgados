using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;

namespace WebProgramAR.DataAccess
{
    public class TipoUsuarioDA
    {
        public static TipoUsuario GetTipoUsuarioById(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.TipoUsuarios.Include("TipoTipoUsuario").Include("TipoUsuarios").Single(u => u.TipoUsuarioId  == id);
            }
        }

        public static void Alta(TipoUsuario TipoUsuario)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                db.TipoUsuarios.AddObject(TipoUsuario);
                db.SaveChanges();
            }
        }

        public static void Modificar(TipoUsuario TipoUsuario)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Modificar(TipoUsuario, db);
            }
        }

        public static void ModificarUltimoLogin(TipoUsuario TipoUsuario)
        {
            using (WebProgramAREntities ce = new WebProgramAREntities())
            {
                TipoUsuario TipoUsuarioOrig = ce.TipoUsuarios.Single(o => o.TipoUsuarioId == TipoUsuario.TipoUsuarioId);
                ce.SaveChanges();
            }
        }

        private static void Modificar(TipoUsuario TipoUsuario, WebProgramAREntities db)
        {
            TipoUsuario TipoUsuarioOrig = db.TipoUsuarios.Single(u => u.TipoUsuarioId == TipoUsuario.TipoUsuarioId);

            db.ObjectStateManager.ChangeObjectState(TipoUsuarioOrig, EntityState.Modified);
            db.SaveChanges();
        }

        public static void Eliminar(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                TipoUsuario TipoUsuario = db.TipoUsuarios.Single(u => u.TipoUsuarioId == id);
                //TipoUsuario.Status = false; //baja lógica

                db.TipoUsuarios.DeleteObject(TipoUsuario);
                db.SaveChanges();
            }
        }

        //static WebProgramAREntities db = new WebProgramAREntities();
        public static int ContarCantidad(int idTipoUsuario, string apellido)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return GetTipoUsuarios(idTipoUsuario, apellido, db).Count();
            }
        }

        public static IEnumerable<TipoUsuario> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, int idTipoUsuario, string apellido)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                if (paginaActual < 1) paginaActual = 1;


                IQueryable<TipoUsuario> query = GetTipoUsuarios(idTipoUsuario, apellido, db);

                if (sortColumns.Contains("TipoTipoUsuario"))
                {
                    sortColumns = sortColumns.Replace("TipoTipoUsuario", "TipoTipoUsuario.Description");
                }


                return query.OrderUsingSortExpression(sortColumns)
                            .Skip((paginaActual - 1) * personasPorPagina)
                            .Take(personasPorPagina)
                            .ToList();
            }
        }

        private static IQueryable<TipoUsuario> GetTipoUsuarios(int idTipoUsuario, string apellido, WebProgramAREntities db)
        {
            IQueryable<TipoUsuario> query = from u in db.TipoUsuarios
                                     //where u.Status == true &&
                                     //(idTipoUsuario == 0 || u.TipoUsuarioId == idTipoUsuario) && u.LastName.Contains(apellido)
                                     select u;
            return query;
        }   

      
    }
}
