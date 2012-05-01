using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using AplicativoEscritorio.DataAccess.Interfases;
using Utilidades.XML;

namespace DataAccess
{
    [XmlRootAttribute("Configuracion", Namespace = "", IsNullable = false)]
    public class ConfiguracionAplicacion
    {
        #region Atributos
        private static string directorioAbrirDefault;
        private static string directorioTemporal;
        private static string directorioEjerciciosDescargados;
        private static string directorioEjerciciosCreados;
        private static string directorioResolucionesEjercicios;
        private static string urlsDescargaEjercicios;
        #endregion

        #region Propiedades
        public static string UrlsDescargaEjercicios
        {
            get { return urlsDescargaEjercicios; }
            set { urlsDescargaEjercicios = value; }
        }

        public static string DirectorioAbrirDefault
        {
            get { return directorioAbrirDefault; }
            set { directorioAbrirDefault = value; }
        }

        public static string DirectorioTemporal 
        {
            get { return directorioTemporal; }
            set { directorioTemporal = value; }
        }

        public static string DirectorioEjerciciosDescargados
        {
            get { return directorioEjerciciosDescargados; }
            set { directorioEjerciciosDescargados = value; }
        }

        public static string DirectorioEjerciciosCreados
        {
            get { return directorioEjerciciosCreados; }
            set { directorioEjerciciosCreados = value; }
        }

        public static string DirectorioResolucionesEjercicios
        {
            get { return directorioResolucionesEjercicios; }
            set { directorioResolucionesEjercicios = value; }
        }
        #endregion

        #region Métodos
     

        public static void Abrir(string pathCompleto)
        {           
            if (File.Exists(pathCompleto))
            {
                XMLReader xmlReader = new XMLReader();
                XMLElement xmlElem = xmlReader.Read(File.ReadAllText(pathCompleto));

                xmlElem = xmlElem.FindFirst("Configuracion");
                if (!Object.Equals(xmlElem, null))
                {
                    if (!Object.Equals(xmlElem.FindFirst("DirectorioEjerciciosCreados"), null))
                        DirectorioEjerciciosCreados = xmlElem.FindFirst("DirectorioEjerciciosCreados").value;
                    if (!Object.Equals(xmlElem.FindFirst("DirectorioEjerciciosDescargados"), null))
                        DirectorioEjerciciosDescargados = xmlElem.FindFirst("DirectorioEjerciciosDescargados").value;
                    if (!Object.Equals(xmlElem.FindFirst("DirectorioResolucionesEjercicios"), null))
                        DirectorioResolucionesEjercicios = xmlElem.FindFirst("DirectorioResolucionesEjercicios").value;
                    if (!Object.Equals(xmlElem.FindFirst("DirectorioTemporal"), null))
                        DirectorioTemporal = xmlElem.FindFirst("DirectorioTemporal").value;
                    if (!Object.Equals(xmlElem.FindFirst("DirectorioAbrirDefault"), null))
                        DirectorioAbrirDefault = xmlElem.FindFirst("DirectorioAbrirDefault").value;
                    if (!Object.Equals(xmlElem.FindFirst("UrlsDescargaEjercicios"), null))
                        UrlsDescargaEjercicios = xmlElem.FindFirst("UrlsDescargaEjercicios").value;
                }
                else
                    CargarDefaults();
            }
            else
            {
                CargarDefaults();
            }
        }

       

        public static void Guardar(string path)
        {
            XMLCreator xml = new XMLCreator();
            xml.AddElement();
            xml.SetTitle("Configuracion");
            xml.AddElement();
            xml.SetTitle("UrlsDescargaEjercicios");
            xml.SetValue(UrlsDescargaEjercicios);
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("DirectorioAbrirDefault");
            xml.SetValue(DirectorioAbrirDefault);
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("DirectorioTemporal");
            xml.SetValue(DirectorioTemporal);
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("DirectorioEjerciciosDescargados");
            xml.SetValue(DirectorioEjerciciosDescargados);
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("DirectorioEjerciciosCreados");
            xml.SetValue(DirectorioEjerciciosCreados);
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("DirectorioResolucionesEjercicios");
            xml.SetValue(DirectorioResolucionesEjercicios);
            xml.LevelUp();
            xml.LevelUp();

            File.WriteAllText(path, xml.Get());
        }

        private static void CargarDefaults()
        {
            string misDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string pathProgramAR = Path.Combine(misDocs, "Program.AR");
            string path = String.Empty;

            try
            {
                if (!Directory.Exists(pathProgramAR))
                    Directory.CreateDirectory(pathProgramAR);

                path = Path.Combine(pathProgramAR, "Ejercicios Creados");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                DirectorioEjerciciosCreados = path;

                path = Path.Combine(pathProgramAR, "Ejercicios Descargados");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                DirectorioEjerciciosDescargados = path;

                path = Path.Combine(pathProgramAR, "Resolucion Ejercicios");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                DirectorioResolucionesEjercicios = path;

                path = Path.Combine(pathProgramAR, "Temp");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                DirectorioTemporal = path;

                DirectorioAbrirDefault = pathProgramAR;
            }
            catch (Exception e)
            {
                throw new Excepciones.ExcepcionCreacionDirectorios("Error al crear los directorios por defecto", e);
            }

            UrlsDescargaEjercicios = "http://www.program-ar.com.ar:8080/Ejercicios";
        }
        #endregion
    }
}
