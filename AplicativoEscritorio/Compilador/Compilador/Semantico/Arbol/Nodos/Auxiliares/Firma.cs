﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Semantico.TablaDeSimbolos;

namespace Compilador.Semantico.Arbol.Nodos.Auxiliares
{
    class Firma
    {
        public string Lexema { get; set; }
        public NodoTablaSimbolos.TipoDeDato Tipo { get; set; }
        public int Valor { get; set; }
        public bool EsArreglo { get; set; }


        public Firma(string lexema, NodoTablaSimbolos.TipoDeDato tipo)
        {
            this.Lexema = lexema;
            this.Tipo = tipo;
        }

        public Firma(string lexema, NodoTablaSimbolos.TipoDeDato tipo, int valor)
        {
            this.Lexema = lexema;
            this.Tipo = tipo;
            this.Valor = valor;
        }

        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // safe because of the GetType check
            Firma f = (Firma)obj;

            // use this pattern to compare reference members
            if (Lexema.Equals(f.Lexema))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
