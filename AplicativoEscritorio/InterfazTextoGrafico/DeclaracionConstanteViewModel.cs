using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace InterfazTextoGrafico
{
    public class DeclaracionConstanteViewModel : ActividadViewModelBase
    {
        public string Nombre {get; set;}
        public InterfazTextoGrafico.Enums.TipoDato Tipo {get; set;}
        public string Valor {get; set;}

    

          public override string  NombreActividad
        {
            get { return "DeclaracionConstante"; }
        }

        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();

                switch (Tipo)
                {
                    case InterfazTextoGrafico.Enums.TipoDato.Numero:
                        strBldr.AppendFormat("const {0} : {1} = {2};", Nombre, Tipo.ToString(), Valor).AppendLine();
                        break;
                    case InterfazTextoGrafico.Enums.TipoDato.Texto:
                        strBldr.AppendFormat("const {0} : {1} = {2};", Nombre, Tipo.ToString(), Valor).AppendLine();
                        break;
                    case InterfazTextoGrafico.Enums.TipoDato.Booleano:
                        strBldr.AppendFormat("const {0} : {1} = {2};", Nombre, Tipo.ToString(), Valor).AppendLine();
                        break;
                }

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

        public override void CalcularLineas(int linea)
        {
            lineaComienzo = linea;



            int cantLineas = Utilidades.StringUtils.CantidadDeLineas(Gargar) - 1;

            lineaFinal = lineaComienzo + cantLineas;
        }

        public override void ToXML(Utilidades.XML.XMLCreator xml)
        {
            xml.AddElement();
            xml.SetTitle("DeclaracionConstante");

            xml.AddElement();
            xml.SetTitle("Nombre");
            xml.SetValue(Utilidades.XML.XMLReader.Escape(Nombre));
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("Tipo");
            xml.SetValue(((int)Tipo).ToString());
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("Valor");
            xml.SetValue(Utilidades.XML.XMLReader.Escape(Valor));
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("NombreTipo");
            xml.SetValue("DeclaracionConstanteViewModel");
            xml.LevelUp();            


            xml.LevelUp();
        }

        public override void FromXML(Utilidades.XML.XMLElement xmlElem)
        {

            this.Nombre = Utilidades.XML.XMLReader.Unescape(xmlElem.FindFirst("Nombre").value);
            this.Tipo = (InterfazTextoGrafico.Enums.TipoDato)int.Parse(xmlElem.FindFirst("Tipo").value);
            this.Valor = Utilidades.XML.XMLReader.Unescape(xmlElem.FindFirst("Valor").value);
            
        }
    }
}
