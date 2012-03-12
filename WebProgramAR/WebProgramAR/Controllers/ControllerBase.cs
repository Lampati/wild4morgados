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

        public Usuario GetUsuarioLogueado()
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

        public Usuario ArmarUsuarioTest()
        {
            TipoUsuario tipo = TipoUsuarioNegocio.GetTipoUsuarioByName("Guest");

            return new Usuario() { TipoUsuarioId = tipo.TipoUsuarioId, Nombre = "Guest", UsuarioId = -1 };
        }
     

    }
}
