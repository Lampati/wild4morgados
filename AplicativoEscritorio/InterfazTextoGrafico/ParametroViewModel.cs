using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfazTextoGrafico.Enums;
using Utilidades.XML;

namespace InterfazTextoGrafico
{
    public class ParametroViewModel : ActividadViewModelBase
    {
        public string Nombre { get; set; }
        public InterfazTextoGrafico.Enums.TipoDato Tipo { get; set; }
        public bool EsArreglo { get; set; }
        public string TopeArreglo { get; set; }

      
         public ParametroViewModel(long id)
            : base(id)
        {

        }

         public ParametroViewModel()
            : base()
        {

        }

        public override string NombreActividad
        {
            get { return ""; }
        }

        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();

                strBldr.AppendFormat("{0} : ",Nombre);


                if (EsArreglo)
                {
                    strBldr.AppendFormat("arreglo [{0}] de {1} ",TopeArreglo,Tipo.ToString());
                }
                else
                {
                    strBldr.AppendFormat("{0} ", Tipo.ToString());
                }

                return strBldr.ToString();
            }
        }


       
        public override void ToXML(Utilidades.XML.XMLCreator xml)
        {
            xml.AddElement();
            xml.SetTitle("Procedimiento");

            xml.AddElement();
            xml.SetTitle("Nombre");
            xml.SetValue(this.Nombre);            
            xml.LevelUp();

            xml.AddElement();
            xml.SetTitle("Tipo");
            xml.SetValue(((int)this.Tipo).ToString());
            xml.LevelUp();

            xml.LevelUp();
        }

        public override void FromXML(Utilidades.XML.XMLElement xmlElem)
        {

           

            this.Nombre = xmlElem.FindFirst("Nombre").value;
           
        }

        public override void CalcularLineasYAsignarContextoAHijos(int linea, string nombreContexto)
        {
           
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
           
            return null;
            
        }
        
    }
}
