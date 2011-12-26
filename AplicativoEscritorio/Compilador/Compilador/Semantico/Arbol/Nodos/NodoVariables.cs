using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoVariables : NodoArbolSemantico
    {
        public NodoVariables(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.EsConstante = false;
            this.DeclaracionesPermitidas = TipoDeclaracionesPermitidas.Variables;
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

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo.Count > 1)
            {
                strBldr.Append(this.hijosNodo[1].Codigo).Append(" ");
                strBldr.AppendLine(";");
                strBldr.Append(this.hijosNodo[3].Codigo);
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
