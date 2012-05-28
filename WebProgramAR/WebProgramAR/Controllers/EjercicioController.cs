using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WebProgramAR.Entidades;
using WebProgramAR.Negocio;
using WebProgramAR.Sitio.Models;
using System.Web;
using System.IO;

namespace WebProgramAR.Controllers
{
    public class EjercicioController : ControllerBase
    {
        //
        // GET: /Ejercicio/

        public ActionResult Index(int page = 1, string sort = "Nombre", string sortDir = "ASC",
             int usuarioId = -1, int cursoId = -1, string nombre = "", 
             int estadoEjercicio = -1, int nivelEjercicio = -1, bool global = false, bool conLayout = true, bool aplicarPermisos = true)
        {
            Usuario userLogueado = GetUsuarioLogueado();

            EstadoEjercicio estado = EstadoEjercicioNegocio.GetEstadoEjercicioByName("Aprobado");
            estadoEjercicio = estado.EstadoEjercicioId;

            var datos = ObtenerEjercicioGrillaModel(page, sort, sortDir, nombre, usuarioId, cursoId, estadoEjercicio, nivelEjercicio, global);
            datos.ConLayout = conLayout;
            datos.AplicarPermisos = aplicarPermisos;

            //ViewBag.NivelesEjercicio = Negocio.NivelEjercicioNegocio.GetNiveles();
            ViewBag.NivelesEjercicio = new List<short>(new short[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });  
            ViewBag.EstadosEjercicio = Negocio.EstadoEjercicioNegocio.GetEstadoEjercicios();

            return View(datos);

        }


