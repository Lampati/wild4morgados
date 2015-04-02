﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoIDSolo: NodoArbolSemantico
    {
        public NodoIDSolo(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

     
        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.Lexema = hijoASintetizar.Lexema;
            this.NombreContextoLocal = hijoASintetizar.Lexema;
        }

      
    }
}
