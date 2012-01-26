using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProgramAR.Entidades;
using WebProgramAR.Negocio;
using WebProgramAR.Sitio.Models;
using System.Web.Security;
using WebProgramAR.Models;

namespace WebProgramAR.Controllers
{
    public class UsuarioController : ControllerBase
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
        private void Initilization()
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
            Initilization();
            return View("Edit", u);
        }

        //
        // GET: /Usuario/MiPerfil/ 
        [Authorize]
        public ActionResult MiPerfil()
        {
            string usuario = SimpleSessionPersister.UserName;
            Usuario u = UsuarioNegocio.GetUsuarioByLoginUsuario(usuario);
            Initilization();
            return View("MiPerfil", u);
        }

        //
        // POST: /Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                ActualizarRolesSiCorresponde(usuario);

               

                UsuarioNegocio.Modificar(usuario);
                return RedirectToAction("Index");
            }else
            {
                return View();
            }
        }

        private void ActualizarRolesSiCorresponde(Usuario usuario)
        {
            Usuario userActual = UsuarioNegocio.GetUsuarioById(usuario.UsuarioId);

            MembershipUser us = Membership.GetUser(usuario.UsuarioNombre);
            string[] roles = Roles.GetRolesForUser(usuario.UsuarioNombre);

            if (userActual.TipoUsuarioId != usuario.TipoUsuarioId)
            {
                UsuarioNegocio.QuitarRolesUsuario(userActual);

                UsuarioNegocio.AgregarRolAUsuario(usuario);               
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
                

                UsuarioNegocio.Eliminar(u);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Register()
        {
            Initilization();
            return View();
        }
        
        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(Usuario model)
        {
            ModelState.Remove("TipoUsuario");

            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                //Aca lo creo en la tabla para la autenticacion
                
                Membership.CreateUser(model.UsuarioNombre, model.Contrasena, model.Email,null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    //Aca le agrego el rol en la tabla para autenticacion
                    Roles.AddUserToRole(model.UsuarioNombre, "Profesor");
                    //Aca creo mi parte, en mis tablas
                    TipoUsuario tipo = TipoUsuarioNegocio.GetTipoUsuarioByName("Profesor");
                    model.TipoUsuarioId = tipo.TipoUsuarioId;
                    UsuarioNegocio.Alta(model);
                    FormsAuthentication.SetAuthCookie(model.UsuarioNombre, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
                return RedirectToAction("Index");
            }
            else
            {
                // If we got this far, something failed, redisplay form
                return View(model);
            }
        }

        /// <summary>
        /// Cargar Provincias de acuerdo al pais.
        /// </summary>
        [HttpPost]
        public JsonResult GetProvinciasByPais(string paisId)
        {
            List<Provincia> listaProvincias = Negocio.ProvinciaNegocio.GetProvinciasByPais(paisId).ToList();

            List<GenericJsonModel> listaRetorno = new List<GenericJsonModel>();
            foreach (Provincia item in listaProvincias)
            {
                listaRetorno.Add(new GenericJsonModel() { Id = item.ProvinciaId, Value = item.Descripcion });
            }

            return Json(listaRetorno, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Cargar Localidades de acuerdo a la provincia.
        /// </summary>
        [HttpPost]
        public JsonResult GetLocalidadesByLocalidadByProvinciaByPais(string Localidad, string provinciaId, string paisId)
        {
            List<Localidad> listaLocalidades = Negocio.LocalidadNegocio.GetLocalidadesByLocalidadByProvinciaByPais(Localidad, provinciaId, paisId).ToList();

            List<GenericJsonModel> listaRetorno = new List<GenericJsonModel>();
            foreach (Localidad item in listaLocalidades)
            {
                listaRetorno.Add(new GenericJsonModel() { Id = item.LocalidadId, Value = item.Descripcion });
            }

            return Json(listaRetorno, JsonRequestBehavior.AllowGet);

        }
    }
}
