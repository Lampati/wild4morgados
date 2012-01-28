using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProgramAR.Entidades;
using WebProgramAR.Negocio;
using WebProgramAR.Sitio.Models;
using System.Web.Security;
using WebProgramAr.MailSender;

namespace WebProgramAR.Controllers
{
    public class EjercicioController : ControllerBase
    {
        //
        // GET: /Ejercicio/

        public ActionResult Index(int page = 1, string sort = "Nombre", string sortDir = "ASC",
             int usuarioId = -1, int cursoId = -1, string nombre = "", 
             int estadoEjercicio = -1, int nivelEjercicio = -1, bool global = false)
        {

            EstadoEjercicio estado = EstadoEjercicioNegocio.GetEstadoEjercicioByName("Aprobado");
            estadoEjercicio = estado.EstadoEjercicioId;

            var datos = ObtenerEjercicioGrillaModel(page, sort, sortDir, nombre, usuarioId, cursoId, estadoEjercicio, nivelEjercicio, global);

            ViewBag.NivelesEjercicio = Negocio.NivelEjercicioNegocio.GetNiveles();
            ViewBag.EstadosEjercicio = Negocio.EstadoEjercicioNegocio.GetEstadoEjercicios();

            return View(datos);

        }

        

        private object ObtenerEjercicioGrillaModel(int page, string sort, string sortDir, string nombre, int usuarioId, int cursoId, int estadoEjercicio, int nivelEjercicio, bool global)
        {
            //Pasar la cantidad por pagina a una constante mas copada.
            int cantidadPorPaginaTPC = 10;

            var numUsuarios = EjercicioNegocio.ContarCantidad(nombre,
                     usuarioId,
                     cursoId,
                     estadoEjercicio,
                     nivelEjercicio,
                     global);

            sortDir = sortDir.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? sortDir : "asc";

            var validColumns = new[] { "Nombre", "Usuario", "Curso", "EstadoEjercicio", "NivelEjercicio", "Global" };

            if (!validColumns.Any(c => c.Equals(sort, StringComparison.CurrentCultureIgnoreCase)))
                sort = "Nombre";

            var ejers = EjercicioNegocio.ObtenerPagina(
                     page,
                     cantidadPorPaginaTPC,
                     sort + " " + sortDir,
                     nombre,
                     usuarioId,
                     cursoId,
                     estadoEjercicio,
                     nivelEjercicio,
                     global
            );

            var datos = new EjercicioGrillaModel()
            {
                Cantidad = numUsuarios,
                CantidadPorPagina = cantidadPorPaginaTPC,
                Ejercicios = ejers,
                PaginaActual = page
            };

            return datos;
        }

        private void Initilization()
        {
            List<NivelEjercicio> listaNivelesEjercicio = new List<NivelEjercicio>();
            listaNivelesEjercicio.AddRange(Negocio.NivelEjercicioNegocio.GetNiveles().ToList());
            ViewBag.NivelesEjercicio = listaNivelesEjercicio;
        }

        //
        // GET: /Ejercicio/Details/5

        public ActionResult Details(int id)
        {
            Ejercicio e = EjercicioNegocio.GetEjercicioById(id);
            return View(e);
        }

        //
        // GET: /Ejercicio/Create

        public ActionResult Create()
        {
            Initilization();
            return View();
        }
      

        [HttpPost]
        public ActionResult Create(Ejercicio ejercicio)
        {
            if (ModelState.IsValid)
            {

                //flanzani
                //Una vez que tengamos el usuarioId en sesion, lo ponemos aca. Mientras tanto, usamos 1.
                ejercicio.UsuarioId = 1;
                //curso.UsuarioId = usuarioLogueado;
                ejercicio.Enunciado = "";
                ejercicio.EstadoEjercicio.EstadoEjercicioId = 1;
                ejercicio.SolucionTexto = "";
                EjercicioNegocio.Alta(ejercicio);
                return Content(Boolean.TrueString);
            }
            else
            {
                return View();
            }
        }
        
        //
        // GET: /Ejercicio/Edit/5 
        public ActionResult Edit(int id)
        {
            Initilization();
            Ejercicio e = EjercicioNegocio.GetEjercicioById(id);
            return View("Edit", e);
        }

        //
        // POST: /Ejercicio/Edit/5

        [HttpPost]
        public ActionResult Edit(Ejercicio ejercicio)
        {
            // TODO: Add update logic here
            if (ModelState.IsValid)
            {
                EjercicioNegocio.Modificar(ejercicio);
                return Content(Boolean.TrueString);
            }else
            {
                return View();
            }
        }

        //
        // GET: /Ejercicio/Delete/5
 
        public ActionResult Delete(int id)
        {
            Ejercicio e = EjercicioNegocio.GetEjercicioById(id);
            return View(e);
        }

        //
        // POST: /Ejercicio/Delete/5

        [HttpPost]
        public ActionResult Delete(Ejercicio ejercicio)
        {
            try
            {
                // TODO: Add delete logic here
                EjercicioNegocio.Eliminar(ejercicio.EjercicioId);

                return Content(Boolean.TrueString);
            }
            catch
            {
                return View();
            }
        }


