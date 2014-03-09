using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfazTextoGrafico
{
    public class LlamarProcedimientoViewModel : ActividadViewModelBase
    {

        public string NombreProcedimiento { get; set; }
        public string Parametros { get; set; }

        public LlamarProcedimientoViewModel(long id)
        : base(id)
        {

        }

        public LlamarProcedimientoViewModel()
            : base()
        {

        }

        public override string NombreActividad
        {
            get { return "LlamarProcedimiento"; }
        }

        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();

                strBldr.AppendFormat("llamar {0} ( {1} );", this.NombreProcedimiento, this.Parametros).AppendLine();

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
            xml.SetTitle("LlamarProcedimiento");

            xml.AddElement();
            xml.SetTitle("NombreTipo");
            xml.SetValue("LlamarProcedimientoViewModel");
            xml.LevelUp();     

            xml.AddElement();
            xml.SetTitle("NombreProcedimiento");
            xml.SetValue(Utilidades.XML.XMLReader.Escape(NombreProcedimiento));
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("Parametros");
            xml.SetValue(Utilidades.XML.XMLReader.Escape(Parametros));
            xml.LevelUp();

               


            xml.LevelUp();
        }

        public override void CalcularLineasYAsignarContextoAHijos(int linea, string nombreContexto)
        {
            lineaComienzo = linea;

            int cantLineas = Utilidades.StringUtils.CantidadDeLineas(Gargar) - 1;

            contexto = nombreContexto;

            lineaFinal = lineaComienzo + cantLineas;
        }

        public override void FromXML(Utilidades.XML.XMLElement xmlElem)
        {

            this.NombreProcedimiento = Utilidades.XML.XMLReader.Unescape(xmlElem.FindFirst("NombreProcedimiento").value);
            this.Parametros = Utilidades.XML.XMLReader.Unescape(xmlElem.FindFirst("Parametros").value);
            
        }
    }
}
