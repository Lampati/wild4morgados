using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;

namespace WebProgramAR.DataAccess
{
    public class CursoDA 
    {
        public static string _nombreTabla = "Curso";

        public static Curso GetCursoById(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.Cursos.Include("Usuario").Include("Ejercicios").Single(u => u.CursoId  == id);
            }
        }
        
        public static void Alta(Curso Curso)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                db.Cursos.AddObject(Curso);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Curso"></param>
        /// <param name="asignarEjercicios">Indica si tengo que reemplazar los ejercicios del curso</param>
        public static void Modificar(Curso Curso, bool asignarEjercicios)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Modificar(Curso, asignarEjercicios, db);
            }
        }

        private static void Modificar(Curso cursoModif, bool asignarEjercicios, WebProgramAREntities db)
        {
            Curso cursoOrig = db.Cursos.Single(u => u.CursoId == cursoModif.CursoId);

            cursoOrig.Nombre = cursoModif.Nombre;

            if (asignarEjercicios)
            {
                AsignarEjerciciosQueCorrespondan(cursoOrig, cursoModif,db);
            }

           

            db.ObjectStateManager.ChangeObjectState(cursoOrig, EntityState.Modified);
            db.SaveChanges();
        }

        private static void AsignarEjerciciosQueCorrespondan(Curso cursoOrig, Curso cursoModif, WebProgramAREntities db)
        {
            cursoOrig.Ejercicios.Clear();

            foreach (var ejer in cursoModif.Ejercicios.ToList())
            {
                if (!cursoOrig.Ejercicios.ToList().Exists(o => o.EjercicioId == ejer.EjercicioId))
                {
                    cursoOrig.Ejercicios.Add(ejer);
                }
            }       
        }

        public static void Eliminar(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Curso Curso = db.Cursos.Single(u => u.CursoId == id);
                //Curso.Status = false; //baja lógica

                db.Cursos.DeleteObject(Curso);
                db.SaveChanges();
            }
        }

        //static WebProgramAREntities db = new WebProgramAREntities();
        public static int ContarCantidad(int idCurso, string apellido, int usuarioId, Usuario userLogueado)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                List<Curso> aux = GetCursos(idCurso, apellido, usuarioId, db).ToList();

                float tiempo;

                return Seguridad.SeguridadXValorManager.Filtrar<Curso>(aux, _nombreTabla, userLogueado, out tiempo).Count();
            }
        }

        public static IEnumerable<Curso> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, int idCurso, string nombre, int usuarioId, Usuario userLogueado)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                if (paginaActual < 1) paginaActual = 1;


                IQueryable<Curso> query = GetCursos(idCurso, nombre, usuarioId, db);


                List<Curso> aux = query.OrderUsingSortExpression(sortColumns)
                                    .Skip((paginaActual - 1) * personasPorPagina)
                                    .Take(personasPorPagina)
                                    .ToList();

                float tiempo;

                return Seguridad.SeguridadXValorManager.Filtrar<Curso>(aux, _nombreTabla, userLogueado, out tiempo);
            }
        }

        private static IQueryable<Curso> GetCursos(int idCurso, string nom, int usuarioId , WebProgramAREntities db)
        {
            IQueryable<Curso> query = from u in db.Cursos
                                      where (idCurso == -1 || u.CursoId == idCurso)
                                      && (usuarioId == -1 || u.UsuarioId == usuarioId)
                                      && ( nom.Equals(string.Empty) ||u.Nombre.ToUpper().Contains(nom.ToUpper()))
                                     select u;
            return query;
        }

        
        #region IFiltrarPorSeguridadXValor Members

        public static List<EntidadProgramARBase> Filtrar(List<EntidadProgramARBase> lista, Usuario user, TipoUsuario tipo)
        {
            throw new NotImplementedException();
        }

        #endregion



        public static void EliminarCursosDeUsuario(int idUsuario)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                IQueryable<Curso> cursos = from u in db.Cursos where u.UsuarioId == idUsuario select u;
                //Curso.Status = false; //baja lógica

                foreach (var item in cursos.ToList())
	            {
                    db.Cursos.DeleteObject(item);		 
	            } 

                
                db.SaveChanges();
            }
        }
    }
}
