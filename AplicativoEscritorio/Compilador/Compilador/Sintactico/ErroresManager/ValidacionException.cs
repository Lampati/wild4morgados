using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar.Sintactico.ErroresManager
{
    internal class ValidacionException : Exception
    {
        public ValidacionException(string m) : base(m)
        {
          
        }
    }
}
