using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;
using WebProgramAR.DataAccess.Interfases;

namespace WebProgramAR.DataAccess
{
    public class EjercicioDA : IFiltrablePorSeguridadPorValor
    {
        public static string _nombreTabla = "Ejercicio";

        public static Ejercicio GetEjercicioById(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.Ejercicios.Include("EstadoEjercicio").Include("Usuario").Include("NivelEjercicio").Single(u => u.EjercicioId  == id);
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

   
        private static void Modificar(Ejercicio Ejercicio, WebProgramAREntities db)
        {
            Ejercicio EjercicioOrig = db.Ejercicios.Single(u => u.EjercicioId == Ejercicio.EjercicioId);

            db.ObjectStateManager.ChangeObjectState(EjercicioOrig, EntityState.Modified);
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
                if (sortColumns.Contains("NivelEjercicio"))
                {
                    sortColumns = sortColumns.Replace("NivelEjercicio", "NivelEjercicio.NivelEjercicioId");
                }



                return query.OrderUsingSortExpression(sortColumns)
                            .Skip((paginaActual - 1) * personasPorPagina)
                            .Take(personasPorPagina)
                            .ToList();
            }
        }

        private static IQueryable<Ejercicio> GetEjercicios(string nombre, int usuarioId, int cursoId, int estadoEjercicio, int nivelEjercicio, bool global, WebProgramAREntities db)
        {
            IQueryable<Ejercicio> query = from u in db.Ejercicios.Include("Cursoes").Include("EstadoEjercicio").Include("NivelEjercicio").Include("Usuario")
                                     where u.Global == global
                                     && (usuarioId == -1 || u.UsuarioId == usuarioId)                                     
                                     && (cursoId == -1 || u.Cursoes.Count(m => m.CursoId == cursoId) > 0)
                                     && (estadoEjercicio == -1 || u.EstadoEjercicioId == estadoEjercicio)
                                     && (nivelEjercicio == -1 || u.NivelEjercicioId == nivelEjercicio)
                                     && (nombre == "" || u.Nombre.Contains(nombre))
                                     select u;
            return query;
        }



        #region IFiltrablePorSeguridadPorValor Members

        public List<EntidadProgramARBase> Filtrar(List<EntidadProgramARBase> lista, Usuario user, TipoUsuario tipo)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
