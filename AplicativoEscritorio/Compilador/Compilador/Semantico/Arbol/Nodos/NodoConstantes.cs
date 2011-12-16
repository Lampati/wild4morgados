using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoConstantes : NodoArbolSemantico
    {
        public NodoConstantes(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.EsConstante = true;
            this.DeclaracionesPermitidas = TipoDeclaracionesPermitidas.Constantes;
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.TextoParaImprimirArbol = this.ToString();
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
