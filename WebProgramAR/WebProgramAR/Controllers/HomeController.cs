using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebProgramAR.Models;
using WebProgramAR.Entidades;
using WebProgramAR.Sitio.Models;
using WebProgramAR.MailSender;
using System.Text;
using System.IO;
using System.Web.Hosting;

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
            Usuario userLogueado = GetUsuarioLogueado();

            ViewBag.Message = "Contacto";
            List<Pais> listaPaises = new List<Pais>();
            List<Provincia> listaProvincias = new List<Provincia>();
            List<Localidad> listaLocalidades = new List<Localidad>();
            listaPaises.AddRange(Negocio.PaisNegocio.GetPaises(userLogueado).ToList());
            ViewBag.Paises = listaPaises;
            ViewBag.Provincias = listaProvincias;
            ViewBag.Localidades = listaLocalidades;
            return View();
        }
        [HttpPost]
        public ActionResult About(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                StringBuilder strbldr = new StringBuilder();
                strbldr.AppendLine("Nombre:" + model.Nombre);
                strbldr.AppendLine("Email:" + model.Email);
                strbldr.AppendLine("Telefono:" + model.Telefono);
                strbldr.AppendLine("Mensaje:" + model.Descripcion);
                MailManager.Enviar(model.Email, "Contacto - " + model.Nombre, strbldr.ToString());
                return Content(Boolean.TrueString);
            }
            else
            {
                return View();
            }
        }
        
        public ActionResult Descripcion()
        {
            ViewBag.Message = "Que Es?";
            return View();
        }
        public ActionResult Ayuda()
        {
            ViewBag.Message = "Ayuda";
            return View();
        }

        public ActionResult DescargarRagnarok()
        {
            ViewBag.Message = "Descargar Ragnarok";
            return View();
        }

        [HttpPost]
        public ActionResult DescargarInstaladorRagnarok()
        {
            FileInfo fileInfo = new FileInfo(Path.Combine(HostingEnvironment.MapPath("~/Files"), "Instalador.zip"));
             
            
            //var document = "";
            //var cd = new System.Net.Mime.ContentDisposition
            //{
            //    // for example foo.bak
            //    FileName = fileInfo.Name,

            //    // always prompt the user for downloading, set to true if you want 
            //    // the browser to try to show the file inline
            //    Inline = false,
            //};
            //Response.AppendHeader("Content-Disposition", cd.ToString());


            return File(fileInfo.FullName, "application/zip","Instalador.zip");
        }
    }
}
