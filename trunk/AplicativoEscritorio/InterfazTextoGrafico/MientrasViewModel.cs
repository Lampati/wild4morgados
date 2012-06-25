using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        }

        public override void FromXML(Utilidades.XML.XMLElement xmlElem)
        {
        }
    }
}
