using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.Arbol.Nodos.Auxiliares;
using Compilador.Auxiliares;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoF : NodoArbolSemantico
    {
        public NodoF(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.ListaFirma = new List<Firma>();
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            this.ListaFirma.Add(new Firma(this.hijosNodo[0].Lexema,this.hijosNodo[2].TipoDato));

            StringBuilder textoArbol = new StringBuilder();
            textoArbol.Append("Declaracion de parámetro ").Append(this.hijosNodo[0].Lexema);
            textoArbol.Append(" de tipo ").Append(EnumUtils.stringValueOf(this.hijosNodo[2].TipoDato));
            this.TextoParaImprimirArbol = textoArbol.ToString();


            return this;
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
    
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            
            
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
