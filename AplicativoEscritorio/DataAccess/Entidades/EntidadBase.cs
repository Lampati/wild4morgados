using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Interfases;
using Globales.Enums;

namespace DataAccess.Entidades
{
    public abstract class EntidadBase : IPersistible, IPropiedadesEjercicios
    {
        protected ModoVisual ultimoModoGuardado;
        protected string gargar;
        protected bool modificadoDesdeUltimoGuardado;
        protected string pathGuardadoActual;

        #region IPersistible Members

        public abstract void Guardar(string path);    

        public abstract void Abrir(string path);  

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
