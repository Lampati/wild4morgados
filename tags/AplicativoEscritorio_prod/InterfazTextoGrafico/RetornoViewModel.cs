using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfazTextoGrafico
{
    public class RetornoViewModel : ActividadViewModelBase
    {
        public string Expresion { get; set; }

        public override string NombreActividad
        {
            get { return ""; }
        }

        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();
                //esta bien esto aca?? como obtengo el nombre de la funcion aca?
                strBldr.AppendFormat("|NOMBRE_FUNC|; {0}", this.Expresion);

                return strBldr.ToString();
            }
        }

        public override string DescripcionLineas
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();

             

                return strBldr.ToString();
            }
        }

        public override ActividadViewModelBase EncontrarActividadPorLinea(int lineaABuscar)
        {
            return null;
        }

        public override void CalcularLineasYAsignarContextoAHijos(int linea, string nombreContexto)
        {
        }

        public override void ToXML(Utilidades.XML.XMLCreator xml)
        {
        }

        public override void FromXML(Utilidades.XML.XMLElement xmlElem)
        {
        }
    }
}
