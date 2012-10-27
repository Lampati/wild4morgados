using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebProgramAR.Models;
using WebProgramAR.Entidades;
using WebProgramAR.MailSender;
using WebProgramAR.Negocio;

namespace WebProgramAR.Controllers
{
    public class ErrorController : ControllerBase
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
            string errorMessage = "El nombe de usuario o la contraseña son incorrectos.";
            
            if (ModelState.IsValid)
            {
                
                if (Membership.GetUser(model.UserName) != null)
                {
                    //Check to see if the user is currently locked out
                    if (Membership.GetUser(model.UserName).IsLockedOut)
                    {
                        //Get the last lockout  date from the user
                        DateTime lastLockout = Membership.GetUser(model.UserName).LastLockoutDate;
                        //Calculate the time the user should be unlocked
                        DateTime unlockDate = lastLockout.AddMinutes(Membership.PasswordAttemptWindow);
 
                        //Check to see if it is time to unlock the user
                        if (DateTime.Now > unlockDate)
                        {
                            Membership.GetUser(model.UserName).UnlockUser();
                        }
                    }               

                    if (Membership.ValidateUser(model.UserName, model.Password))
                    {
                    
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        SimpleSessionPersister.UserName = model.UserName;

                        model.isAuthenticated = true;

                        //RedirectToAction("Index", "Home");
                        return Content(Boolean.TrueString);
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                        model.isAuthenticated = false;
                        return Content(errorMessage);
                        //RedirectToAction("Index", "Home");
                    }
                }
                else
	            {
                    return Content(errorMessage);
	            }
            }
            
            // If we got this far, something failed, redisplay form
            //return View(model);
            return Content(errorMessage);
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
        //// GET: /Account/LogOff        
        //public ActionResult LogOff()
        //{
        //    FormsAuthentication.SignOut();
        //    return Content(Boolean.TrueString);
        //}

        [Authorize]
        [HttpPost]
        public ActionResult LogOff()
        {
            LogOutWithSessionClear();
            return Json(Boolean.TrueString,JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            if (Request.IsAjaxRequest())
            {
                ChangePasswordModel model = new ChangePasswordModel();
                model.EsResetPassword = false;
                model.UserName = User.Identity.Name;

                return View("ChangePassword", model);
            }
            else
            {
                return View("Error");
                throw new Exception("No se puede acceder a esta pagina de ese modo. Por favor use la pagina para acceder");
            }
        }

        
        [Authorize(Roles = "administrador")]
        public ActionResult ResetPassword(int id)
        {
            if (Request.IsAjaxRequest())
            {
                if (UsuarioNegocio.ExisteUsuarioById(id))
                {
                    Usuario usuario = UsuarioNegocio.GetUsuarioById(id);


                    ChangePasswordModel model = new ChangePasswordModel();
                    model.EsResetPassword = true;
                    model.OldPassword = "noValueInserted";
                    model.UserName = usuario.UsuarioNombre;

                    return View("ChangePassword", model);
                }
                else
                {
                    throw new Exception("El usuario seleccionado fue borrado.");
                }
            }
            else
            {
                return View("Error");
                throw new Exception("No se puede acceder a esta pagina de ese modo. Por favor use la pagina para acceder");
            }
        }

        //
        // POST: /Account/ChangePassword

        
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            string errorMessage = "La actual contraseña es incorrecta o la nueva contraseña es invalida";
                    
            if (ModelState.IsValid)
            {
                
                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    if (model.EsResetPassword)
                    {
                        errorMessage = "Ha ocurrido un error inesperado. Contactese con su administrador.";
                        MembershipUser usuarioAResetar = Membership.GetUser(model.UserName);
                        string passActual = usuarioAResetar.GetPassword();
                        changePasswordSucceeded = usuarioAResetar.ChangePassword(passActual, model.NewPassword);

                    }
                    else
                    {
                        MembershipUser currentUser = Membership.GetUser(model.UserName, true /* userIsOnline */);
                        changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                    }
                    
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return Content(Boolean.TrueString);
                    //return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    return Content(errorMessage);
                }
                //return RedirectToAction("Index");
                return Content(Boolean.FalseString);
            }
            else
            {
                // If we got this far, something failed, redisplay form
                //return View(model);
                return Content(Boolean.FalseString);
            }
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public ActionResult RecoverPassword()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult RecoverPassword(RecoverPasswordModel model)
        {
            if (ModelState.IsValid)
            {

                MembershipUserCollection usuarios = Membership.FindUsersByEmail(model.Email);

                if (usuarios.Count > 0)
                {
                    
                    if (usuarios.Count > 1)
                    {
                        //Hay mas de un usuario por mail, y eso no esta permitido.
                        string errorMensaje = "Existe mas de un usuario con ese Email";
                        return Content(errorMensaje);
                        //DEVOLVER ERROR
                    }
                    else
                    {
                        string username = Membership.GetUserNameByEmail(model.Email);
                        MembershipUser usuarioARecuperar = usuarios[username];

                        string actualPassword = usuarioARecuperar.GetPassword();

                        MailManager.Enviar( model.Email, "Su contraseña", string.Format("Su contraseña es: {0}", actualPassword));
                    }
                }
                else
                {
                    // El mail de alguna manera llego incorrecto. Devuelvo false.
                    string errorMensaje = "No Existe un usuario con ese Email";
                    return Content(errorMensaje);
                    //No existe usuario con ese mail. Abortamos todo
                }

                //Siempre devuelvo true, pq me interesa que un usuario malicioso no pueda descubrir los mails registrados en la aplicacion.
                return Content(Boolean.TrueString);
            }
            else
            {
                // El mail de alguna manera llego incorrecto. Devuelvo false.
                return Content(Boolean.FalseString);
            }
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
