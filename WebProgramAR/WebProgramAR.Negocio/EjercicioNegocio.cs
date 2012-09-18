using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using WebProgramAR.DataAccess;
using System.IO;
using Utilidades.XML;
using Utilidades.Criptografia;
using System.Web;

namespace WebProgramAR.Negocio
{
    public class EjercicioNegocio
    {
        public static Ejercicio GetEjercicioById(int id)
        {
            return EjercicioDA.GetEjercicioById(id);
        }
        public static IEnumerable<Ejercicio> GetEjerciciosByCursoByUsuarioByNivelByEstado(int usuarioId, int cursoId, int nivelEjercicio,int estadoEjercicio)
        {
            return EjercicioDA.GetEjerciciosByCursoByUsuarioByNivelByEstado(usuarioId, cursoId, estadoEjercicio, nivelEjercicio);
        }
        public static IEnumerable<Ejercicio> GetEjercicioNotUsuario(int usuarioId, int cursoId, int nivelEjercicio, int estadoEjercicio, Usuario userLogueado)
        {
            return EjercicioDA.GetEjercicioNotUsuario(usuarioId, cursoId, estadoEjercicio, nivelEjercicio, userLogueado);
        }
        //public static IEnumerable<Ejercicio> GetEjerciciosNotCurso(string nombre, int usuarioId, int cursoId, int nivelEjercicio, int estadoEjercicio, bool global, Usuario userLogueado)
        //{
        //    return EjercicioDA.GetEjerciciosNotCurso(nombre,usuarioId, cursoId, estadoEjercicio, nivelEjercicio,global, userLogueado);
        //}

        public static int Alta(Ejercicio Ejercicio)
        {
            
            Ejercicio.FechaAlta = DateTime.Now;
            return EjercicioDA.Alta(Ejercicio);
        }

        public static void Modificar(Ejercicio Ejercicio)
        {
            EjercicioDA.Modificar(Ejercicio);
        }

        public static void Eliminar(int id)
        {

            EjercicioDA.Eliminar(id);
        }

        public static int ContarCantidad(string nombre, int usuarioId, int cursoId, int estadoEjercicio, int nivelEjercicio, bool global, Usuario userLogueado)
        {
            return EjercicioDA.ContarCantidad(nombre, usuarioId, cursoId, estadoEjercicio, nivelEjercicio, global,userLogueado);
        }
        public static int ContarCantidadNotCurso(string nombre, int usuarioId, int cursoId, int estadoEjercicio, int nivelEjercicio, bool global, Usuario userLogueado)
        {
            return EjercicioDA.ContarCantidadNotCurso(nombre, usuarioId, cursoId, estadoEjercicio, nivelEjercicio, global, userLogueado);
        }
        public static IEnumerable<Ejercicio> ObtenerPagina(int paginaActual, int personasPorPagina, string sortColumns, string nombre, int usuarioId, int cursoId, int estadoEjercicio, int nivelEjercicio, bool global, Usuario userLogueado)
        {
            return EjercicioDA.ObtenerPagina(paginaActual, personasPorPagina, sortColumns, nombre, usuarioId, cursoId, estadoEjercicio, nivelEjercicio, global,userLogueado).ToList();
        }
        public static IEnumerable<Ejercicio> ObtenerPaginaNotCurso(int paginaActual, int personasPorPagina, string sortColumns, string nombre, int usuarioId, int cursoId, int estadoEjercicio, int nivelEjercicio, bool global, Usuario userLogueado)
        {
            return EjercicioDA.ObtenerPaginaNotCurso(paginaActual, personasPorPagina, sortColumns, nombre, usuarioId, cursoId, estadoEjercicio, nivelEjercicio, global, userLogueado).ToList();
        }


        public static Ejercicio GetEjercicioByIdOnlyUser(int id)
        {
            return EjercicioDA.GetEjercicioByIdOnlyUser(id);
        }

        public static void ModificarEstado(Ejercicio ejercicio)
        {
            EjercicioDA.ModificarEstado(ejercicio);
        }


        public static Ejercicio ObtenerEjercicioDeArchivo(MemoryStream memStreamArchEj)
        {
            AplicativoEscritorio.DataAccess.Entidades.Ejercicio ej;
            try
            {
                ej = new AplicativoEscritorio.DataAccess.Entidades.Ejercicio();
                ej.Abrir(memStreamArchEj);
            }         
            catch (Exception ex)
            {                
                throw new CargarEjercicioArchivoException("No se pudo cargar el ejercicio. El contenido del archivo no corresponde con lo que deberia contener un ejercicio.");
            }
            

            if (ej.EsValidoSubirWeb)
            {
                
                Ejercicio ejRetorno = new Ejercicio();

                
                ejRetorno.Nombre = ej.Nombre;
                ejRetorno.SolucionTexto = ej.SolucionTexto;
                ejRetorno.Enunciado = ej.Enunciado;
                ejRetorno.NivelEjercicio = ej.NivelDificultad;
                ejRetorno.SolucionGarGar = ej.SolucionGargar;
                XMLCreator xml = new XMLCreator();
                ej.ToXML(xml);
                ejRetorno.XmlDelEjercicio = HttpUtility.HtmlEncode(xml.Get()); 

                return ejRetorno;
            }
            else
            {
                throw new CargarEjercicioArchivoException("El ejercicio no estaba valido para subir a web. Por favor guarde el ejercicio en formato para subir a web, siga los pasos en el aplicativo, y luego vuelva a subirlo");
            }
        }

        public static bool ExisteEjercicioById(int ejercicioId)
        {
            return EjercicioDA.ExisteEjercicioById(ejercicioId);
        }

        public static void ActualizarXml(int id, string xml)
        {
            XMLCreator xmlCreator = new XMLCreator();
            AplicativoEscritorio.DataAccess.Entidades.Ejercicio ej = new AplicativoEscritorio.DataAccess.Entidades.Ejercicio();
            XMLReader xmlReader = new XMLReader();
            XMLElement xmlElem = xmlReader.Read(xml);

            ej.FromXML(xmlElem);
            ej.EjercicioId = id;
            ej.ToXML(xmlCreator);
            
            string xmlEncriptado = Crypto.Encriptar(xmlCreator.Get());

            EjercicioDA.ModificarXml(id, xmlEncriptado);
        }
    }
}
