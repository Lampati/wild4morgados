using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebProgramAR.Models;
using WebProgramAR.Entidades;

namespace WebProgramAR.Controllers
{
    public class AccountController : Controller
    {

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }
        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    SimpleSessionPersister.UserName = model.UserName;
                    model.isAuthenticated = true;
                    
                    return Json(new { success = true });
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    model.isAuthenticated = false;
                    return Json(new { success = false });
                }
            }
            
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        //
        // POST: /Account/LogOn
        /*
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

       */
        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
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

            listaPaises.AddRange(Negocio.PaisNegocio.GetPaises().ToList());

            ViewBag.Paises = listaPaises;
            ViewBag.Provincias = listaProvincias;
            ViewBag.Localidades = listaLocalidades;
            //ViewBag.Provincias = new SelectList(Negocio.ProvinciaNegocio.GetProvincias(),"ProvinciaId","Descripcion");
            //ViewBag.Localidades = new SelectList(Negocio.LocalidadNegocio.GetLocalidades(), "LocalidadId", "Descripcion");
        }
        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(UserModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
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

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
