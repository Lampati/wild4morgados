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
        public ActionResult Index(int page = 1, string sort = "Tabla", string sortDir = "ASC", int tablaId = -1, int tipoUsuarioId = -1,
            string usuario = null, int comparadorId = -1, int columnaId = -1, int activa = -1)
        {
            //Pasar la cantidad por pagina a una constante mas copada.
            int cantidadPorPaginaTPC = 10;

            Usuario usuarioLogueado = GetUsuarioLogueado();

            bool? habilitado = null;

            if (activa == 0)
            {
                habilitado = false;
            } 
            else if (activa == 1)
            {
                habilitado = true;
            }

            var numReglas = SeguridadNegocio.ContarCantidad(tablaId, columnaId, comparadorId, usuario, tipoUsuarioId, habilitado);

            sortDir = sortDir.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? sortDir : "asc";

            var validColumns = new[] { "Tabla.Nombre", "Columna.Nombre", "Comparador.Nombre", "Usuario","TipoUsuario", "Valor","Activa" };

            if (!validColumns.Any(c => c.Equals(sort, StringComparison.CurrentCultureIgnoreCase)))
                sort = "Tabla";

            var reglas = SeguridadNegocio.ObtenerPagina(
                     page,
                     cantidadPorPaginaTPC,
                     sort + " " + sortDir,
                     tablaId, 
                     columnaId, 
                     comparadorId, 
                     usuario, 
                     tipoUsuarioId,
                     habilitado
            );

            var datos = new SeguridadGrillaModel()
            {
                Cantidad = numReglas,
                CantidadPorPagina = cantidadPorPaginaTPC,
                ReglasSeguridad = reglas,
                PaginaActual = page
            };

            List<Tabla> listaTabla = new List<Tabla>();
            //listaTabla.Add(new Tabla() { TablaId = -1, Nombre = "Todos" });
            listaTabla.AddRange(SeguridadNegocio.GetTablas());
            ViewBag.ListaTablas = listaTabla;     
            List<TipoUsuario> listaTipo = new List<TipoUsuario>();
            //listaTipo.Add(new TipoUsuario() { TipoUsuarioId = -1, Descripcion = "Todos" });
            listaTipo.AddRange(TipoUsuarioNegocio.GetTiposUsuario());
            ViewBag.ListaTipoUsuarios = listaTipo;
            ViewBag.listaHabilitado = new []{new SelectListItem{Value="0",Text="No"},new SelectListItem{Value="1",Text="Si"}};
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
            if (Request.IsAjaxRequest())
            {
                ArmarViewBags();
                return View();
            }
            else
            {
                throw new Exception("No se puede acceder a esta pagina de ese modo. Por favor use la pagina para acceder");
            }
        } 

        //
        // POST: /Seguridad/Create

        [HttpPost]
        [Authorize(Roles = "administrador")]
        public ActionResult Create(ReglasSeguridad regla)
        {
            bool error = false;
            string errorMensaje = "";
            if (ModelState.IsValid)
            {
                if (regla.TablaId== null || regla.TablaId.Equals("-1"))
                {
                    error = true;
                    errorMensaje = "Debe seleccionar una tabla";
                }
                if (!error && (regla.ColumnaId == null || regla.ColumnaId.Equals("-1")))
                {
                    error = true;
                    errorMensaje = "Debe seleccionar una columna";
                }
                if (!error && (regla.ComparadorId == null || regla.ComparadorId.Equals("-1")))
                {
                    error = true;
                    errorMensaje = "Debe seleccionar un comparador";
                }
                if (regla.TipoUsuarioId == -1) regla.TipoUsuarioId = null;
                if (!error)
                {
                    try
                    {
                        SeguridadNegocio.Alta(regla);
                    }
                    catch (Exception)
                    {
                        return Content(errorMensaje);
                    }
                }
                else
                {
                    return Content(errorMensaje);
                }

                return Content(Boolean.TrueString);
            }else
            {
                return Content("Ha ocurrido un error. Verifique los datos ingresados por favor");
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
                ArmarViewBags(id);
                ReglasSeguridad modelo = new ReglasSeguridad();
                modelo.ReglaId = c.ReglaId;
                modelo.Activa = c.Activa;
                modelo.ColumnaId = c.ColumnaId;
                modelo.ComparadorId = c.ComparadorId;
                modelo.TablaId = c.TablaId;
                modelo.TipoUsuarioId = c.TipoUsuarioId.HasValue ? c.TipoUsuarioId.Value : -1;
                if (c.UsuarioId.HasValue) modelo.UsuarioId = c.UsuarioId.Value;
                //ViewBag.usuarioDescripcion = c.UsuarioId.HasValue ? c.Usuario.UsuarioNombre : "";
                ViewBag.usuarioDescripcion = c.UsuarioId.HasValue ? c.Usuario.UsuarioNombre.ToString() : "";
                modelo.Valor = c.Valor;

                //modelo.Tipo = c.Columna.Tipo.Nombre.ToUpper();

                return View("Edit", modelo);
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
            bool error = false;
            string errorMensaje = "";
            if (ModelState.IsValid)
            {
                if (regla.TablaId == null || regla.TablaId.Equals("-1"))
                {
                    error = true;
                    errorMensaje = "Debe seleccionar una tabla";
                }
                if (!error && (regla.ColumnaId == null || regla.ColumnaId.Equals("-1")))
                {
                    error = true;
                    errorMensaje = "Debe seleccionar una columna";
                }
                if (!error && (regla.ComparadorId == null || regla.ComparadorId.Equals("-1")))
                {
                    error = true;
                    errorMensaje = "Debe seleccionar un comparador";
                }
                if (regla.TipoUsuarioId == -1) regla.TipoUsuarioId = null;
                if (!error)
                {
                    try
                    {
                        SeguridadNegocio.Modificar(regla);
                    }
                    catch (Exception)
                    {
                        return Content(errorMensaje);
                    }
                }
                else
                {
                    return Content(errorMensaje);
                }

                return Content(Boolean.TrueString);
            }
            else
            {
                return Content("Ha ocurrido un error. Verifique los datos ingresados por favor");
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

            List<ColumnaJsonModel> listaRetorno = new List<ColumnaJsonModel>();
            foreach (Columna item in listaCols)
            {
                listaRetorno.Add(new ColumnaJsonModel() { Id = item.ColumnaId.ToString(), Value = item.Nombre, TipoId = item.TipoId.ToString(), TipoDesc = item.Tipo.Nombre.ToUpper() });
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
            Usuario usuarioLogueado = GetUsuarioLogueado();

            List<Usuario> listaCols = UsuarioNegocio.GetUsuarioByLoginUsuarioAutocomplete(user,usuarioLogueado).ToList();

            List<GenericJsonModel> listaRetorno = new List<GenericJsonModel>();
            foreach (Usuario item in listaCols)
            {
                listaRetorno.Add(new GenericJsonModel() { Id = item.UsuarioId.ToString(),
                    Value = string.Format("{0} ({1}, {2})",item.UsuarioNombre,item.Apellido,item.Nombre) });
            }

            return Json(listaRetorno, JsonRequestBehavior.AllowGet);
        }


        private void ArmarViewBags(int id=-1)
        {
            Usuario usuarioLogueado = GetUsuarioLogueado();
            if (id != -1)
            {
                ViewBag.Columnas = SeguridadNegocio.GetColumnasByTabla(SeguridadNegocio.GetReglaSeguridadById(id).TablaId);
                ViewBag.Comparadores = SeguridadNegocio.GetComparadorByColumna(SeguridadNegocio.GetReglaSeguridadById(id).ColumnaId);
            }
            else
            {
                ViewBag.Columnas = new List<Columna>();
                ViewBag.Comparadores = new List<Comparador>();
            }
            ViewBag.ListaTablas = SeguridadNegocio.GetTablas();
            List<TipoUsuario> listaTipo = new List<TipoUsuario>();
            listaTipo.Add(new TipoUsuario() { TipoUsuarioId = -1, Descripcion = "Todos" });
            listaTipo.AddRange(TipoUsuarioNegocio.GetTiposUsuario());
            ViewBag.ListaTipoUsuarios = listaTipo;
        }


    }
}
