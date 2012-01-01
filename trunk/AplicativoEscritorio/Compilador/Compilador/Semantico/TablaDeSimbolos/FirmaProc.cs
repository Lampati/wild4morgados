using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar.Semantico.TablaDeSimbolos
{
    internal class FirmaProc
    {

        public NodoTablaSimbolos.TipoDeDato TipoDato { get; set; }
        public bool EsArreglo { get; set; }
        public string Lexema { get; set; }

        public FirmaProc(string nombre, NodoTablaSimbolos.TipoDeDato tipo, bool arr)
        {
            TipoDato = tipo;
            EsArreglo = arr;
            Lexema = nombre;
        }
    }
}
