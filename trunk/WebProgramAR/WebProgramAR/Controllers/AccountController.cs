﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebProgramAR.Models;
using WebProgramAR.Entidades;
using WebProgramAr.MailSender;
using WebProgramAR.Negocio;

namespace WebProgramAR.Controllers
{
    public class AccountController : ControllerBase
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
                    return Content(Boolean.FalseString);
                    //RedirectToAction("Index", "Home");
                }
            }
            
            // If we got this far, something failed, redisplay form
            //return View(model);
            return Content(Boolean.FalseString);
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
            FormsAuthentication.SignOut();
            return Json(Boolean.TrueString,JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            ChangePasswordModel model = new ChangePasswordModel();
            model.EsResetPassword = false;
            model.UserName = User.Identity.Name;

            return View("ChangePassword", model);
        }

        [Authorize]
        public ActionResult ResetPassword(int id)
        {
            Usuario usuario = UsuarioNegocio.GetUsuarioById(id);
            

            ChangePasswordModel model = new ChangePasswordModel();
            model.EsResetPassword = true;
            model.OldPassword = "noValueInserted";
            model.UserName = usuario.UsuarioNombre;

            return View("ChangePassword",model);
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
                    if (model.EsResetPassword)
                    {
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
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    return Content(Boolean.FalseString);
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

                        //DEVOLVER ERROR
                    }
                    else
                    {
                        string username = Membership.GetUserNameByEmail(model.Email);
                        MembershipUser usuarioARecuperar = usuarios[username];

                        string nuevaPassword = usuarioARecuperar.ResetPassword();

                        //MailManager.Enviar("programar", model.Email, "Cambio de Password", string.Format("La nueva contraseña es: {0}", nuevaPassword));
                    }
                }
                else
                {
                    //No existe usuario con ese mail. Abortamos todo
                }

                return Content(Boolean.TrueString);
            }
            else
            {
                // If we got this far, something failed, redisplay form
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
