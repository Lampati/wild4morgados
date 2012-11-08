using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AplicativoEscritorio.DataAccess.Interfases;
using Utilidades.Criptografia;
using System.IO;
using Utilidades.XML;
using AplicativoEscritorio.DataAccess.Enums;

namespace AplicativoEscritorio.DataAccess.Entidades
{
    public abstract class EntidadBase : IPersistible, IPropiedadesEjercicios
    {
        protected ModoVisual ultimoModoGuardado;
        protected string gargar;
        protected InterfazTextoGrafico.ProgramaViewModel representacionGrafica;
        protected bool modificadoDesdeUltimoGuardado;
        protected string pathGuardadoActual;
        protected string nombre;
        protected bool compilacionCorrecta;
        protected bool ejecucionCorrecta;

        #region IPersistible Members

        public virtual void Guardar(string path)
        {
            XMLCreator xml = new XMLCreator();
            this.ToXML(xml);
            string xmlEncriptado = Crypto.Encriptar(xml.Get());           

            File.WriteAllText(path, xmlEncriptado);
        }

        public virtual void Abrir(MemoryStream stream)
        {
            string decodedString = Encoding.UTF8.GetString(stream.ToArray());
            this.Abrir(decodedString);
        }

        public virtual void Abrir(FileInfo fi)
        {
            this.Abrir(File.ReadAllText(fi.FullName));
        }

        public virtual void Abrir(string textoEncriptado)
        {
            string xmlEncriptado = textoEncriptado;
            string xmlDesencriptado;
            try
            {
                xmlDesencriptado = Crypto.Desencriptar(xmlEncriptado);
            }
            catch (Exception ex)
            {
                throw new Excepciones.ExcepcionCriptografia("Error al abrir el archivo", ex);
            }

            XMLReader xmlReader = new XMLReader();
            XMLElement xmlElem = xmlReader.Read(xmlDesencriptado);

            this.FromXML(xmlElem);
        }

        public abstract void ToXML(Utilidades.XML.XMLCreator xml);

        public abstract void FromXML(Utilidades.XML.XMLElement xmlElem);

        public string Hash
        {
            get { return Crypto.ComputarHash(this.ToString()); }
        }
        #endregion

        #region IPropiedadesEjercicios Members

        public bool CompilacionCorrecta
        {
            get
            {
                return compilacionCorrecta;
            }
            set
            {
                compilacionCorrecta = value;
            }
        }

        public bool EjecucionCorrecta
        {
            get
            {
                return ejecucionCorrecta;
            }
            set
            {
                ejecucionCorrecta = value;
            }
        }

        public bool ModificadoDesdeUltimoGuardado
        {
            get
            {
                return modificadoDesdeUltimoGuardado;
            }
            set
            {
                modificadoDesdeUltimoGuardado = value;
            }
        }

        public string PathGuardadoActual
        {
            get { return this.pathGuardadoActual; }
            set
            {
                string path = value;
                if (!path.ToLower().EndsWith(string.Format(".{0}", this.Extension)))
                {
                    path = string.Format("{0}.{1}", path, this.Extension);
                }                
                this.pathGuardadoActual = path;

                string[] auxPath = path.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                nombre = auxPath[auxPath.Length - 1];
            }
        }

        public  string Nombre
        {
            get { return this.nombre; }
        }

        public abstract string Extension { get; }

        public  ModoVisual UltimoModoGuardado
        {
            get { return this.ultimoModoGuardado; }
            set { this.ultimoModoGuardado = value; }
        }



        

        public abstract string Enunciado
        {
            get;
            set;
        }

        public abstract InterfazTextoGrafico.ProgramaViewModel RepresentacionGrafica
        {
            get;
            set;
        }


        public abstract string Gargar
        {
            get;
            set;
        }

        public abstract short NivelDificultad
        {
            get;
            set;
        }

        public abstract string SolucionGargar
        {
            get;
        }

        public abstract string SolucionTexto
        {
            get;
            set;
        }

       

        public abstract Enums.ModoEjercicio Modo
        {
            get;
            set;
        }

        public abstract List<TestPrueba> TestsPrueba
        {
            get;
            
        }

        #endregion
    }
}
