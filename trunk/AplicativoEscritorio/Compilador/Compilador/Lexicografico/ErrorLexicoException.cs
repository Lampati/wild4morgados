using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar.Lexicografico
{
    class ErrorLexicoException : ErrorCompilacionException
    {

        public ErrorLexicoException(string desc, int f, int c)
            : base(desc, f, c)
        {
            this.Tipo = "Lexico";
        }
    }
}
