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
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/

        public ActionResult Index(int page = 1, string sort = "Nombre", string sortDir = "ASC",
              int tipoUsuarioId = -1,  string nombre = "", string apellido = "", string usuarioNombre = "",
              string pais = "", string provincia = "", string localidad = "")
        {
            //Pasar la cantidad por pagina a una constante mas copada.
            int cantidadPorPaginaTPC = 10;

            var numUsuarios = UsuarioNegocio.ContarCantidad(nombre,
                     apellido,
                     usuarioNombre,
                     tipoUsuarioId,
                     pais,
                     provincia,
                     localidad);

            sortDir = sortDir.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? sortDir : "asc";

            var validColumns = new[] { "Nombre", "Apellido", "NombreUsuario", "TipoUsuario", "Pais", "Provincia" , "Localidad"};

            if (!validColumns.Any(c => c.Equals(sort, StringComparison.CurrentCultureIgnoreCase)))
                sort = "Nombre";

            var ejers = UsuarioNegocio.ObtenerPagina(
                     page,
                     cantidadPorPaginaTPC,
                     sort + " " + sortDir,
                     nombre,
                     apellido,
                     usuarioNombre,
                     tipoUsuarioId,
                     pais,
                     provincia,
                     localidad
            );

            var datos = new UsuarioGrillaModel()
            {
                Cantidad = numUsuarios,
                CantidadPorPagina = cantidadPorPaginaTPC,
                Usuarios = ejers,
                PaginaActual = page
            };

            ViewBag.TipoUsuarios = new SelectList(Negocio.TipoUsuarioNegocio.GetTiposUsuario(), "TipoUsuarioId", "Descripcion"); 
            return View(datos);

        }

        //
        // GET: /Usuario/Details/5

        public ActionResult Details(int id)
        {
            Usuario u = UsuarioNegocio.GetUsuarioById(id);
            return View(u);
        }

        //
        // GET: /Usuario/Create

        public ActionResult Create()
        {
            Initilization();
            return View();
        }
        /// <summary>
        /// Cargar Paises. Las provincias y localidades se cargan por ajax, deben traer resultados de acuerdo a la eleccion
        /// </summary>
        public void Initilization()
        {
            List<Pais> listaPaises = new List<Pais>();
            List<Provincia> listaProvincias = new List<Provincia>();
            List<Localidad> listaLocalidades = new List<Localidad>();
            List<TipoUsuario> listaTipoUsuarios = new List<TipoUsuario>();
            listaPaises.AddRange(Negocio.PaisNegocio.GetPaises().ToList());
            listaTipoUsuarios.AddRange(Negocio.TipoUsuarioNegocio.GetTiposUsuario().ToList());
            ViewBag.Paises = listaPaises;
            ViewBag.Provincias = listaProvincias;
            ViewBag.Localidades = listaLocalidades;
            ViewBag.TipoUsuarios = listaTipoUsuarios;
            //ViewBag.Provincias = new SelectList(Negocio.ProvinciaNegocio.GetProvincias(),"ProvinciaId","Descripcion");
            //ViewBag.Localidades = new SelectList(Negocio.LocalidadNegocio.GetLocalidades(), "LocalidadId", "Descripcion");
        }

        //
        // POST: /Usuario/Create

        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            try
            {
                UsuarioNegocio.Alta(usuario);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Usuario/Edit/5
 
        public ActionResult Edit(int id)
        {
            Usuario u = UsuarioNegocio.GetUsuarioById(id);
            return View("Edit", u);
        }

        //
        // POST: /Usuario/Edit/5

        [HttpPost]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                UsuarioNegocio.Modificar(usuario);
                return RedirectToAction("Index");
            }else
            {
                return View();
            }
        }

        //
        // GET: /Usuario/Delete/5
 
        public ActionResult Delete(int id)
        {
            Usuario u = UsuarioNegocio.GetUsuarioById(id);
            return View(u);
        }

        //
        // POST: /Usuario/Delete/5

        [HttpPost]
        public ActionResult Delete(Usuario u)
        {
            try
            {
                UsuarioNegocio.Eliminar(u.UsuarioId);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
