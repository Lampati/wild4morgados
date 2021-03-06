﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfazTextoGrafico
{
    public class AsignacionViewModel : ActividadViewModelBase
    {

        public string LadoIzquierdo { get; set; }
        public string LadoDerecho { get; set; }

        public AsignacionViewModel(long id)
            : base(id)
        {

        }

        public AsignacionViewModel()
            : base()
        {

        }
       
        public override string  NombreActividad
        {
            get { return "Asignacion"; }
        }

        public override string Gargar
        {
            // flanzani 9/11/2012
            // IDC_APP_3
            // Cambiar el := por =
            // Cambio el componenteLexico por el Igual, ya que ahora es el que indica asignacion y modifico su lexema
            get
            {
                StringBuilder strBldr = new StringBuilder();

                strBldr.AppendFormat("{0} = {1} ;",
                    LadoIzquierdo,
                    LadoDerecho).AppendLine();

                return strBldr.ToString();
            }
        }

        public override void CalcularLineasYAsignarContextoAHijos(int linea, string nombreContexto)
        {
            lineaComienzo = linea;

            int cantLineas = Utilidades.StringUtils.CantidadDeLineas(Gargar) - 1;

            contexto = nombreContexto;

            lineaFinal = lineaComienzo + cantLineas;
        }

        public override string DescripcionLineas
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();

                strBldr.AppendLine(string.Format("{0} - {1} a {2}",
                    this.GetType().ToString(),
                    this.lineaComienzo,
                    this.lineaFinal
                ));

                return strBldr.ToString();
            }
        }


        public override ActividadViewModelBase EncontrarActividadPorLinea(int lineaABuscar)
        {
            if (this.lineaComienzo == lineaABuscar)
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        public override void ToXML(Utilidades.XML.XMLCreator xml)
        {
            xml.AddElement();
            xml.SetTitle("Asignacion");

            xml.AddElement();
            xml.SetTitle("NombreTipo");
            xml.SetValue("AsignacionViewModel");
            xml.LevelUp();    

            xml.AddElement();
            xml.SetTitle("LadoIzquierdo");
            xml.SetValue(Utilidades.XML.XMLReader.Escape(LadoIzquierdo));
            xml.LevelUp();

            

            xml.AddElement();
            xml.SetTitle("LadoDerecho");
            xml.SetValue(Utilidades.XML.XMLReader.Escape(LadoDerecho));
            xml.LevelUp();

                   

            xml.LevelUp();
        }

        public override void FromXML(Utilidades.XML.XMLElement xmlElem)
        {

            this.LadoIzquierdo = Utilidades.XML.XMLReader.Unescape(xmlElem.FindFirst("LadoIzquierdo").value);
            this.LadoDerecho = Utilidades.XML.XMLReader.Unescape(xmlElem.FindFirst("LadoDerecho").value);
            
        }
    

    }
}