        [Authorize(Roles = "administrador, moderador")]
        public ActionResult PendientesAprobacion(int page = 1, string sort = "Nombre", string sortDir = "ASC",
             int usuarioId = -1, int cursoId = -1, string nombre = "", int nivelEjercicio = -1, bool global = false)
        {

            EstadoEjercicio estado = EstadoEjercicioNegocio.GetEstadoEjercicioByName("Pendiente");
            int estadoEjercicio = estado.EstadoEjercicioId;

            //Pasar la cantidad por pagina a una constante mas copada.


            var datos = ObtenerEjercicioGrillaModel(page, sort, sortDir, nombre, usuarioId, cursoId, estadoEjercicio, nivelEjercicio, global);

            ViewBag.NivelesEjercicio = Negocio.NivelEjercicioNegocio.GetNiveles();

            return View("PendientesAprobacion", datos);

        }

        [Authorize(Roles = "administrador, moderador")]
        public ActionResult Desaprobados(int page = 1, string sort = "Nombre", string sortDir = "ASC",
             int usuarioId = -1, int cursoId = -1, string nombre = "", int nivelEjercicio = -1, bool global = false)
        {

            EstadoEjercicio estado = EstadoEjercicioNegocio.GetEstadoEjercicioByName("Desaprobado");
            int estadoEjercicio = estado.EstadoEjercicioId;

            //Pasar la cantidad por pagina a una constante mas copada.


            var datos = ObtenerEjercicioGrillaModel(page, sort, sortDir, nombre, usuarioId, cursoId, estadoEjercicio, nivelEjercicio, global);

            ViewBag.NivelesEjercicio = Negocio.NivelEjercicioNegocio.GetNiveles();

            return View("Desaprobados", datos);

        }

        //
        // GET: /Curso/Details/5

        public ActionResult AsociarCursoEjercicio(int page = 1, string sort = "Nombre", string sortDir = "ASC",
             int usuarioId = -1, int cursoId = -1, string nombre = "",
             int estadoEjercicio = -1, int nivelEjercicio = -1, bool global = false)
        {
            var datos = ObtenerEjercicioGrillaModel(page, sort, sortDir, nombre, usuarioId, cursoId, estadoEjercicio, nivelEjercicio, global);

            ViewBag.NivelesEjercicio = Negocio.NivelEjercicioNegocio.GetNiveles();
            ViewBag.EstadosEjercicio = Negocio.EstadoEjercicioNegocio.GetEstadoEjercicios();

            return View(datos);
        }

        //
        // POST: /Curso/Edit/5

        [HttpPost]
        public ActionResult AsociarCursoEjercicio(Curso curso)
        {
            // TODO: Add update logic here
            if (ModelState.IsValid)
            {
                CursoNegocio.AsociarCursoEjercicio(curso);
                return Content(Boolean.TrueString);
            }
            else
            {
                return View();
            }

        }

   
        [Authorize(Roles = "administrador, moderador")]
        public ActionResult Moderar(int id)
        {
            ModerarEjercicioModel model = new ModerarEjercicioModel();
            model.Ejercicio = EjercicioNegocio.GetEjercicioById(id);
            model.Aceptado = false;
            model.MensajeMail = string.Empty;


            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "administrador, moderador")]
        public ActionResult Moderar(ModerarEjercicioModel model)
        {
            bool resultado = false;

            try
            {
                Ejercicio ejercicio = EjercicioNegocio.GetEjercicioByIdOnlyUser(model.Ejercicio.EjercicioId);


                
                

                MembershipUser membUsuario = Membership.GetUser(ejercicio.Usuario.UsuarioNombre);
                Ejercicio ejercicioFunca = new Ejercicio();

                ejercicioFunca.EjercicioId = ejercicio.EjercicioId;
                ejercicioFunca.EstadoEjercicioId = ejercicio.EstadoEjercicioId;
                ejercicioFunca.Enunciado = ejercicio.Enunciado;
                ejercicioFunca.SolucionTexto = ejercicio.SolucionTexto;
                ejercicioFunca.Global = ejercicio.Global;

                EstadoEjercicio estado;

                if (model.Aceptado)
                {
                    //MailManager.Enviar("programAr@gmail.com", membUsuario.Email, string.Format("Ejercicio Aprobado"), "Aprobado");
                    estado = EstadoEjercicioNegocio.GetEstadoEjercicioByName("Aprobado");
                    
                }
                else
                {
                    //MailManager.Enviar("programAr@gmail.com", membUsuario.Email, string.Format("Ejercicio Desaprobado"), "Desaprobado");
                    estado = EstadoEjercicioNegocio.GetEstadoEjercicioByName("Desaprobado");
                }

                ejercicioFunca.EstadoEjercicioId = estado.EstadoEjercicioId;
                EjercicioNegocio.Modificar(ejercicioFunca);
            }
            catch (Exception)
            {
                return Content(Boolean.FalseString);
            }

            return Content(Boolean.TrueString);
        }
    }
}
