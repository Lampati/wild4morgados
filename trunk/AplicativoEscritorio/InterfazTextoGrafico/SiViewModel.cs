using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilidades.XML;

namespace InterfazTextoGrafico
{
    public class SiViewModel : ActividadViewModelBase
    {
        public SecuenciaViewModel BranchVerdadero { get; set; }
        public SecuenciaViewModel BranchFalso { get; set; }
        public string Condicion { get; set; }

        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();

                strBldr.AppendFormat("si ( {0} ) entonces",Condicion).AppendLine();
                strBldr.AppendLine(BranchVerdadero.Gargar);
                if (BranchFalso != null)
                {
                    strBldr.AppendLine(BranchFalso.Gargar);
                }

                strBldr.AppendLine("finsi;");

                return strBldr.ToString();
            }
        }

        public override void ToXML(Utilidades.XML.XMLCreator xml)
        {
            xml.AddElement();
            xml.SetTitle(string.Format("Si{0}",InterfazTextoGrafico.Auxiliares.GlobalXMLTags.Instance.CantSi));

            xml.AddElement();
            xml.SetTitle("Condicion");
            xml.SetValue(Utilidades.XML.XMLReader.Escape(this.Condicion));
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle(string.Format("BranchVerdadero{0}", InterfazTextoGrafico.Auxiliares.GlobalXMLTags.Instance.CantSi));            
            this.BranchVerdadero.ToXML(xml);
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle(string.Format("BranchFalso{0}", InterfazTextoGrafico.Auxiliares.GlobalXMLTags.Instance.CantSi));
            this.BranchFalso.ToXML(xml);
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("NombreTipo");
            xml.SetValue("SiViewModel");
            xml.LevelUp();            


            xml.LevelUp();
        }

        public override void FromXML(Utilidades.XML.XMLElement xmlElem)
        {


            this.Condicion = Utilidades.XML.XMLReader.Unescape(xmlElem.FindFirst("Condicion").value);

            XMLElement xmlBranchVerdadero = xmlElem.FindFirstPrefix("BranchVerdadero");
            if (!Object.Equals(xmlBranchVerdadero, null))
            {
                SecuenciaViewModel cuerpo = new SecuenciaViewModel();
                cuerpo.FromXML(xmlBranchVerdadero);
                BranchVerdadero = cuerpo;
            }

            XMLElement xmlBranchFalso = xmlElem.FindFirstPrefix("BranchFalso");
            if (!Object.Equals(xmlBranchFalso, null))
            {
                SecuenciaViewModel cuerpo = new SecuenciaViewModel();
                cuerpo.FromXML(xmlBranchFalso);
                BranchFalso = cuerpo;
            }
            
        }
    }
}
