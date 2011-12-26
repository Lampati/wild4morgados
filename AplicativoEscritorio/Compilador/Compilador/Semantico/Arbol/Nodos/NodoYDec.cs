using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.TablaDeSimbolos;
using Compilador.Semantico.Arbol.Nodos.Auxiliares;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoYDec : NodoArbolSemantico
    {
        public NodoYDec(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.VariablesACrear = new List<Variable>();
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (this.hijosNodo.Count > 1) //No es lambda en ese caso
            {
                this.EsArreglo = true;
                this.TipoDato = this.hijosNodo[1].TipoDato;
            }
            else
            {
                this.EsArreglo = false;
            }

            return this;
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {            
        }

        public override void ChequearAtributos(Terminal t)
        {
            if (this.EsArreglo)
            {
                if (this.hijosNodo[1].TipoDato != NodoTablaSimbolos.TipoDeDato.Numero)
                {
                    throw new ErrorSemanticoException(new StringBuilder("El subindice del arreglo debe ser natural.").ToString());
                }
            }
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            this.TipoDato = NodoTablaSimbolos.TipoDeDato.Numero;
            return this;
        }
    }
}
