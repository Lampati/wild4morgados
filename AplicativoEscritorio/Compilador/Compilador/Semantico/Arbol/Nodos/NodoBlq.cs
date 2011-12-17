using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoBlq: NodoArbolSemantico
    {
        public NodoBlq(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {

        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (this.hijosNodo.Count > 1)
            {
                this.LlamaProcSalida = this.hijosNodo[0].LlamaProcSalida || this.hijosNodo[1].LlamaProcSalida;
                this.TieneLecturas = this.hijosNodo[0].TieneLecturas || this.hijosNodo[1].TieneLecturas;
                this.LlamaProcs = this.hijosNodo[0].LlamaProcs || this.hijosNodo[1].LlamaProcs;
                this.ModificaParametros = this.hijosNodo[0].ModificaParametros || this.hijosNodo[1].ModificaParametros;
                this.AsignaParametros = this.hijosNodo[0].AsignaParametros || this.hijosNodo[1].AsignaParametros;
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
                strBldr.Append(this.hijosNodo[0].Codigo);
                strBldr.Append(this.hijosNodo[1].Codigo);

                this.Codigo = strBldr.ToString();
            }
            else
            {
                this.Codigo = string.Empty;
            }
        }

    }
}
