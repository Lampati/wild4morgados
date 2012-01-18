using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProgramAR.Entidades;
using WebProgramAR.Negocio;
using WebProgramAR.Sitio.Models;

namespace WebProgramAR.Controllers
{
    public class CursoController : Controller
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
            return View();
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
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    curso.FechaAlta = DateTime.Now;
                    //curso.UsuarioId = usuarioLogueado;

                    CursoNegocio.Alta(curso);

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Curso/Edit/5
 
        public ActionResult Edit(int id)
        {
            Curso c = CursoNegocio.GetCursoById(id);
            return View("Edit",c);
        }

        //
        // POST: /Curso/Edit/5

        [HttpPost]
        public ActionResult Edit(Curso curso)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    CursoNegocio.Modificar(curso);
                }
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Curso/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Curso/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
