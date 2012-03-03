using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Interfases;
using DataAccess.Enums;
using Globales.Enums;
using Utilidades.XML;
using Utilidades.Criptografia;

namespace DataAccess.Entidades
{
    public class ResolucionEjercicio : EntidadBase
    {

        #region Atributos
        private Ejercicio ejercicio;        
        #endregion




        #region IPersistible Members
        protected override string Hash
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(this.ModificadoDesdeUltimoGuardado.ToString());
                sb.Append(this.PathGuardadoActual);
                sb.Append(this.Gargar);

                return Crypto.ComputarHash(sb.ToString());
            }
        }

        protected override string ToXML()
        {
            XMLCreator xml = new XMLCreator();
            xml.AddElement();
            xml.SetTitle("ResolucionEjercicioProgramAr");
            xml.AddElement();
            xml.SetTitle("ModificadoDesdeUltimoGuardado");
            xml.SetValue(this.ModificadoDesdeUltimoGuardado.ToString());
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("PathGuardadoActual");
            xml.SetValue(this.PathGuardadoActual);
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("Gargar");
            xml.SetValue(this.Gargar);
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("HashResolucionEjercicio");
            xml.SetValue(this.Hash);

            return xml.Get();
        }


        protected override void FromXML(string plainXml)
        {
            XMLReader xmlReader = new XMLReader();
            XMLElement xmlElem = xmlReader.Read(plainXml);
            xmlElem = xmlElem.FindFirst("ResolucionEjercicioProgramAr");

            this.ModificadoDesdeUltimoGuardado = bool.Parse(xmlElem.FindFirst("ModificadoDesdeUltimoGuardado").value);
            this.PathGuardadoActual = this.Enunciado = xmlElem.FindFirst("PathGuardadoActual").value;
            this.Gargar = xmlElem.FindFirst("Gargar").value;
        }

        #endregion

        

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
