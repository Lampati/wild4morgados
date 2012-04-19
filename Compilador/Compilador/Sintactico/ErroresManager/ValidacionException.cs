using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar.Sintactico.ErroresManager
{
    internal class ValidacionException : Exception
    {
        public int Fila { get; set; }
        public int Columna { get; set; }

        public ValidacionException(string m) : base(m)
        {
          
        }
    }
}
