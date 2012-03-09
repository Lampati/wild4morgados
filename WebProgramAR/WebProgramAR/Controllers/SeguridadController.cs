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
    public class SeguridadController : ControllerBase
    {
        //
        // GET: /Seguridad/
        [Authorize(Roles = "administrador")]
        public ActionResult Index(int page = 1, string sort = "Tabla", string sortDir = "ASC", int tablaId = -1, int? tipoUsuarioId = -1,
            int? usuarioId = -1, int comparadorId = -1, int columnaId = -1, bool? activa = null)
        {
            //Pasar la cantidad por pagina a una constante mas copada.
            int cantidadPorPaginaTPC = 10;

            var numReglas = SeguridadNegocio.ContarCantidad(tablaId, columnaId, comparadorId, usuarioId, tipoUsuarioId, activa);

            sortDir = sortDir.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? sortDir : "asc";

            var validColumns = new[] { "Tabla", "Columna", "Comparador", "Usuario","TipoUsuario" };

            if (!validColumns.Any(c => c.Equals(sort, StringComparison.CurrentCultureIgnoreCase)))
                sort = "Tabla";

            var reglas = SeguridadNegocio.ObtenerPagina(
                     page,
                     cantidadPorPaginaTPC,
                     sort + " " + sortDir,
                     tablaId, 
                     columnaId, 
                     comparadorId, 
                     usuarioId, 
                     tipoUsuarioId,
                     activa
            );

            var datos = new SeguridadGrillaModel()
            {
                Cantidad = numReglas,
                CantidadPorPagina = cantidadPorPaginaTPC,
                ReglasSeguridad = reglas,
                PaginaActual = page
            };

            
            
            return View(datos);

        }

        //
        // GET: /Seguridad/Details/5
        [Authorize(Roles = "administrador")]
        public ActionResult Details(int id)
        {
            ReglasSeguridad c = SeguridadNegocio.GetReglaSeguridadById(id);
            return View(c);
        }
        
        //
        // GET: /Seguridad/Create
        [Authorize(Roles = "administrador")]
        public ActionResult Create()
        {
            ArmarViewBags();
            return View();
        } 

        //
        // POST: /Seguridad/Create

        [HttpPost]
        [Authorize(Roles = "administrador")]
        public ActionResult Create(ReglasSeguridad regla)
        {
            if (ModelState.IsValid)
            {
                SeguridadNegocio.Alta(regla);
                return Content(Boolean.TrueString);
                
            }else
            {
                return View();
            }
        }
        
        //
        // GET: /Seguridad/Edit/5
        [Authorize(Roles = "administrador")]
        public ActionResult Edit(int id)
        {
            if (Request.IsAjaxRequest())
            {
                ReglasSeguridad c = SeguridadNegocio.GetReglaSeguridadById(id);
                ArmarViewBags();
                return View("Edit", c);
            }
            else
            {
                throw new Exception("No se puede acceder a esta pagina de ese modo. Por favor use la pagina para acceder");
            }
        }

        //
        // POST: /Seguridad/Edit/5
        [HttpPost]
        [Authorize(Roles = "administrador")]
        public ActionResult Edit(ReglasSeguridad regla)
        {
            // TODO: Add update logic here
            if (ModelState.IsValid)
            {
                SeguridadNegocio.Modificar(regla);
                return Content(Boolean.TrueString);
            }
            else
            {
                return View();
            }
            
        }


      

     

        //
        // GET: /Seguridad/Delete/5
        [Authorize(Roles = "administrador")] 
        public ActionResult Delete(int id)
        {
            if (Request.IsAjaxRequest())
            {
                ReglasSeguridad c = SeguridadNegocio.GetReglaSeguridadById(id);
                return View("Delete", c);
            }
            else
            {
                throw new Exception("No se puede acceder a esta pagina de ese modo. Por favor use la pagina para acceder");
            }
        }

        //
        // POST: /Seguridad/Delete/5

        [HttpPost]
        [Authorize(Roles = "administrador")]
        public ActionResult Delete(ReglasSeguridad regla)
        {
            SeguridadNegocio.Eliminar(regla.ReglaId);
            return Content(Boolean.TrueString);
            
        }


        /// <summary>
        /// Cargar Columnas de acuerdo a la tabla.
        /// </summary>
        [HttpPost]
        public JsonResult GetColumnasByTabla(int tablaId)
        {
            List<Columna> listaCols = Negocio.SeguridadNegocio.GetColumnasByTabla(tablaId).ToList();

            List<GenericJsonModel> listaRetorno = new List<GenericJsonModel>();
            foreach (Columna item in listaCols)
            {
                listaRetorno.Add(new GenericJsonModel() { Id = item.ColumnaId.ToString(), Value = item.Nombre });
            }

            return Json(listaRetorno, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Cargar Comparadores de acuerdo a la columna
        /// .
        /// </summary>
        [HttpPost]
        public JsonResult GetComparadoresByColumna(int colId)
        {
            List<Comparador> listaCols = SeguridadNegocio.GetComparadorByColumna(colId).ToList();

            List<GenericJsonModel> listaRetorno = new List<GenericJsonModel>();
            foreach (Comparador item in listaCols)
            {
                listaRetorno.Add(new GenericJsonModel() { Id = item.ComparadorId.ToString(), Value = item.Nombre });
            }

            return Json(listaRetorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetUsuariosByNombreUsuario(string user)
        {
            List<Usuario> listaCols = UsuarioNegocio.GetUsuarioByLoginUsuarioAutocomplete(user).ToList();

            List<GenericJsonModel> listaRetorno = new List<GenericJsonModel>();
            foreach (Usuario item in listaCols)
            {
                listaRetorno.Add(new GenericJsonModel() { Id = item.UsuarioId.ToString(),
                    Value = string.Format("{0} ({1}, {2})",item.UsuarioNombre,item.Apellido,item.Nombre) });
            }

            return Json(listaRetorno, JsonRequestBehavior.AllowGet);
        }


        private void ArmarViewBags()
        {
            ViewBag.ListaTablas = SeguridadNegocio.GetTablas();
            ViewBag.Columnas =null;
            ViewBag.Comparadores = null;
            ViewBag.ListaTipoUsuarios = TipoUsuarioNegocio.GetTiposUsuario();
        }


    }
}
