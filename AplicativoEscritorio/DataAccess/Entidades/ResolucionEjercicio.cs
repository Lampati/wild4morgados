using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AplicativoEscritorio.DataAccess.Interfases;
using AplicativoEscritorio.DataAccess.Enums;
using Utilidades.XML;
using Utilidades.Criptografia;
using AplicativoEscritorio.DataAccess.Excepciones;
using System.IO;
using System.Xml.Serialization;

namespace AplicativoEscritorio.DataAccess.Entidades
{
    public class ResolucionEjercicio : EntidadBase
    {
        public const string EXTENSION_RESOLUCION = @"gres";

        #region Atributos
        private Ejercicio ejercicio;        
        #endregion

        #region Propiedades
        public override string Extension
        {
            get { return EXTENSION_RESOLUCION; }
        }
        #endregion

        #region Constructores
        public ResolucionEjercicio() 
        {
            ejercicio = new Ejercicio();
        }

        public ResolucionEjercicio(Ejercicio ej)
        {
            ejercicio = ej;
        }
        #endregion

        #region IPersistible Members
        public override void ToXML(XMLCreator xml)
        {
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

            if (Object.Equals(this.representacionGrafica, null))
                this.representacionGrafica = new InterfazTextoGrafico.ProgramaViewModel();

            this.representacionGrafica.ToXML(xml);

            xml.AddElement();
            xml.SetTitle("EjercicioCorrespondiente");
            this.ejercicio.ToXML(xml);
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("RepresentacionGrafica");
            MemoryStream strm = new MemoryStream();
            XmlSerializer serializador = new XmlSerializer(this.representacionGrafica.GetType());
            serializador.Serialize(strm, this.representacionGrafica);
            xml.SetValue(new StreamReader(strm).ReadToEnd());
            xml.LevelUp();
            xml.AddElement();
            xml.SetTitle("HashResolucionEjercicio");
            xml.SetValue(this.Hash);
            xml.LevelUp();
        }


        public override void FromXML(XMLElement xmlElem)
        {
            if (Object.Equals(xmlElem, null))
                throw new NullReferenceException("El XML para la Resolución de Ejercicio se encuentra nulo.");

            xmlElem = xmlElem.FindFirst("ResolucionEjercicioProgramAr");

            if (Object.Equals(xmlElem, null))
                throw new NullReferenceException("El XML no contiene el tag <ResolucionEjercicioProgramAr>");

            this.ModificadoDesdeUltimoGuardado = bool.Parse(xmlElem.FindFirst("ModificadoDesdeUltimoGuardado").value);
            this.PathGuardadoActual = this.Enunciado = xmlElem.FindFirst("PathGuardadoActual").value;
            this.UltimoModoGuardado = (ModoVisual)int.Parse(xmlElem.FindFirst("UltimoModoGuardado").value);
            

            this.ejercicio = new Ejercicio();
            this.ejercicio.FromXML(xmlElem.FindFirst("EjercicioCorrespondiente"));

            if (UltimoModoGuardado == ModoVisual.Texto)
            {
                this.gargar = Utilidades.XML.XMLReader.Unescape(xmlElem.FindFirst("Gargar").value);
                this.RepresentacionGrafica = new InterfazTextoGrafico.ProgramaViewModel();
            }
            else
            {

                this.RepresentacionGrafica = new InterfazTextoGrafico.ProgramaViewModel();
                this.RepresentacionGrafica.FromXML(xmlElem.FindFirst("ProgramaViewModel"));
                this.gargar = string.Empty;
            }
        }

        #endregion        

        #region IPropiedadesEjercicios Members

       

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

        public override InterfazTextoGrafico.ProgramaViewModel RepresentacionGrafica
        {
            get { return this.representacionGrafica; }
            set
            {
                this.representacionGrafica = value;

            }
        }

        public override short NivelDificultad
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

       

        public override ModoEjercicio Modo
        {
            get { return this.ejercicio.Modo; }
            set { }
        }

        #endregion

        #region Object Members
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.ModificadoDesdeUltimoGuardado.ToString());
            sb.Append(this.PathGuardadoActual);
            sb.Append(this.Gargar);

            return sb.ToString();
        }
        #endregion
    }
}
