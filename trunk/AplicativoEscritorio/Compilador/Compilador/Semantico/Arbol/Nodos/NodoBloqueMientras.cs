using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

using CompiladorGargar.Auxiliares;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoBloqueMientras: NodoArbolSemantico
    {
        public NodoBloqueMientras(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
       
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

            strBldr.AppendLine(GeneracionCodigoHelpers.AsignarLinea(this.hijosNodo[2].LineaCorrespondiente));

            strBldr.Append("While ");
            strBldr.Append("( ");
            strBldr.Append(this.hijosNodo[2].Codigo);
            strBldr.Append(") ");
            strBldr.AppendLine("do");
            strBldr.AppendLine("begin");
            strBldr.Append("\t").AppendLine(this.hijosNodo[5].Codigo.Replace("\r\n", "\r\n\t"));
            strBldr.AppendLine("end;");

            this.Codigo = strBldr.ToString();
        }
    }
}
