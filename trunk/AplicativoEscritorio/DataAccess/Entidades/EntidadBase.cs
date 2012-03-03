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

        protected abstract string ToXML();

        protected abstract void FromXML(string plainXml);

        protected abstract string Hash { get; }

        #endregion

        #region IPropiedadesEjercicios Members

        public abstract bool ModificadoDesdeUltimoGuardado
        {
            get;
            set;
        }

        public abstract string Enunciado
        {
            get;
            set;
        }

        public abstract string PathGuardadoActual
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
            set;
        }

        public abstract string SolucionTexto
        {
            get;
            set;
        }

        public abstract ModoVisual UltimoModoGuardado
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
