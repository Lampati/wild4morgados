using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Interfases;
using Globales.Enums;
using Utilidades.Criptografia;
using System.IO;
using Utilidades.XML;

namespace DataAccess.Entidades
{
    public abstract class EntidadBase : IPersistible, IPropiedadesEjercicios
    {
        protected ModoVisual ultimoModoGuardado;
        protected string gargar;
        protected bool modificadoDesdeUltimoGuardado;
        protected string pathGuardadoActual;
        protected string nombre;

        #region IPersistible Members

        public virtual void Guardar(string path)
        {
            XMLCreator xml = new XMLCreator();
            this.ToXML(xml);
            string xmlEncriptado = Crypto.Encriptar(xml.Get());           

            File.WriteAllText(path, xmlEncriptado);
        }

        public virtual void Abrir(string path)
        {
            string xmlEncriptado = File.ReadAllText(path);
            string xmlDesencriptado = Crypto.Desencriptar(xmlEncriptado);

            XMLReader xmlReader = new XMLReader();
            XMLElement xmlElem = xmlReader.Read(xmlDesencriptado);

            this.FromXML(xmlElem);
        }

        public abstract void ToXML(Utilidades.XML.XMLCreator xml);

        public abstract void FromXML(Utilidades.XML.XMLElement xmlElem);

        protected string Hash
        {
            get { return Crypto.ComputarHash(this.ToString()); }
        }
        #endregion

        #region IPropiedadesEjercicios Members
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
