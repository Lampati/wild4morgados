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
            //Membership.CreateUser("admin", "admin123", "admin@mail.com", null, null, true, null, out createStatus);           
            //Roles.AddUserToRole("admin", "Administrador");

            string usuario = SimpleSessionPersister.UserName;


            ViewBag.Message = "Welcome to ASP.NET MVC!";
            ViewBag.LoginRequired = LoginRequiered ? 1 : 0;
       

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
