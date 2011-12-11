using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Semantico.TablaDeSimbolos;
using Compilador.Sintactico.Gramatica;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoMaxArregloDec: NodoArbolSemantico
    {
        public NodoMaxArregloDec(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }


        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

    

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.Valor = hijoASintetizar.Valor;
            this.TipoDato = hijoASintetizar.TipoDato;
            
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

        }
    }
}
