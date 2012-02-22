using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.TablaDeSimbolos;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoVisualizar : NodoArbolSemantico
    {
        public bool ConSaltoLinea { get; set; }

        public NodoVisualizar(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            LineaCorrespondiente = GlobalesCompilador.UltFila;

            this.ListaElementosVisualizar = this.hijosNodo[2].ListaElementosVisualizar;

            return this;
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

            strBldr.AppendLine(GeneracionCodigoHelpers.AsignarLinea(LineaCorrespondiente));

            strBldr.Append("WriteLn ");
            strBldr.Append("( ");
            strBldr.Append(this.hijosNodo[2].Codigo);
            strBldr.Append(") ");
            strBldr.Append(";");

            this.Codigo = strBldr.ToString();
        }
    }
}
