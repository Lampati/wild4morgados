using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfazTextoGrafico
{
    public class DeclaracionVariableViewModel  : ActividadViewModelBase
    {
        public string Nombre { get; set; }
        public InterfazTextoGrafico.Enums.TipoDato Tipo { get; set; }

        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();

                strBldr.AppendFormat("var {0} : {1};", Nombre, Tipo.ToString());

                return strBldr.ToString();
            }
        }

        public override void ToXML(Utilidades.XML.XMLCreator xml)
        {
            xml.AddElement();
            xml.SetTitle("DeclaracionVariable");

            xml.AddElement();
            xml.SetTitle("Nombre");
            xml.SetValue(Utilidades.XML.XMLReader.Escape(Nombre));
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("Tipo");
            xml.SetValue(((int)Tipo).ToString());
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("NombreTipo");
            xml.SetValue("DeclaracionVariableViewModel");
            xml.LevelUp();            


            xml.LevelUp();
        }

        public override void FromXML(Utilidades.XML.XMLElement xmlElem)
        {

            this.Nombre = Utilidades.XML.XMLReader.Unescape(xmlElem.FindFirst("Nombre").value);
            this.Tipo = (InterfazTextoGrafico.Enums.TipoDato)int.Parse(xmlElem.FindFirst("Tipo").value);
            
        }
    }
}
