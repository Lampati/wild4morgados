using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using AplicativoEscritorio.DataAccess.Interfases;

namespace AplicativoEscritorio.DataAccess
{
    [XmlRootAttribute("Configuracion", Namespace = "", IsNullable = false)]
    public class ConfiguracionAplicacion
    {
        #region Atributos
        private string directorioAbrirDefault;
        private string directorioTemporal;
        private string directorioEjerciciosDescargados;
        private string directorioEjerciciosCreados;
        private string directorioResolucionesEjercicios;
        private string urlsDescargaEjercicios;
        #endregion

        #region Propiedades
        public string UrlsDescargaEjercicios
        {
            get { return urlsDescargaEjercicios; }
            set { urlsDescargaEjercicios = value; }
        }

        public string DirectorioAbrirDefault
        {
            get { return directorioAbrirDefault; }
            set { directorioAbrirDefault = value; }
        }
        
        public string DirectorioTemporal 
        {
            get { return directorioTemporal; }
            set { directorioTemporal = value; }
        }
        
        public string DirectorioEjerciciosDescargados
        {
            get { return directorioEjerciciosDescargados; }
            set { directorioEjerciciosDescargados = value; }
        }
        
        public string DirectorioEjerciciosCreados
        {
            get { return directorioEjerciciosCreados; }
            set { directorioEjerciciosCreados = value; }
        }
        
        public string DirectorioResolucionesEjercicios
        {
            get { return directorioResolucionesEjercicios; }
            set { directorioResolucionesEjercicios = value; }
        }
        #endregion

        #region Constructores
        public ConfiguracionAplicacion() { }
        #endregion

        #region Métodos
        public void Abrir(string pathCompleto)
        {           
            if (File.Exists(pathCompleto))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(ConfiguracionAplicacion));
                TextReader textReader = new StreamReader(pathCompleto);
                ConfiguracionAplicacion config;
                config = (ConfiguracionAplicacion)deserializer.Deserialize(textReader);
                textReader.Close();

                this.directorioEjerciciosCreados = config.DirectorioEjerciciosCreados;
                this.directorioEjerciciosDescargados = config.DirectorioEjerciciosDescargados;
                this.directorioResolucionesEjercicios = config.DirectorioResolucionesEjercicios;
                this.directorioTemporal = config.DirectorioTemporal;
                this.directorioAbrirDefault = config.DirectorioAbrirDefault;
                this.urlsDescargaEjercicios = config.UrlsDescargaEjercicios;
            }
            else
            {
                CargarDefaults();
            }
        }

        public void Guardar()
        {
            string path = Path.Combine(Globales.ConstantesGlobales.PathEjecucionAplicacion,
                                         Globales.ConstantesGlobales.NOMBRE_ARCH_CONFIG_APLICACION);
            XmlSerializer serializer = new XmlSerializer(typeof(ConfiguracionAplicacion));
            TextWriter textWriter = new StreamWriter(path,false);
            serializer.Serialize(textWriter, this);
            textWriter.Close(); 
        }

        private void CargarDefaults() { }
        #endregion
    }
}
