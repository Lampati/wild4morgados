using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoDeclaracionesIni : NodoArbolSemantico
    {
        public NodoDeclaracionesIni(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
               
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico nodoArbolSemantico)
        {
       
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico nodoArbolSemantico)
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

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();
            strBldr.AppendLine("Const");
            strBldr.AppendLine(this.hijosNodo[1].Codigo);
            strBldr.AppendLine("Var");
            strBldr.AppendLine(this.hijosNodo[3].Codigo);

            this.Codigo = strBldr.ToString();
        }
    }
}
