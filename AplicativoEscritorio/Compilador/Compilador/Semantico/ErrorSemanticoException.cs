using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compilador.Semantico
{
    class ErrorSemanticoException : ErrorCompilacionException
    {
        public ErrorSemanticoException(string desc, int f, int c) 
            : base(desc, f, c)
        {
            this.Tipo = "Semantico";

           
        }
    }
}
