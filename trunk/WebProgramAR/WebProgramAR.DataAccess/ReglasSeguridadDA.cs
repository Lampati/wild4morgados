using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;

namespace WebProgramAR.DataAccess
{
    public class ReglasSeguridadDA
    {     
        public static IEnumerable<ReglasSeguridad> GetReglasByTablaByUsuarioByTipoUsuario(string tablaNombre, int? userId, int? tipoUserId)
        {
           try
           {
               using (WebProgramAREntities db = new WebProgramAREntities())
                {
                    IQueryable<ReglasSeguridad> query = from u in db.ReglasSeguridads.Include("Tabla").Include("Columna").Include("Comparador").Include("Columna.Tipo")
                                                        where (   
                                                            u.Tabla.Nombre.ToUpper() == tablaNombre.ToUpper()
                                                            && ((u.UsuarioId == -1 || u.UsuarioId == null) || u.UsuarioId == userId )
                                                            && (u.TipoUsuarioId == null || u.TipoUsuarioId == tipoUserId)
                                                            && u.Activa
                                                        )
                                                        select u;
                    return query.ToList();
                }
            }
           catch (Exception ex)
           {
               throw new Exception(ex.Message.ToString());
           }
        }


        public static ReglasSeguridad GetReglaSeguridadById(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.ReglasSeguridads.Include("Tabla").Include("Columna").Include("Comparador").Include("Columna.Tipo").Include("Comparador.Tipoes").Single(u => u.ReglaId == id);
            }
        }

        public static void Alta(ReglasSeguridad ReglasSeguridad)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                db.ReglasSeguridads.AddObject(ReglasSeguridad);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ReglasSeguridad"></param>
        /// <param name="asignarEjercicios">Indica si tengo que reemplazar los ejercicios del ReglasSeguridad</param>
        public static void Modificar(ReglasSeguridad regla)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Modificar(regla, db);
            }
        }



        private static void Modificar(ReglasSeguridad reglaModif,  WebProgramAREntities db)
        {
            ReglasSeguridad reglaOrig = db.ReglasSeguridads.Single(u => u.ReglaId == reglaModif.ReglaId);

            reglaOrig.TablaId = reglaModif.TablaId;
            reglaOrig.ColumnaId = reglaModif.ColumnaId;
            reglaOrig.ComparadorId = reglaModif.ComparadorId;
            reglaOrig.UsuarioId = reglaModif.UsuarioId;
            reglaOrig.TipoUsuarioId = reglaModif.TipoUsuarioId;
            reglaOrig.Activa = reglaModif.Activa;
            reglaOrig.Valor = reglaModif.Valor;

            db.ObjectStateManager.ChangeObjectState(reglaOrig, EntityState.Modified);
            db.SaveChanges();
        }

        

        public static void Eliminar(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                ReglasSeguridad regla = db.ReglasSeguridads.Single(u => u.ReglaId == id);
                //ReglasSeguridad.Status = false; //baja lógica

                db.ReglasSeguridads.DeleteObject(regla);
                db.SaveChanges();
            }
        }

        //static WebProgramAREntities db = new WebProgramAREntities();
        public static int ContarCantidad(int tablaId, int columnaId, int comparadorId, string usuario, int? tipoUsuarioId, bool? activa)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return GetReglasSeguridads(tablaId, columnaId, comparadorId, usuario, tipoUsuarioId, activa, db).ToList().Count;
            }
        }

        public static IEnumerable<ReglasSeguridad> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, int tablaId, int columnaId, int comparadorId, string usuario, int? tipoUsuarioId, bool? activa)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                if (paginaActual < 1) paginaActual = 1;


                IQueryable<ReglasSeguridad> query = GetReglasSeguridads(tablaId, columnaId, comparadorId, usuario, tipoUsuarioId, activa, db);

                if (sortColumns.Contains("Tabla"))
                {
                    sortColumns = sortColumns.Replace("Tabla", "Tabla.Nombre");
                }

                return query.OrderUsingSortExpression(sortColumns)
                            .Skip((paginaActual - 1) * personasPorPagina)
                            .Take(personasPorPagina)
                            .ToList();
            }
        }

        private static IQueryable<ReglasSeguridad> GetReglasSeguridads(int tablaId, int columnaId, int comparadorId, string usuario, int? tipoUsuarioId, bool? activa, WebProgramAREntities db)
        {
            bool esVacioUsuario = string.IsNullOrWhiteSpace(usuario);

            IQueryable<ReglasSeguridad> query = from u in db.ReglasSeguridads.Include("Tabla").Include("Columna").Include("Comparador").Include("Columna.Tipo").Include("Comparador.Tipoes").Include("Usuario").Include("TipoUsuario")
                                                where (tablaId == -1 || u.TablaId == tablaId)
                                                && (columnaId == -1 || u.ColumnaId == columnaId)
                                                && (comparadorId == -1 || u.ComparadorId == comparadorId)
                                                && (esVacioUsuario || (u.Usuario != null && u.Usuario.UsuarioNombre.Contains(usuario)))
                                                //&& (tipoUsuarioId == -1 || u.TipoUsuarioId.Equals(tipoUsuarioId))
                                                && (activa == null || u.Activa == activa.Value)
                                                select u;
            return query;
        }

      
    }
}
