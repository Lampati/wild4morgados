using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProgramAR.Entidades;
using WebProgramAR.Negocio;
using WebProgramAR.Sitio.Models;
using WebProgramAR.Models;
using System.Web.Security;

namespace WebProgramAR.Controllers
{
    public class CursoController : ControllerBase
    {
        //
        // GET: /Curso/

        public ActionResult Index(int page = 1, string sort = "Nombre", string sortDir = "ASC", int cursoId = -1, string nombre = "",
            bool conLayout = true, bool aplicarPermisos = true, int usuarioId = -1)
        {
            //Pasar la cantidad por pagina a una constante mas copada.
            int cantidadPorPaginaTPC = 10;

            Usuario usuarioLogueado = GetUsuarioLogueado();

            var numUsuarios = CursoNegocio.ContarCantidad(cursoId, nombre, usuarioId, usuarioLogueado);

            sortDir = sortDir.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? sortDir : "asc";

            var validColumns = new[] { "Nombre"};

            if (!validColumns.Any(c => c.Equals(sort, StringComparison.CurrentCultureIgnoreCase)))
                sort = "Nombre";

            var cursos = CursoNegocio.ObtenerPagina(
                     page,
                     cantidadPorPaginaTPC,
                     sort + " " + sortDir,
                     cursoId, 
                     nombre,
                     usuarioId,
                     usuarioLogueado
            );

            var datos = new CursoGrillaModel()
            {
                Cantidad = numUsuarios,
                CantidadPorPagina = cantidadPorPaginaTPC,
                Cursos = cursos,
                PaginaActual = page
            };
            datos.ConLayout = conLayout;
            datos.AplicarPermisos = aplicarPermisos;
            ViewBag.usuarioLogueado = GetUsuarioLogueado();
            return View(datos);

        }

        //
        // GET: /Curso/Details/5

        public ActionResult Details(int id)
        {
            if (Request.IsAjaxRequest())
            {
                if (CursoNegocio.ExisteCursoById(id))
                {
                    Curso c = CursoNegocio.GetCursoById(id);
                    return View(c);
                }
                else
                {
                    throw new Exception("El curso seleccionado no existe mas.");
                }
            }
            else
            {
                return View("Error");
                throw new Exception("No se puede acceder a esta pagina de ese modo. Por favor use la pagina para acceder");
            }
        }
        
        //
        // GET: /Curso/Create
        [Authorize]
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
            {
                return View();
            }
            else
            {
                return View("Error");
                throw new Exception("No se puede acceder a esta pagina de ese modo. Por favor use la pagina para acceder");
            }
        } 

        //
        // POST: /Curso/Create

        [HttpPost]
        public ActionResult Create(Curso curso)
        {
            if (ModelState.IsValid)
            {

                if (CursoNegocio.ExisteCursoByNombre(curso.Nombre))
                {
                    return Content("Ya existe un curso con ese nombre.Modifica el nombre que has elegido e intenta nuevamente");
                }
                else
                {
                    //flanzani
                    //Una vez que tengamos el usuarioId en sesion, lo ponemos aca. Mientras tanto, usamos 1.
                    Usuario userBd = UsuarioNegocio.GetUsuarioByLoginUsuario(SimpleSessionPersister.UserName);

                    curso.UsuarioId = userBd.UsuarioId;
                    //curso.UsuarioId = usuarioLogueado;

                    CursoNegocio.Alta(curso);
                    return Content(Boolean.TrueString);
                }
            }else
            {
                return View();
            }
        }
        
