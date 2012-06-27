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
                strBldr.Append(Cuerpo.Gargar);
                strBldr.AppendLine("finmientras;");

                return strBldr.ToString();
            }
        }

        public override void CalcularLineas(int linea)
        {
            lineaComienzo = linea;

            int lineaAux = lineaComienzo;

            //aumento 1 por la linea de la condicion del mientras
            lineaAux++;

            if (Cuerpo != null)
            {
                Cuerpo.CalcularLineas(lineaAux);
                lineaAux = Cuerpo.LineaFinal;
            }

            //aumento 1 por la linea del finmientras
            lineaAux++;

            lineaFinal = lineaAux;
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

     
                if (Cuerpo != null)
                {
                    strBldr.AppendLine(Cuerpo.DescripcionLineas);
                }

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

        public override ActividadViewModelBase EncontrarActividadPorLinea(int lineaABuscar)
        {
           

            if (Cuerpo != null)
            {
                if (Cuerpo.LineaComienzo <= lineaABuscar && lineaABuscar <= Cuerpo.LineaFinal)
                {
                    return Cuerpo.EncontrarActividadPorLinea(lineaABuscar);
                }
            }

            //Si no era ninguno de sus subhijos es pq es algo de esta actividad sin subHijos
            return this;
        }
    }
}
