using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;

namespace WebProgramAR.DataAccess
{
    public class EjercicioDA 
    {
        public static string _nombreTabla = "Ejercicio";

        public static Ejercicio GetEjercicioById(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {

                IQueryable<Ejercicio> query = from u in db.Ejercicios.Include("MensajeModeracion").Include("EstadoEjercicio").Include("Usuario")
                                                where u.EjercicioId  == id select u;
                return query.ToArray()[0];
            }
        }

        public static Ejercicio GetEjercicioByIdOnlyUser(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {

                IQueryable<Ejercicio> query = from u in db.Ejercicios.Include("Usuario")
                                              where u.EjercicioId == id
                                              select u;
                return query.ToList()[0];
            }
        }

        public static void Alta(Ejercicio Ejercicio)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                db.Ejercicios.AddObject(Ejercicio);
                db.SaveChanges();
            }
        }

        public static void Modificar(Ejercicio Ejercicio)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Modificar(Ejercicio, db);
            }
        }

   
        private static void Modificar(Ejercicio ejercicio, WebProgramAREntities db)
        {
            Ejercicio ejercicioOrig = db.Ejercicios.Single(u => u.EjercicioId == ejercicio.EjercicioId);

            ejercicioOrig.EstadoEjercicioId = ejercicio.EstadoEjercicioId;
            ejercicioOrig.Enunciado = ejercicio.Enunciado;
            ejercicioOrig.SolucionTexto = ejercicio.SolucionTexto;
            ejercicioOrig.Global = ejercicio.Global;
            


            db.ObjectStateManager.ChangeObjectState(ejercicioOrig, EntityState.Modified);
            db.SaveChanges();
        }

        public static void Eliminar(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Ejercicio Ejercicio = db.Ejercicios.Single(u => u.EjercicioId == id);
                //Ejercicio.Status = false; //baja lógica

                db.Ejercicios.DeleteObject(Ejercicio);
                db.SaveChanges();
            }
        }

        //static WebProgramAREntities db = new WebProgramAREntities();
        public static int ContarCantidad(string nombre, int usuarioId, int cursoId, int estadoEjercicio, int nivelEjercicio, bool global)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return GetEjercicios(nombre, usuarioId, cursoId, estadoEjercicio, nivelEjercicio, global, db).Count();
            }
        }

        public static IEnumerable<Ejercicio> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, string nombre, int usuarioId, int cursoId, int estadoEjercicio, int nivelEjercicio, bool global)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                if (paginaActual < 1) paginaActual = 1;


                IQueryable<Ejercicio> query = GetEjercicios(nombre, usuarioId, cursoId, estadoEjercicio, nivelEjercicio, global, db);

                if (sortColumns.Contains("Usuario"))
                {
                    sortColumns = sortColumns.Replace("Usuario", "Usuario.UsuarioId");
                }
                //if (sortColumns.Contains("Curso"))
                //{
                //    sortColumns = sortColumns.Replace("Curso", "Curso.CursoId");
                //}
                if (sortColumns.Contains("EstadoEjercicio"))
                {
                    sortColumns = sortColumns.Replace("EstadoEjercicio", "EstadoEjercicio.EstadoEjercicioId");
                }
               



                return query.OrderUsingSortExpression(sortColumns)
                            .Skip((paginaActual - 1) * personasPorPagina)
                            .Take(personasPorPagina)
                            .ToList();
            }
        }

        private static IQueryable<Ejercicio> GetEjercicios(string nombre, int usuarioId, int cursoId, int estadoEjercicio, int nivelEjercicio, bool global, WebProgramAREntities db)
        {
            IQueryable<Ejercicio> query = from u in db.Ejercicios.Include("Cursoes").Include("EstadoEjercicio").Include("Usuario")
                                     where (usuarioId == -1 || u.UsuarioId == usuarioId)                                     
                                     && (cursoId == -1 || u.Cursoes.Count(m => m.CursoId == cursoId) > 0)
                                     && (estadoEjercicio == -1 || u.EstadoEjercicioId == estadoEjercicio)
                                     && (nivelEjercicio == -1 || u.NivelEjercicio == nivelEjercicio)
                                     && (nombre == "" || u.Nombre.Contains(nombre))
                                     select u;
            return query;
        }

        public static IEnumerable<Ejercicio> GetEjerciciosByCursoByUsuarioByNivelByEstado(int usuarioId, int cursoId, int nivelEjercicio, int estadoEjercicio)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                IQueryable<Ejercicio> query = from u in db.Ejercicios.Include("Cursoes").Include("EstadoEjercicio").Include("Usuario")
                                              where (usuarioId == -1 || u.UsuarioId == usuarioId)
                                              && (cursoId == -1 || u.Cursoes.Count(m => m.CursoId == cursoId) > 0)
                                              && (estadoEjercicio == -1 || u.EstadoEjercicioId == estadoEjercicio)
                                              && (nivelEjercicio == -1 || u.NivelEjercicio == nivelEjercicio)
                                              select u;
                return query.ToList();
            }
        }
        public static IEnumerable<Ejercicio> GetEjercicioNotUsuario(int usuarioId, int cursoId, int nivelEjercicio, int estadoEjercicio)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                IQueryable<Ejercicio> query = from u in db.Ejercicios.Include("Cursoes").Include("EstadoEjercicio").Include("Usuario")
                                              where (u.UsuarioId != usuarioId)
                                              && (cursoId == -1 || u.Cursoes.Count(m => m.CursoId == cursoId) > 0)
                                              && (estadoEjercicio == -1 || u.EstadoEjercicioId == estadoEjercicio)
                                              && (nivelEjercicio == -1 || u.NivelEjercicio == nivelEjercicio)
                                              select u;
                return query.ToList();
            }
        }

        #region IFiltrablePorSeguridadPorValor Members

        public static List<EntidadProgramARBase> Filtrar(List<EntidadProgramARBase> lista, Usuario user, TipoUsuario tipo)
        {
            throw new NotImplementedException();
        }

        #endregion

        public static void ModificarEstado(Ejercicio ejercicio)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Ejercicio ejercicioOrig = db.Ejercicios.Single(u => u.EjercicioId == ejercicio.EjercicioId);

                ejercicioOrig.EstadoEjercicioId = ejercicio.EstadoEjercicioId;
                ejercicioOrig.MensajeModeracion = ejercicio.MensajeModeracion;

                //MensajeModeracion mensajeModeracion = new MensajeModeracion();

                //mensajeModeracion.EjercicioId = ejercicio.MensajeModeracion.EjercicioId;
                //mensajeModeracion.UsuarioModeradorId = ejercicio.MensajeModeracion.UsuarioModeradorId;
                //mensajeModeracion.Mensaje = ejercicio.MensajeModeracion.Mensaje;

                //db.MensajeModeracions.AddObject(mensajeModeracion);

                db.ObjectStateManager.ChangeObjectState(ejercicioOrig, EntityState.Modified);
                db.SaveChanges();
            }
        }
    }
}
