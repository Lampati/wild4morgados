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


        #region IPersistible Members

        public void Guardar(string path)
        {
            throw new NotImplementedException();
        }

        public void Abrir(string path)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IPropiedadesEjercicios Members

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