        public ActionResult ListarEjerciciosGrillaSinPermisos(int page = 1, string sort = "Nombre", string sortDir = "ASC",
            int usuarioId = -1, int cursoId = -1, string nombre = "",
            int estadoEjercicio = -1, int nivelEjercicio = -1, bool global = false, bool conLayout = true, bool aplicarPermisos = true)
        {
            Usuario userLogueado = GetUsuarioLogueado();

            var datos = ObtenerEjercicioGrillaModel(page, sort, sortDir, nombre, usuarioId, cursoId, estadoEjercicio, nivelEjercicio, global);
            datos.ConLayout = conLayout;
            datos.AplicarPermisos = aplicarPermisos;

            //ViewBag.NivelesEjercicio = Negocio.NivelEjercicioNegocio.GetNiveles();
            ViewBag.NivelesEjercicio = new List<short>(new short[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });  
            ViewBag.EstadosEjercicio = Negocio.EstadoEjercicioNegocio.GetEstadoEjercicios();

            switch (estadoEjercicio)
            {
                case 1://pendientes
                    return View("PendientesAprobacion", datos);
                    break;
                case 3://desaprobados
                    return View("Desaprobados", datos);
                    break;
                default:
                    return View("Index", datos);
                    break;
            }
        }
        public ActionResult ObtenerEjercicioGrilla(int pageA = 1, string sortA = "Nombre", string sortDirA = "ASC", string nombreA = "", int usuarioIdA = -1, int cursoIdA = -1, int estadoEjercicioA = -1, int nivelEjercicioA = -1, bool global = true)
        {
            var datos = ObtenerEjercicioGrillaModel(pageA, sortA, sortDirA, nombreA, usuarioIdA, cursoIdA, estadoEjercicioA, nivelEjercicioA, global);
            return View(datos);
        }
        private EjercicioGrillaModel ObtenerEjercicioGrillaModel(int page, string sort, string sortDir, string nombre, int usuarioId, int cursoId, int estadoEjercicio, int nivelEjercicio, bool global)
        {
            //Pasar la cantidad por pagina a una constante mas copada.
            int cantidadPorPaginaTPC = 10;

            Usuario userLogueado = GetUsuarioLogueado();

            var numUsuarios = EjercicioNegocio.ContarCantidad(nombre,
                     usuarioId,
                     cursoId,
                     estadoEjercicio,
                     nivelEjercicio,
                     global,
                     userLogueado);

            sortDir = sortDir.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? sortDir : "asc";

            var validColumns = new[] { "EjercicioId", "Nombre", "Usuario", "Curso", "EstadoEjercicio", "NivelEjercicio", "Global" };

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
                     global,
                     userLogueado
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

        public ActionResult ObtenerEjercicioGrillaNotCurso(int pageNA = 1, string sortNA = "Nombre", string sortDirNA = "ASC", string nombreNA = "", int usuarioIdNA = -1, int cursoIdNA = -1, int estadoEjercicioNA = -1, int nivelEjercicioNA = -1, bool global = true)
        {
            var datos = ObtenerEjercicioGrillaModelNotCurso(pageNA, sortNA, sortDirNA, nombreNA, usuarioIdNA, cursoIdNA, estadoEjercicioNA, nivelEjercicioNA, global);
            return View(datos);
        }
        private EjercicioGrillaModel ObtenerEjercicioGrillaModelNotCurso(int page, string sort, string sortDir, string nombre, int usuarioId, int cursoId, int estadoEjercicio, int nivelEjercicio, bool global)
        {
            //Pasar la cantidad por pagina a una constante mas copada.
            int cantidadPorPaginaTPC = 10;

            Usuario userLogueado = GetUsuarioLogueado();

            var numUsuarios = EjercicioNegocio.ContarCantidadNotCurso(nombre,
                     usuarioId,
                     cursoId,
                     2, //Los aprobados
                     nivelEjercicio,
                     global,
                     userLogueado);

            sortDir = sortDir.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? sortDir : "asc";

            var validColumns = new[] { "EjercicioId", "Nombre", "Usuario", "Curso", "EstadoEjercicio", "NivelEjercicio", "Global" };

            if (!validColumns.Any(c => c.Equals(sort, StringComparison.CurrentCultureIgnoreCase)))
                sort = "Nombre";

            var ejers = EjercicioNegocio.ObtenerPaginaNotCurso(
                     page,
                     cantidadPorPaginaTPC,
                     sort + " " + sortDir,
                     nombre,
                     usuarioId,
                     cursoId,
                     2, //Los aprobados
                     nivelEjercicio,
                     global,
                     userLogueado
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
            //List<NivelEjercicio> listaNivelesEjercicio = new List<NivelEjercicio>();
            //listaNivelesEjercicio.AddRange(Negocio.NivelEjercicioNegocio.GetNiveles().ToList());

            List<short> listaNivelesEjercicio = new List<short>(new short[]{1,2,3,4,5,6,7,8,9,10});            
            ViewBag.NivelesEjercicio = listaNivelesEjercicio;
        }

        //
        // GET: /Ejercicio/Details/5

        public ActionResult Details(int id)
        {
            if (Request.IsAjaxRequest())
            {
                Ejercicio e = EjercicioNegocio.GetEjercicioById(id);
                ViewBag.EsDelete = false;
                return View(e);
            }
            else
            {
                throw new Exception("No se puede acceder a esta pagina de ese modo. Por favor use la pagina para acceder");
            }
        }

        //
        // GET: /Ejercicio/Upload

        [Authorize]
        public ActionResult Upload()
        {
            EjercicioUploadModel model = new EjercicioUploadModel();

            
            model.Error = false;
            model.MensajeError = string.Empty;

            return View("Upload", model);            
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                // extract only the fielname
                var fileName = Path.GetFileName(file.FileName);

                System.IO.MemoryStream memStream = new System.IO.MemoryStream();
                file.InputStream.CopyTo(memStream);

                try
                {
                    Ejercicio ejDeArchivo = EjercicioNegocio.ObtenerEjercicioDeArchivo(memStream);

                    return View("Create", ejDeArchivo);
                }
                catch (CargarEjercicioArchivoException ex)
                {
                    EjercicioUploadModel modelError = new EjercicioUploadModel();

                    modelError.Error = true;
                    modelError.MensajeError = ex.Message;

                    return View("Upload", modelError);
                }
                catch (Exception ex)
                {

                    throw;
                }               
            }
            else
            {
                EjercicioUploadModel modelError = new EjercicioUploadModel();

                modelError.Error = true;
                modelError.MensajeError = "No se subio ningun archivo o el archivo tenia 0 bytes";

                return View("Upload", modelError);
            }


            
        }
      

        [HttpPost]
        public ActionResult Create(Ejercicio ejercicio)
        {
            if (ModelState.IsValid)
            {

                //flanzani
                //Una vez que tengamos el usuarioId en sesion, lo ponemos aca. Mientras tanto, usamos 1.
                ejercicio.UsuarioId = GetUsuarioLogueado().UsuarioId;
                ejercicio.EstadoEjercicioId = 1; //lo coloco en pendiente
                
                EjercicioNegocio.Alta(ejercicio);
                return View("EjercicioCreado", ejercicio);
            }
            else
            {
                return View();
            }
        }
        
        //
        // GET: /Ejercicio/Edit/5 
        [Authorize]
        public ActionResult Edit(int id)
        {
            if (Request.IsAjaxRequest())
            {
                Initilization();
                Ejercicio e = EjercicioNegocio.GetEjercicioById(id);
                return View("Edit", e);
            }
            else
            {
                throw new Exception("No se puede acceder a esta pagina de ese modo. Por favor use la pagina para acceder");
            }
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
                Initilization();
                Ejercicio e = EjercicioNegocio.GetEjercicioById(ejercicio.EjercicioId);
                return View("Edit", e);
            }
        }

