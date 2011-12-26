using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoTipoDatoConArregloParam : NodoArbolSemantico
    {
        public NodoTipoDatoConArregloParam(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
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
                this.TipoDato = this.hijosNodo[2].TipoDato;
                this.EsArreglo = true;
            }
            else
            {
                this.TipoDato = this.hijosNodo[0].TipoDato;
                this.EsArreglo = false;
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

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo.Count > 1)
            {
                
                strBldr.Append("Array ");
                
                strBldr.Append(" of ");
                strBldr.Append(this.hijosNodo[2].Codigo);
                
            }
            else
            {
                strBldr.Append(this.hijosNodo[0].Codigo);
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
