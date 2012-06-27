﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfazTextoGrafico
{
    public class LeerViewModel : ActividadViewModelBase
    {
        public string Parametro { get; set; }

        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();

                strBldr.AppendFormat("leer ( {0} );", this.Parametro).AppendLine();

                return strBldr.ToString();
            }
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
            xml.SetTitle("Leer");

            xml.AddElement();
            xml.SetTitle("Parametro");
            xml.SetValue(Utilidades.XML.XMLReader.Escape(Parametro));
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("NombreTipo");
            xml.SetValue("LeerViewModel");
            xml.LevelUp();            


            xml.LevelUp();
        }

        public override void CalcularLineas(int linea)
        {
            lineaComienzo = linea;

            int cantLineas = Utilidades.StringUtils.CantidadDeLineas(Gargar) - 1;

            lineaFinal = lineaComienzo + cantLineas;
        }

        public override void FromXML(Utilidades.XML.XMLElement xmlElem)
        {

            this.Parametro = Utilidades.XML.XMLReader.Unescape(xmlElem.FindFirst("Parametro").value);
            
        }
    }
}
