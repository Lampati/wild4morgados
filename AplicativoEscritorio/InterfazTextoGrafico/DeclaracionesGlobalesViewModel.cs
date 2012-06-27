using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfazTextoGrafico
{
    public class DeclaracionesGlobalesViewModel  : ActividadViewModelBase
    {
        public SecuenciaViewModel ConstantesGlobales { get; set; }
        public SecuenciaViewModel VariablesGlobales { get; set; }

        public override string Gargar
        {
            get
            {
                StringBuilder strBldr = new StringBuilder();


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

        public override void CalcularLineas(int linea)
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
