using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Auxiliares;
using Compilador.Semantico.TablaDeSimbolos;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoBool : NodoArbolSemantico
    {
        public NodoBool(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.Lexema = hijoASintetizar.Lexema;
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
            string valor;
            switch (this.hijosNodo[0].Lexema.ToLower())
            {
                case "verdadero":
                    valor = "true";
                    break;
                case "falso":
                    valor = "false";
                    break;               
                default:
                    valor = string.Empty;
                    break;
            }
            strBldr.Append(valor);
            this.Codigo = strBldr.ToString();
        }
    }
}
