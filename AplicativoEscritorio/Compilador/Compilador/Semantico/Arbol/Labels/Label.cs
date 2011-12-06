using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compilador.Semantico.Arbol.Labels
{
    public class CodeLabel
    {

        public string Nombre { get; set; }

        public CodeLabel(string n)
        {
            this.Nombre = n;
        }

        public override string ToString()
        {
            return this.Nombre;
        }
    }
}
