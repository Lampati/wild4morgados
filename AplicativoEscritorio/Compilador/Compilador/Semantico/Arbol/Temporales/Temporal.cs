using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compilador.Semantico.Arbol.Temporales
{
    public class Temporal
    {
        public string Nombre { get; set; }
        public string NombreProc { get; set; }
        public string NodoCreador { get; set; }
        public string Valor { get; set; }

        public Temporal()
        {

        }

        public override string ToString()
        {
            StringBuilder strbldr = new StringBuilder();
            strbldr.Append(Nombre).Append(", ");
            strbldr.Append(NombreProc).Append(", ");
            strbldr.Append(NodoCreador);

            return strbldr.ToString();
        }
    }
}
