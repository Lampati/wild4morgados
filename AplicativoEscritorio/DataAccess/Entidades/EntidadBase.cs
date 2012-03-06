using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Interfases;
using Globales.Enums;
using Utilidades.Criptografia;
using System.IO;

namespace DataAccess.Entidades
{
    public abstract class EntidadBase : IPersistible, IPropiedadesEjercicios
    {
        protected ModoVisual ultimoModoGuardado;
        protected string gargar;
        protected bool modificadoDesdeUltimoGuardado;
        protected string pathGuardadoActual;
        protected string nombre;
        protected string extension;

        #region IPersistible Members

        public virtual void Guardar(string path)
        {
            string xml = this.ToXML();
            string xmlEncriptado = Crypto.Encriptar(xml);

           

            File.WriteAllText(path, xmlEncriptado);
        }

        public virtual void Abrir(string path)
        {
            string xmlEncriptado = File.ReadAllText(path);
            string xmlDesencriptado = Crypto.Desencriptar(xmlEncriptado);

            this.FromXML(xmlDesencriptado);
        }

        public abstract string ToXML();

        public abstract void FromXML(string plainXml);

        protected abstract string Hash { get; }

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
                if (!path.ToLower().EndsWith(string.Format(".{0}", extension)))
                {
                    path = string.Format("{0}.{1}", path, extension);
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

        public  string Extension
        {
            get { return this.extension; }
            set { this.extension = value; }
        }

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

        public abstract Enums.NivelDificultad NivelDificultad
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
