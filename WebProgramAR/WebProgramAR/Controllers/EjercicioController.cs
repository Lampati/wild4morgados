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
    public class EjercicioController : Controller
    {
        //
        // GET: /Ejercicio/

        public ActionResult Index(int page = 1, string sort = "Nombre", string sortDir = "ASC",
             int usuarioId = -1, int cursoId = -1, string nombre = "", 
             int estadoEjercicio = -1, int nivelEjercicio = -1, bool global = false)
        {
            //Pasar la cantidad por pagina a una constante mas copada.
            int cantidadPorPaginaTPC = 10;

            var numUsuarios = EjercicioNegocio.ContarCantidad(nombre,
                     usuarioId,
                     cursoId,
                     estadoEjercicio,
                     nivelEjercicio,
                     global);

            sortDir = sortDir.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? sortDir : "asc";

            var validColumns = new[] { "Nombre", "Usuario", "Curso", "EstadoEjercicio", "NivelEjercicio", "Global" };

            if (!validColumns.Any(c => c.Equals(sort, StringComparison.CurrentCultureIgnoreCase)))
                sort = "Nombre";

            var ejers = EjercicioNegocio.ObtenerPagina(
                     page,
                     cantidadPorPaginaTPC,
                     sort + " " + sortDir,                     
                     nombre, 
                     usuarioId,
                     cursoId,
                     estadoEjercicio,
                     nivelEjercicio,
                     global
            );

            var datos = new EjercicioGrillaModel()
            {
                Cantidad = numUsuarios,
                CantidadPorPagina = cantidadPorPaginaTPC,
                Ejercicios = ejers,
                PaginaActual = page
            };

            ViewBag.NivelesEjercicio = new SelectList(Negocio.NivelEjercicioNegocio.GetNiveles(), "NivelEjercicioId", "Descripcion"); 

            return View(datos);

        }

        //
        // GET: /Ejercicio/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Ejercicio/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Ejercicio/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Ejercicio/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Ejercicio/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Ejercicio/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Ejercicio/Delete/5

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
