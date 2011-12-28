using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar.Sintactico
{
    public class ErrorSintacticoException : ErrorCompilacionException 
    {
        public bool descartarTopePila { get; set; }
        public bool descartarTopeCadena { get; set; }
        public bool pararAnalisis { get; set; }

        public ErrorSintacticoException(string desc, int f, int c, bool descartarPila, bool descartarCadena, bool parar ) 
            : base(desc, f, c)
        {
            this.Tipo = "Sintactico";
            this.descartarTopePila = descartarPila;
            this.descartarTopeCadena = descartarCadena;
            this.pararAnalisis = parar;
        }
    }
}
