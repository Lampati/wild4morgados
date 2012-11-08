using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoVis: NodoArbolSemantico
    {
        public bool ConSaltoLinea {get;set;}

        public NodoVis(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre, elem)
        {

        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {

        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            if (hijoASintetizar.Lexema.ToUpper() == "VISUALIZARLN")
            {
                this.ConSaltoLinea = true;
            }
            else
            {
                this.ConSaltoLinea = false;
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
