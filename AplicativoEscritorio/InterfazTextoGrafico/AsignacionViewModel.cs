using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfazTextoGrafico
{
    public class AsignacionViewModel : ActividadViewModelBase
    {

        public string LadoIzquierdo { get; set; }
        public string LadoDerecho { get; set; }
        
        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();

                strBldr.AppendFormat("{0} := {1};",
                    LadoIzquierdo,
                    LadoDerecho).AppendLine();

                return strBldr.ToString();
            }
        }



        public override void ToXML(Utilidades.XML.XMLCreator xml)
        {
            xml.AddElement();
            xml.SetTitle("Asignacion");

            xml.AddElement();
            xml.SetTitle("LadoIzquierdo");
            xml.SetValue(Utilidades.XML.XMLReader.Escape(LadoIzquierdo));
            xml.LevelUp();

            

            xml.AddElement();
            xml.SetTitle("LadoDerecho");
            xml.SetValue(Utilidades.XML.XMLReader.Escape(LadoDerecho));
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("NombreTipo");
            xml.SetValue("AsignacionViewModel");
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
