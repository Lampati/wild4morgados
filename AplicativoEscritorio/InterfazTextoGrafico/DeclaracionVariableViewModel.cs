using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfazTextoGrafico
{
    public class DeclaracionVariableViewModel  : ActividadViewModelBase
    {
        public DeclaracionVariableViewModel()
            : base()
        {

        }

        public DeclaracionVariableViewModel(long p)
            : base(p)
        {
            
        }

        public string Nombre { get; set; }
        public InterfazTextoGrafico.Enums.TipoDato Tipo { get; set; }

     

        public override string  NombreActividad
        {
            get { return "DeclaracionVariable"; }
        }

        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();

                strBldr.AppendFormat("var {0} : {1} ;", Nombre, Tipo.ToString()).AppendLine();

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

        public override void CalcularLineasYAsignarContextoAHijos(int linea, string nombreContexto)
        {
            lineaComienzo = linea;

            int cantLineas = Utilidades.StringUtils.CantidadDeLineas(Gargar) - 1;

            contexto = nombreContexto;

            lineaFinal = lineaComienzo + cantLineas;
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
