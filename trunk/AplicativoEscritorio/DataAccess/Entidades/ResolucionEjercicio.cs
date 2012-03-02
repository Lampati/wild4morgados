using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Interfases;
using DataAccess.Enums;
using Globales.Enums;

namespace DataAccess.Entidades
{
    public class ResolucionEjercicio : EntidadBase
    {

        #region Atributos
        
        
        #endregion




        #region IPersistible Members



        public override void Guardar(string path)
        {
            
        }

        public override void Abrir(string path)
        {
            
        }

        #endregion

        private Ejercicio ejercicio;

        #region IPropiedadesEjercicios Members

        public override bool ModificadoDesdeUltimoGuardado
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

        public override string PathGuardadoActual
        {
            get { return this.pathGuardadoActual; }
            set { this.pathGuardadoActual = value; }
        }

        public override string Enunciado
        {
            get
            {
                return ejercicio.Enunciado;
            }
            set
            {

            }
        }

        public override string Gargar
        {
            get
            {
                return gargar;
            }
            set
            {
                gargar = value;
            }
        }

        public override Enums.NivelDificultad NivelDificultad
        {
            get
            {
                return ejercicio.NivelDificultad;
            }
            set
            {

            }
            
        }

        public override string SolucionGargar
        {
            get
            {
                return ejercicio.SolucionGargar;
            }
            set
            {
            }

            
        }

        public override string SolucionTexto
        {
            get
            {
                return ejercicio.SolucionTexto;
            }
            set
            {
            }
        }

        public override List<TestPrueba> TestsPrueba
        {
            get { return this.ejercicio.TestsPrueba; }
            
        }

        public override ModoVisual UltimoModoGuardado
        {
            get { return this.ultimoModoGuardado; }
            set { this.ultimoModoGuardado = value; }
        }

        public override ModoEjercicio Modo
        {
            get { return this.ejercicio.Modo; }
            set { }
        }

        #endregion
    }
}
