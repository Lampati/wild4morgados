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

            Usuario usuarioLogueado = GetUsuarioLogueado();

            var numUsuarios = UsuarioNegocio.ContarCantidad(nombre,
                     apellido,
                     usuarioNombre,
                     tipoUsuarioId,
                     pais,
                     provincia,
                     localidad,
                     usuarioLogueado);

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
                     localidad,
                     usuarioLogueado
            );

            var datos = new UsuarioGrillaModel()
            {
                Cantidad = numUsuarios,
                CantidadPorPagina = cantidadPorPaginaTPC,
                Usuarios = ejers,
                PaginaActual = page
            };

            ViewBag.TipoUsuarios = new SelectList(Negocio.TipoUsuarioNegocio.GetTiposUsuarioSinGuest(), "TipoUsuarioId", "Descripcion"); 
            return View(datos);

        }

        //
        // GET: /Usuario/Details/5

        public ActionResult Details(int id)
        {
            if (Request.IsAjaxRequest())
            {
                Usuario u = UsuarioNegocio.GetUsuarioById(id);

                MembershipUser membUser = Membership.GetUser(u.UsuarioNombre);
                u.Email = membUser.Email;

                return View(u);
            }
            else
            {
                throw new Exception("No se puede acceder a esta pagina de ese modo. Por favor use la pagina para acceder");
            }
        }

        //
        // GET: /Usuario/Create

        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
            {
                ViewBag.EsRegister = false;
                Initilization();
                return View();
            }
            else
            {
                throw new Exception("No se puede acceder a esta pagina de ese modo. Por favor use la pagina para acceder");
            }
        }
        /// <summary>
        /// Cargar Paises. Las provincias y localidades se cargan por ajax, deben traer resultados de acuerdo a la eleccion
        /// </summary>
        private void Initilization()
        {
            Usuario usuarioLogueado = GetUsuarioLogueado(); 

            List<Pais> listaPaises = new List<Pais>();
            List<Provincia> listaProvincias = new List<Provincia>();
            List<Localidad> listaLocalidades = new List<Localidad>();
            List<TipoUsuario> listaTipoUsuarios = new List<TipoUsuario>();
            listaPaises.AddRange(Negocio.PaisNegocio.GetPaises(usuarioLogueado).ToList());
            listaTipoUsuarios.AddRange(Negocio.TipoUsuarioNegocio.GetTiposUsuarioSinGuest().ToList());
            ViewBag.Paises = listaPaises;
            ViewBag.Provincias = listaProvincias;
            ViewBag.Localidades = listaLocalidades;
            ViewBag.TipoUsuarios = listaTipoUsuarios;
        }

        //
        // POST: /Usuario/Create
        [HttpPost]
        public ActionResult Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus estado = CrearUsuario(model.Usuario, model.Contrasena, model.Usuario.TipoUsuarioId);

                if (estado != MembershipCreateStatus.Success)
                {
                    string errorDevolver = ObtenerErrorCreacionOModificacionUsuario(estado);
                    return Content(errorDevolver);
                }
                else
                {
                    return Content(Boolean.TrueString);
                }            
            }
            else
            {
                // If we got this far, something failed, redisplay form
                return View(model);
            }
        }
        
        //
        // GET: /Usuario/Edit/5 
        public ActionResult Edit(int id, bool esMiPerfil = false)
        {
            if (Request.IsAjaxRequest())
            {
                Usuario u = UsuarioNegocio.GetUsuarioById(id);

                MembershipUser membUser = Membership.GetUser(u.UsuarioNombre);
                u.Email = membUser.Email;

                ViewBag.EsMiPerfil = esMiPerfil;
                ViewBag.descripcionLocalidad = Negocio.LocalidadNegocio.GetLocalidadById(u.LocalidadId).Descripcion;
                Initilization();
                return View("Edit", u);
            }
            else
            {
                throw new Exception("No se puede acceder a esta pagina de ese modo. Por favor use la pagina para acceder");
            }
        }

        //
        // GET: /Usuario/MiPerfil/ 
        [Authorize]
        public ActionResult MiPerfil()
        {
            string usuario = SimpleSessionPersister.UserName;
            Usuario u = UsuarioNegocio.GetUsuarioByLoginUsuario(usuario);
            
            MembershipUser membUser = Membership.GetUser(u.UsuarioNombre);
            u.Email = membUser.Email;

            ViewBag.EsMiPerfil = false;

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

                MembershipUser membUser = Membership.GetUser(usuario.UsuarioNombre);

                bool error = false;
                string errorMensaje="";
                string mailViejo = membUser.Email;
                bool mailCambiado = false;

                if  ( string.Compare( usuario.Email, membUser.Email, true) != 0)
                {
                    
                    
                    membUser.Email = usuario.Email;
                    try
                    {
                        Membership.UpdateUser(membUser);
                        mailCambiado = true;
                    }
                    catch (Exception)
                    {
                        error = true;
                        errorMensaje = "Error al intentar modificar el mail. Ese mail ya estaba tomado";
                    }
                    
                }

                if (!error)
                {
                    try
                    {
                        UsuarioNegocio.Modificar(usuario);
                    }
                    catch (Exception)
                    {
                        if (mailCambiado)
                        {
                            membUser.Email = mailViejo;
                            Membership.UpdateUser(membUser);
                        }

                        return Content(Boolean.FalseString);
                    }
                    
                }
                else
                {
                    return Content(errorMensaje);
                }

                return RedirectToAction("Index");
            }
            else
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
            if (Request.IsAjaxRequest())
            {
                Usuario u = UsuarioNegocio.GetUsuarioById(id);
                return View(u);
            }
            else
            {
                throw new Exception("No se puede acceder a esta pagina de ese modo. Por favor use la pagina para acceder");
            }

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
            ViewBag.EsRegister = true;
            return View();
        }
        
        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            ModelState.Remove("TipoUsuario");

            if (ModelState.IsValid)
            {

                MembershipCreateStatus estado = CrearUsuario(model.Usuario, model.Contrasena);

                if (estado != MembershipCreateStatus.Success)
                {
                    string errorDevolver = "Error:"+ObtenerErrorCreacionOModificacionUsuario(estado);
                    return Content(errorDevolver);
                }
                else
                {
                    //FormsAuthentication.SetAuthCookie(model.Usuario.UsuarioNombre, false /* createPersistentCookie */);
                    return Content("../Home/Index");
                }            

            }
            else
            {
                ViewBag.EsRegister = true;
                // If we got this far, something failed, redisplay form
                return View(model);
            }
        }

        private MembershipCreateStatus CrearUsuario(Usuario model, string pass)
        {            
            return CrearUsuario(model, pass, "profesor");
        }

        private MembershipCreateStatus CrearUsuario(Usuario model, string pass, int idTipo)
        {
            TipoUsuario tipo = TipoUsuarioNegocio.GetTipoUsuarioById(idTipo);
            return CrearUsuario(model, pass, tipo.Descripcion.ToLower());
        }

        private MembershipCreateStatus CrearUsuario(Usuario model, string pass, string rolParaBd)
        {
            // Attempt to register the user
            MembershipCreateStatus createStatus;
            //Aca lo creo en la tabla para la autenticacion

            Membership.CreateUser(model.UsuarioNombre, pass, model.Email, null, null, true, null, out createStatus);

            if (createStatus == MembershipCreateStatus.Success)
            {
                try
                {
                    //Aca le agrego el rol en la tabla para autenticacion
                    Roles.AddUserToRole(model.UsuarioNombre, rolParaBd);

                    try
                    {
                        TipoUsuario tipo = TipoUsuarioNegocio.GetTipoUsuarioByName(rolParaBd);
                        model.TipoUsuarioId = tipo.TipoUsuarioId;
                        UsuarioNegocio.Alta(model);
                    }
                    catch (Exception)
                    {
                        Roles.RemoveUserFromRole(model.UsuarioNombre, rolParaBd);
                        throw;
                    }
                }
                catch
                {
                    Membership.DeleteUser(model.UsuarioNombre);
                    createStatus = MembershipCreateStatus.UserRejected;
                }
            }

            return createStatus;
           
        }

        /// <summary>
        /// Cargar Provincias de acuerdo al pais.
        /// </summary>
        [HttpPost]
        public JsonResult GetProvinciasByPais(string paisId)
        {
            Usuario userLogueado = GetUsuarioLogueado();

            List<Provincia> listaProvincias = Negocio.ProvinciaNegocio.GetProvinciasByPais(paisId,userLogueado).ToList();

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
            Usuario userLogueado = GetUsuarioLogueado();

            List<Localidad> listaLocalidades = Negocio.LocalidadNegocio.GetLocalidadesByLocalidadByProvinciaByPais(Localidad, provinciaId, paisId, userLogueado).ToList();

            List<GenericJsonModel> listaRetorno = new List<GenericJsonModel>();
            foreach (Localidad item in listaLocalidades)
            {
                listaRetorno.Add(new GenericJsonModel() { Id = item.LocalidadId, Value = item.Descripcion });
            }

            return Json(listaRetorno, JsonRequestBehavior.AllowGet);

        }


        private string ObtenerErrorCreacionOModificacionUsuario(MembershipCreateStatus estado)
        {
            string mensaje = string.Empty;
            switch (estado)
            {
                case MembershipCreateStatus.Success:
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    mensaje = "Ese mail ya se encuentra vinculado a otro usuario. Por favor, utilice otro.";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    mensaje = "Ese nombre de usuario ya se encuentra vinculado a otro usuario. Por favor, utilice otro.";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    mensaje = "El mail ingresado es invalido. Por favor, utilice otro.";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    mensaje = "La contraseña ingresada es invalida. Por favor, utilice otra.";
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    mensaje = "El mombre de usuario ingresado es invalido. Por favor, utilice otro.";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                case MembershipCreateStatus.ProviderError:
                case MembershipCreateStatus.DuplicateProviderUserKey:
                case MembershipCreateStatus.UserRejected:
                    mensaje = "Error al crear el nuevo usuario. Por favor intentelo de nuevo.";
                    break;
                default:
                    mensaje = "Error al crear el nuevo usuario. Por favor intentelo de nuevo.";
                    break;
            }

            return mensaje;
        }
    }
}
