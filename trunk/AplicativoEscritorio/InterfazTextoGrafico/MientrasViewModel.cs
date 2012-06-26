using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilidades.XML;
using InterfazTextoGrafico.Auxiliares;

namespace InterfazTextoGrafico
{
    public class MientrasViewModel : ActividadViewModelBase
    {
        public SecuenciaViewModel Cuerpo { get; set; }
        public string Condicion { get; set; }

        public override string Gargar
        {
            get 
            {
                StringBuilder strBldr = new StringBuilder();

                strBldr.AppendFormat("mientras ( {0} ) hacer", Condicion).AppendLine();
                strBldr.AppendLine(Cuerpo.Gargar);
                strBldr.AppendLine("finmientras;");

                return strBldr.ToString();
            }
        }

        public override void ToXML(Utilidades.XML.XMLCreator xml)
        {
            xml.AddElement();
            xml.SetTitle(string.Format("Mientras{0}",GlobalXMLTags.Instance.CantMientras));

            xml.AddElement();
            xml.SetTitle("Condicion");
            xml.SetValue(Utilidades.XML.XMLReader.Escape(this.Condicion));
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle(string.Format("CuerpoMientras{0}", GlobalXMLTags.Instance.CantMientras));
            this.Cuerpo.ToXML(xml);
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("NombreTipo");
            xml.SetValue("MientrasViewModel");
            xml.LevelUp();            

            
            xml.LevelUp();
        }

        public override void FromXML(Utilidades.XML.XMLElement xmlElem)
        {

            this.Condicion = Utilidades.XML.XMLReader.Unescape(xmlElem.FindFirst("Condicion").value);

            XMLElement xmlCuerpo = xmlElem.FindFirstPrefix("CuerpoMientras");
            if (!Object.Equals(xmlCuerpo, null))
            {
                SecuenciaViewModel cuerpo = new SecuenciaViewModel();
                cuerpo.FromXML(xmlCuerpo);
                Cuerpo = cuerpo;
            }
              
        }
    }
}
