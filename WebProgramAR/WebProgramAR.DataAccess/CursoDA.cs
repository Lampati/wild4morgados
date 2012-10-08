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
                Curso c = db.Cursoes.Include("Usuario").Include("Ejercicios").Single(u => u.CursoId  == id);

                Curso copia = c.Clone() as Curso;
                

                return copia;
            }
        }
        
        public static void Alta(Curso Curso)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                db.Cursoes.AddObject(Curso);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Curso"></param>
        /// <param name="idEjerciciosAgregar">Ids de los ejercicios a agregar</param>
        /// /// <param name="agregar">True indica agregar / False indica quitar los ejercicios</param>
        public static void Modificar(Curso Curso, int[] idEjerciciosAgregar, bool agregar)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Modificar(Curso, idEjerciciosAgregar, agregar, db);
            }
        }

        private static void Modificar(Curso cursoModif, int[] idEjerciciosAgregar,  bool agregar,WebProgramAREntities db)
        {
            Curso cursoOrig = db.Cursoes.Single(u => u.CursoId == cursoModif.CursoId);

            cursoOrig.Nombre = cursoModif.Nombre;

            if (idEjerciciosAgregar.Length > 0)
            {
                for (int i = 0; i < idEjerciciosAgregar.Length; i++)
			    {          
                    int ejId = idEjerciciosAgregar[i];

                    Ejercicio ejModif = db.Ejercicios.Single(x => x.EjercicioId == ejId);

                    if (agregar)
                    {
                        cursoOrig.Ejercicios.Add(ejModif);
                    }
                    else
                    {
                        cursoOrig.Ejercicios.Remove(ejModif);
                    }
			    }
                //AsignarEjerciciosQueCorrespondan(cursoOrig, cursoModif,db);
            }

           

            db.ObjectStateManager.ChangeObjectState(cursoOrig, EntityState.Modified);

            db.DetectChanges();
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
                Curso curso = db.Cursoes.Include("Ejercicios").Single(u => u.CursoId == id);
                //Curso.Status = false; //baja lógica

                

                db.Cursoes.DeleteObject(curso);
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
            IQueryable<Curso> query = from u in db.Cursoes
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
                IQueryable<Curso> cursos = from u in db.Cursoes where u.UsuarioId == idUsuario select u;
                //Curso.Status = false; //baja lógica

                foreach (var item in cursos.ToList())
	            {
                    db.Cursoes.DeleteObject(item);		 
	            } 

                
                db.SaveChanges();
            }
        }

        public static bool ExisteCursoById(int id)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                IQueryable<Curso> query = from u in db.Cursoes
                                          where u.CursoId == id
                                          select u;

                return query.ToList().Count == 1;
            }
        }

        public static bool ExisteCursoByNombre(string nombre)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                IQueryable<Curso> query = from u in db.Cursoes
                                          where u.Nombre == nombre
                                          select u;

                return query.ToList().Count == 1;
            }
        }
    }
}
