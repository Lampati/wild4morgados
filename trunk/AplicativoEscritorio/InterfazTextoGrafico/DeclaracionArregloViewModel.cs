﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfazTextoGrafico
{
    public class DeclaracionArregloViewModel : ActividadViewModelBase
    {
        public string Nombre { get; set; }
        public InterfazTextoGrafico.Enums.TipoDato Tipo { get; set; }
        public string Tope { get; set; }

        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();

                strBldr.AppendFormat("var {0} : arreglo [1..{1}] de {2};", Nombre, Tope ,Tipo.ToString());

                return strBldr.ToString();
            }
        }

        public override void ToXML(Utilidades.XML.XMLCreator xml)
        {
            xml.AddElement();
            xml.SetTitle("DeclaracionArreglo");

            xml.AddElement();
            xml.SetTitle("Nombre");
            xml.SetValue(Utilidades.XML.XMLReader.Escape(Nombre));
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("Tipo");
            xml.SetValue(((int)Tipo).ToString());
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("Tope");
            xml.SetValue(Utilidades.XML.XMLReader.Escape(Tope));
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("NombreTipo");
            xml.SetValue("DeclaracionArregloViewModel");
            xml.LevelUp();            

            xml.LevelUp();
        }

        public override void FromXML(Utilidades.XML.XMLElement xmlElem)
        {

            this.Nombre = Utilidades.XML.XMLReader.Unescape(xmlElem.FindFirst("Nombre").value);
            this.Tipo = (InterfazTextoGrafico.Enums.TipoDato)int.Parse(xmlElem.FindFirst("Tipo").value);
            this.Tope = Utilidades.XML.XMLReader.Unescape(xmlElem.FindFirst("Tope").value);
            
        }
    }
}
