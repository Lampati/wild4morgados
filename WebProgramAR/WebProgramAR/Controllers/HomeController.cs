using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebProgramAR.Models;

namespace WebProgramAR.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index(bool LoginRequiered = false)
        {
            //MembershipCreateStatus createStatus;
            //Membership.CreateUser("sa", "morgado", "morgado@mail.com", null, null, true, null, out createStatus);           
            //Roles.AddUserToRole("sa", "Administrador");

            //Membership.CreateUser("profe", "morgado", "morgado@gmail.com", null, null, true, null, out createStatus);
            //Roles.AddUserToRole("profe", "Profesor");

            //Membership.CreateUser("moderador", "morgado", "morgado@gmail.com", null, null, true, null, out createStatus);
            //Roles.AddUserToRole("moderador", "Moderador");



            ViewBag.Message = "Welcome to ASP.NET MVC!";
            ViewBag.LoginRequired = LoginRequiered ? 1 : 0;

            ////////MailSender.MailManager.Enviar("fede@program-ar.com.ar", "fed_lanza@hotmail.com", "prueba", "hola! probando");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Contacto";
            return View();
        }
        public ActionResult Descripcion()
        {
            ViewBag.Message = "Que Es?";
            return View();
        }
    }
}