        //
        // GET: /Ejercicio/Delete/5
 
        public ActionResult Delete(int id)
        {
            if (Request.IsAjaxRequest())
            {
                Ejercicio e = EjercicioNegocio.GetEjercicioById(id);
                ViewBag.EsDelete = true;
                return View(e);
            }
            else
            {
                throw new Exception("No se puede acceder a esta pagina de ese modo. Por favor use la pagina para acceder");
            }
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
                return Content(Boolean.FalseString);
            }
        }


        [Authorize(Roles = "administrador, moderador")]
        public ActionResult PendientesAprobacion(int page = 1, string sort = "Nombre", string sortDir = "ASC",
             int usuarioId = -1, int cursoId = -1, string nombre = "", int nivelEjercicio = -1, bool global = false,
            bool conLayout = true, bool aplicarPermisos = true)
        {

            EstadoEjercicio estado = EstadoEjercicioNegocio.GetEstadoEjercicioByName("Pendiente");
            int estadoEjercicio = estado.EstadoEjercicioId;

            //Pasar la cantidad por pagina a una constante mas copada.


            var datos = ObtenerEjercicioGrillaModel(page, sort, sortDir, nombre, usuarioId, cursoId, estadoEjercicio, nivelEjercicio, global);
            datos.ConLayout = conLayout;
            datos.AplicarPermisos = aplicarPermisos;

            //ViewBag.NivelesEjercicio = Negocio.NivelEjercicioNegocio.GetNiveles();
            ViewBag.NivelesEjercicio = new List<short>(new short[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }); 

            return View("PendientesAprobacion", datos);

        }

        [Authorize(Roles = "administrador, moderador")]
        public ActionResult Desaprobados(int page = 1, string sort = "Nombre", string sortDir = "ASC",
             int usuarioId = -1, int cursoId = -1, string nombre = "", int nivelEjercicio = -1, bool global = false,
             bool conLayout = true, bool aplicarPermisos = true)
        {

            EstadoEjercicio estado = EstadoEjercicioNegocio.GetEstadoEjercicioByName("Desaprobado");
            int estadoEjercicio = estado.EstadoEjercicioId;

            //Pasar la cantidad por pagina a una constante mas copada.


            var datos = ObtenerEjercicioGrillaModel(page, sort, sortDir, nombre, usuarioId, cursoId, estadoEjercicio, nivelEjercicio, global);
            datos.ConLayout = conLayout;
            datos.AplicarPermisos = aplicarPermisos;

            //ViewBag.NivelesEjercicio = Negocio.NivelEjercicioNegocio.GetNiveles();
            ViewBag.NivelesEjercicio = new List<short>(new short[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }); 

            return View("Desaprobados", datos);

        }

