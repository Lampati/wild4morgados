using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProgramAR.Entidades;
using WebProgramAR.Negocio;
using WebProgramAR.Sitio.Models;
using WebProgramAR.Models;

namespace WebProgramAR.Controllers
{
    public class CursoController : ControllerBase
    {
        //
        // GET: /Curso/

        public ActionResult Index(int page = 1, string sort = "Nombre", string sortDir = "ASC",  int cursoId = -1, string nombre = "")
        {
            //Pasar la cantidad por pagina a una constante mas copada.
            int cantidadPorPaginaTPC = 10;
            
            var numUsuarios = CursoNegocio.ContarCantidad(cursoId, nombre);

            sortDir = sortDir.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? sortDir : "asc";

            var validColumns = new[] { "Nombre"};

            if (!validColumns.Any(c => c.Equals(sort, StringComparison.CurrentCultureIgnoreCase)))
                sort = "Nombre";

            var cursos = CursoNegocio.ObtenerPagina(
                     page,
                     cantidadPorPaginaTPC,
                     sort + " " + sortDir,
                     cursoId, 
                     nombre
            );

            var datos = new CursoGrillaModel()
            {
                Cantidad = numUsuarios,
                CantidadPorPagina = cantidadPorPaginaTPC,
                Cursos = cursos,
                PaginaActual = page
            };

            
            return View(datos);

        }

        //
        // GET: /Curso/Details/5

        public ActionResult Details(int id)
        {
            Curso c = CursoNegocio.GetCursoById(id);
            return View(c);
        }
        
        //
        // GET: /Curso/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Curso/Create

        [HttpPost]
        public ActionResult Create(Curso curso)
        {
            if (ModelState.IsValid)
            {

                //flanzani
                //Una vez que tengamos el usuarioId en sesion, lo ponemos aca. Mientras tanto, usamos 1.
                curso.UsuarioId = 1;
                //curso.UsuarioId = usuarioLogueado;

                CursoNegocio.Alta(curso);
                return Content(Boolean.TrueString);
                
            }else
            {
                return View();
            }
        }
        
        //
        // GET: /Curso/Edit/5
 
        public ActionResult Edit(int id)
        {
            if (Request.IsAjaxRequest())
            {
                Curso c = CursoNegocio.GetCursoById(id);
                return View("Edit", c);
            }
            else
            {
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
                    CursoNegocio.Modificar(curso);
                    return Content(Boolean.TrueString);
                }
                else
                {
                    return View();
                }
            
        }

        //
        // GET: /Curso/Delete/5
 
        public ActionResult Delete(int id)
        {
            if (Request.IsAjaxRequest())
            {
                Curso c = CursoNegocio.GetCursoById(id);
                return View("Delete", c);
            }
            else
            {
                throw new Exception("No se puede acceder a esta pagina de ese modo. Por favor use la pagina para acceder");
            }
        }

        //
        // POST: /Curso/Delete/5

        [HttpPost]
        public ActionResult Delete(Curso curso)
        {
            CursoNegocio.Eliminar(curso.CursoId);
            return Content(Boolean.TrueString);
            
        }

        
        /// <summary>
        /// Cargar Provincias de acuerdo al pais.
        /// </summary>
        [HttpPost]
        public JsonResult GetEjerciciosNotByUser(int usuarioId, int cursoId, int nivelEjercicio, int estadoEjercicio)
        {
            List<Ejercicio> listaEjercicios = Negocio.EjercicioNegocio.GetEjercicioNotUsuario(usuarioId, cursoId, nivelEjercicio, estadoEjercicio).ToList();

            List<GenericJsonModel> listaRetorno = new List<GenericJsonModel>();
            foreach (Ejercicio item in listaEjercicios)
            {
                listaRetorno.Add(new GenericJsonModel() { Id = item.EjercicioId.ToString(), Value = item.Nombre });
            }

            return Json(listaRetorno, JsonRequestBehavior.AllowGet);
        }

        
        
    }
}
