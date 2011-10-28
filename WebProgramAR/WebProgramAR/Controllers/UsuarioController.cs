using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProgramAR.Entidades;
using WebProgramAR.Negocio;

namespace WebProgramAR.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/

        public ActionResult Index()
        {
            List<Usuario> usuarios = UsuarioNegocio.ObtenerPagina(1, 1, "", 0, "").ToList();
            List<TipoUsuario> tiposUsuarios = TipoUsuarioNegocio.ObtenerPagina(1, 1, "", 0, "").ToList();

            List<Pais> paises = PaisNegocio.ObtenerPagina(1, 1, "", "1", "").ToList();
            List<Provincia> provs = ProvinciaNegocio.ObtenerPagina(1, 1, "", "1", "").ToList();
            List<Localidad> localidades = LocalidadNegocio.ObtenerPagina(1, 1, "", "1", "").ToList();
            return View();
        }

        //
        // GET: /Usuario/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Usuario/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Usuario/Create

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
        // GET: /Usuario/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Usuario/Edit/5

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
        // GET: /Usuario/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Usuario/Delete/5

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