        //
        // GET: /Curso/Details/5

        [Authorize]
        public ActionResult AsociarCursoEjercicio(int id, int Curso_page = 1, string Curso_sort = "Nombre", string Curso_sortDir = "ASC",
             int usuarioId = -1, int cursoId = -1, string nombreA = "",
             int nivelEjercicioA = -1, bool global = false,
             int NotCurso_page = 1, string NotCurso_sort = "Nombre", string NotCurso_sortDir = "ASC",
             string nombreNA = "",  int nivelEjercicioNA = -1)
        {
            
            if (CursoNegocio.ExisteCursoById(id))
            {

                ListEjercicioGrillaModel datos = new ListEjercicioGrillaModel();
                Usuario userLogueado = GetUsuarioLogueado();
                cursoId = id;

                Curso curso = CursoNegocio.GetCursoById(id);

                int idEjercicioAprobado = 2;
                //EjercicioGrillaModel ejAsociados = new EjercicioGrillaModel();
                //ejAsociados.Ejercicios = curso.Ejercicios.ToList();
                //ejAsociados.Cantidad = curso.Ejercicios.ToList().Count;
                //ejAsociados.CantidadPorPagina = 10;
                //ejAsociados.PaginaActual = Curso_page;

                datos.ListEjerciciosGrillaModel = new List<EjercicioGrillaModel>();
                datos.cursoId = cursoId;
                //datos.ListEjerciciosGrillaModel.Add(ejAsociados);
                datos.ListEjerciciosGrillaModel.Add(ObtenerEjercicioGrillaModel(Curso_page, Curso_sort, Curso_sortDir, nombreA, usuarioId, cursoId, idEjercicioAprobado, nivelEjercicioA, global));
                datos.ListEjerciciosGrillaModel.Add(ObtenerEjercicioGrillaModelNotCurso(NotCurso_page, NotCurso_sort, NotCurso_sortDir, nombreNA, usuarioId, cursoId, idEjercicioAprobado, nivelEjercicioNA, global));
                ViewBag.NivelesEjercicio = new List<short>(new short[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
                ViewBag.EstadosEjercicio = Negocio.EstadoEjercicioNegocio.GetEstadoEjercicios();
                return View(datos);
            }
            else
            {
                throw new Exception("El curso especificado no existe. Por favor utilice la pagina para acceder a la pagina");
            }
        }



        //
        // POST: /Curso/Edit/5

        /*[HttpPost]
        public ActionResult AsociarCursoEjercicio(Curso curso,Ejercicio ejercicio)
        {
            // TODO: Add update logic here
            if (ModelState.IsValid)
            {
                CursoNegocio.AsociarCursoEjercicio(curso,ejercicio);
                return Content(Boolean.TrueString);
            }
            else
            {
                return Content(Boolean.TrueString);
                //return View();
            }

        }
        */
   
        [Authorize(Roles = "administrador, moderador")]
        public ActionResult Moderar(int id)
        {
            if (Request.IsAjaxRequest())
            {
                ModerarEjercicioModel model = new ModerarEjercicioModel();
                model.Ejercicio = EjercicioNegocio.GetEjercicioById(id);
                model.Aceptado = false;
                model.MensajeModeracion = string.Empty;
                ViewBag.EsDelete = false;

                return View(model);
            }
            else
            {
                throw new Exception("No se puede acceder a esta pagina de ese modo. Por favor use la pagina para acceder");
            }
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

                ejercicioFunca.MensajeModeracion = new MensajeModeracion();
                //ejercicioFunca.MensajeModeracion.MensajeModeracionId = null;
                ejercicioFunca.MensajeModeracion.EjercicioId = ejercicio.EjercicioId;
                ejercicioFunca.MensajeModeracion.Mensaje = model.MensajeModeracion;

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
                EjercicioNegocio.ModificarEstado(ejercicioFunca);
            }
            catch (Exception)
            {
                return Content(Boolean.FalseString);
            }

            return Content(Boolean.TrueString);
        }
    }
}
