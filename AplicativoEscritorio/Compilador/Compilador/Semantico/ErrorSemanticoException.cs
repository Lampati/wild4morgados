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

        public ErrorSemanticoException(string desc)
            : base(desc)
        {
            this.Tipo = "Semantico";

            //Cambiado a partir de ahora, toma de global
            this.Fila = Global.UltFila;
            this.Columna = Global.UltCol;


        }
    }
}
