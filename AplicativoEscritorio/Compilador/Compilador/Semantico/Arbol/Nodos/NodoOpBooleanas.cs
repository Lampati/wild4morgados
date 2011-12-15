using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoOpBooleanas : NodoArbolSemantico
    {
        public NodoOpBooleanas(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        
        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.TipoDato = hijoASintetizar.TipoDato;
            switch (hijoASintetizar.Lexema)
            {
                case "and":
                    this.Operacion = TipoOperatoria.And;
                    break;

                case "or":
                    this.Operacion = TipoOperatoria.Or;
                    break;
            }

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