        //
        // GET: /Curso/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            if (Request.IsAjaxRequest())
            {
                if (CursoNegocio.ExisteCursoById(id))
                {
                    Curso c = CursoNegocio.GetCursoById(id);
                    return View("Edit", c);
                }
                else
                {
                    throw new Exception("El curso seleccionado no existe mas.");
                }
            }
            else
            {
                return View("Error");
                throw new Exception("No se puede acceder a esta pagina de ese modo. Por favor use la pagina para acceder");
            }
        }

        //
        // POST: /Curso/Edit/5
        [HttpPost]
        public ActionResult Edit(Curso curso)
        {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    if (CursoNegocio.ExisteCursoById(curso.CursoId))
                    {
                            CursoNegocio.Modificar(curso);
                            return Content(Boolean.TrueString);
                    }
                    else
                    {
                        if (CursoNegocio.ExisteCursoByNombre(curso.Nombre))
                        {
                            return Content("Ya existe un curso con ese nombre.Modifica el nombre que has elegido e intenta nuevamente");
                        }
                        else
                        {
                            throw new Exception("El curso sobre el cual se estaba trabajando fue borrado.");
                        }
                    }
                }
                else
                {
                    return View();
                }
            
        }


        //
        // GET: /Curso/AsociarEjercicios/5
        [Authorize]
        public ActionResult AsociarEjercicios(int id)
        {
            if (Request.IsAjaxRequest())
            {

                if (CursoNegocio.ExisteCursoById(id))
                {
                    Curso c = CursoNegocio.GetCursoById(id);

                    CursoAsociarModel model = new CursoAsociarModel();
                    model.Curso = c;

                    List<int> listaEjerciciosId = new List<int>();

                    foreach (Ejercicio item in c.Ejercicios)
                    {
                        listaEjerciciosId.Add(item.EjercicioId);
                    }

                    model.Curso.Ejercicios.Clear();
                    model.EjerciciosId = listaEjerciciosId.ToArray();

                    return View("AsociarEjercicios", model);
                }
                else
                {
                    throw new Exception("El curso seleccionado no existe mas.");
                }
            }
            else
            {
                return View("Error");
                throw new Exception("No se puede acceder a esta pagina de ese modo. Por favor use la pagina para acceder");
            }
        }

        //
        //// POST: /Curso/Edit/5
        //[HttpPost]
        //public ActionResult AsociarEjercicios(CursoAsociarModel model)
        //{
        //    // TODO: Add update logic here
        //    if (ModelState.IsValid)
        //    {
        //        AgregarAsociacionesEjercicioACurso(model.Curso, model.EjerciciosId);

        //        CursoNegocio.Modificar(model.Curso, true);
                
        //        return Content(Boolean.TrueString);
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}



        [HttpPost]
        public ActionResult AsociarEjercicioACurso(int cursoId, int ejercicioId)
        {
            // TODO: Add update logic here
            if (ModelState.IsValid)
            {
                if (CursoNegocio.ExisteCursoById(cursoId))
                {
                    if (EjercicioNegocio.ExisteEjercicioById(ejercicioId))
                    {
                        Curso curso = CursoNegocio.GetCursoById(cursoId);

                        CursoNegocio.Modificar(curso, new int[] { ejercicioId }, true);

                        return Json(new { resultado = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new Exception("El ejercicio que se intento asociar fue borrado.");
                    }
                }
                else
	            {
                    throw new Exception("El curso sobre el cual se estaba trabajando fue borrado.");
	            }
            }
            else
            {
                return Json(new { resultado = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DesasociarEjercicioACurso(int cursoId, int ejercicioId)
        {
            // TODO: Add update logic here
            if (ModelState.IsValid)
            {
                if (CursoNegocio.ExisteCursoById(cursoId))
                {
                    if (EjercicioNegocio.ExisteEjercicioById(ejercicioId))
                    {
                        Curso curso = CursoNegocio.GetCursoById(cursoId);

                        CursoNegocio.Modificar(curso, new int[] { ejercicioId }, false);

                        return Json(new { resultado = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new Exception("El ejercicio que se intento desasociar fue borrado.");
                    }
                }
                else
                {
                    throw new Exception("El curso sobre el cual se estaba trabajando fue borrado.");
                }
            }
            else
            {
                return Json(new { resultado = false }, JsonRequestBehavior.AllowGet);
            }
        }


        //
        // GET: /Curso/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            if (Request.IsAjaxRequest())
            {
                if (CursoNegocio.ExisteCursoById(id))
                {
                    Curso c = CursoNegocio.GetCursoById(id);
                    return View("Delete", c);
                }
                else
                {
                    throw new Exception("El curso seleccionado no existe mas.");
                }
            }
            else
            {
                return View("Error");
                throw new Exception("No se puede acceder a esta pagina de ese modo. Por favor use la pagina para acceder");
            }
        }

        //
        // POST: /Curso/Delete/5

        [HttpPost]
        public ActionResult Delete(Curso curso)
        {
            if (CursoNegocio.ExisteCursoById(curso.CursoId))
            {
                CursoNegocio.Eliminar(curso.CursoId);
                return Content(Boolean.TrueString);
            }
            else
            {
                throw new Exception("Se intento borrar un curso que no existía mas.");
            }
            
        }

        
        /// <summary>
        /// Cargar Provincias de acuerdo al pais.
        /// </summary>
        [HttpPost]
        public JsonResult GetEjerciciosNotByUser(int usuarioId, int cursoId, int nivelEjercicio, int estadoEjercicio)
        {
            Usuario userLogueado = GetUsuarioLogueado();

            List<Ejercicio> listaEjercicios = Negocio.EjercicioNegocio.GetEjercicioNotUsuario(usuarioId, cursoId, nivelEjercicio, estadoEjercicio,userLogueado).ToList();

            List<GenericJsonModel> listaRetorno = new List<GenericJsonModel>();
            foreach (Ejercicio item in listaEjercicios)
            {
                listaRetorno.Add(new GenericJsonModel() { Id = item.EjercicioId.ToString(), Value = item.Nombre });
            }

            return Json(listaRetorno, JsonRequestBehavior.AllowGet);
        }

        
        
    }
}
