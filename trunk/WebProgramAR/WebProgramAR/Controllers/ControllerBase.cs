using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebProgramAR.Entidades;
using WebProgramAR.Negocio;
using WebProgramAR.Models;

namespace WebProgramAR.Controllers
{
    [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class ControllerBase : Controller
    {

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            
            //Validacion para desloguear si el usuario logueado fue borrado de la base
            if (!string.IsNullOrEmpty(SimpleSessionPersister.UserName))
            {
                //Hay un usuario logueado, me fijo que exista en la base.
                Usuario userBd = UsuarioNegocio.GetUsuarioByLoginUsuario(SimpleSessionPersister.UserName);

                if (userBd == null)
                {
                    //significa que el usuario con el que estoy logueado, es inexistente, fue borrado mientras estaba online yo.
                    //Hago sign out y lo echo de la web
                    LogOutWithSessionClear();

                    throw new Exception("El usuario con el que esta logueado fue borrado.");
                }
            }
            //else
            //{
            //    //Significa que la sesion expiro, me salgo, y voy al home
            //    LogOutWithSessionClear();
            //    RedirectToAction("Index", "Home", new { LoginRequiered = true });
            //    return;

            //}
        }

        public void LogOutWithSessionClear()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
        }
      

        public Usuario GetUsuarioLogueado()
        {

            if (!string.IsNullOrEmpty(SimpleSessionPersister.UserName))
            {
                Usuario userBd = UsuarioNegocio.GetUsuarioByLoginUsuario(SimpleSessionPersister.UserName);

                if (userBd != null)
                {
                    return userBd;
                }
                else
                {
                    return ArmarUsuarioTest();
                }
            }
            else
            {
                return ArmarUsuarioTest();
            }

        }

        public Usuario ArmarUsuarioTest()
        {
            TipoUsuario tipo = TipoUsuarioNegocio.GetTipoUsuarioByName("Guest");

            return new Usuario() { TipoUsuarioId = tipo.TipoUsuarioId, Nombre = "Guest", UsuarioId = -1 };
        }
     

    }
}
