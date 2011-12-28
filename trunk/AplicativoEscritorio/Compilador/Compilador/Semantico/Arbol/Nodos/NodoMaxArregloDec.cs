using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Semantico.Arbol.Nodos
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
            StringBuilder strBldr = new StringBuilder();

            strBldr.Append(this.hijosNodo[0].Lexema);   

            this.Codigo = strBldr.ToString();
        }
    }
}
