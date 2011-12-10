using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoTipoDatoConArreglo : NodoArbolSemantico
    {
        public NodoTipoDatoConArreglo(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.TipoDato = hijoASintetizar.TipoDato;          
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (this.hijosNodo.Count > 1)
            {
                this.TipoDato = this.hijosNodo[5].TipoDato;
            }
            else
            {
                this.TipoDato = this.hijosNodo[0].TipoDato;
            }

            return this;
        }

        public override void ChequearAtributos(Terminal t)
        {
           
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            return this;
        }
    }
}
