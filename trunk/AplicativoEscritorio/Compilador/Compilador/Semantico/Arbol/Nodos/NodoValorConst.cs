﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoValorConst : NodoArbolSemantico
    {
        public NodoValorConst(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.TipoDato = hijoASintetizar.TipoDato;
            this.ValorConstanteNumerica = hijoASintetizar.ValorConstanteNumerica;
        }

        public override void ChequearAtributos(Terminal t)
        {
            
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            return this;
        }

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo[0].GetType() == typeof(NodoTerminal))
            {
                strBldr.Append(this.hijosNodo[0].Lexema);
            }
            else
            {
                strBldr.Append(this.hijosNodo[0].Codigo);
            }
            
            this.Codigo = strBldr.ToString();
        }
    }
}
