using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilidades.XML;

namespace InterfazTextoGrafico
{
    public class MostrarViewModel : ActividadViewModelBase
    {
        public string ElementosAMostrar { get; set; }
        public bool ConPausa { get; set; }


        public override string NombreActividad
        {
            get { return "Mostrar"; }
        }

        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();

                if (ConPausa)
                {
                    strBldr.AppendFormat("MostrarP({0});",ElementosAMostrar).AppendLine();
                }
                else
                {
                    strBldr.AppendFormat("Mostrar({0});", ElementosAMostrar).AppendLine();
                }

                return strBldr.ToString();
            }
        }

        public override void ToXML(Utilidades.XML.XMLCreator xml)
        {
            xml.AddElement();
            xml.SetTitle("Mostrar");

            xml.AddElement();
            xml.SetTitle("ElementosAMostrar");
            xml.SetValue(Utilidades.XML.XMLReader.Escape(ElementosAMostrar));
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("ConPausa");
            xml.SetValue(((bool)ConPausa).ToString());
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("NombreTipo");
            xml.SetValue("MostrarViewModel");
            xml.LevelUp();            


            xml.LevelUp();
        }

        public override void CalcularLineasYAsignarContextoAHijos(int linea, string nombreContexto)
        {
            lineaComienzo = linea;

            contexto = nombreContexto;

            int cantLineas = Utilidades.StringUtils.CantidadDeLineas(Gargar) - 1;

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

        public override void FromXML(Utilidades.XML.XMLElement xmlElem)
        {

            this.ElementosAMostrar = Utilidades.XML.XMLReader.Unescape(xmlElem.FindFirst("ElementosAMostrar").value);
            this.ConPausa = bool.Parse(xmlElem.FindFirst("ConPausa").value);

            
        }
    }
}
