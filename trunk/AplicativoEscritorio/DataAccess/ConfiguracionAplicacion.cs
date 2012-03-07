using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using DataAccess.Interfases;

namespace DataAccess
{
    [XmlRootAttribute("Configuracion", Namespace = "", IsNullable = false)]
    public class ConfiguracionAplicacion 
    {
        private string directorioAbrirDefault;
        public string DirectorioAbrirDefault
        {
            get
            {
                return directorioAbrirDefault;
            }
            set
            {
                directorioAbrirDefault = value;
            }
        }

        private string directorioTemporal;
        public string DirectorioTemporal 
        {
            get
            {
                return directorioTemporal;
            }
            set
            {
                directorioTemporal = value;
            }
        }

        private string directorioEjerciciosDescargados;
        public string DirectorioEjerciciosDescargados
        {
            get
            {
                return directorioEjerciciosDescargados;
            }
            set
            {
                directorioEjerciciosDescargados = value;
            }
        }

        private string directorioEjerciciosCreados;
        public string DirectorioEjerciciosCreados
        {
            get
            {
                return directorioEjerciciosCreados;
            }
            set
            {
                directorioEjerciciosCreados = value;
            }
        }

        private string directorioResolucionesEjercicios;
        public string DirectorioResolucionesEjercicios
        {
            get
            {
                return directorioResolucionesEjercicios;
            }
            set
            {
                directorioResolucionesEjercicios = value;
            }
        }


        public ConfiguracionAplicacion()
        {
            
        }

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
            }
            else
            {
                CargarDefaults();
            }
        }

     

        public void Guardar(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ConfiguracionAplicacion));
            TextWriter textWriter = new StreamWriter(path,false);
            serializer.Serialize(textWriter, this);
            textWriter.Close(); 
        }

        private void CargarDefaults()
        {

        }

        
    }
}
