using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;


namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoLinea: NodoArbolSemantico
    {
        public NodoLinea(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
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
            this.LlamaProcSalida = this.hijosNodo[0].LlamaProcSalida;
            this.TieneLecturas = this.hijosNodo[0].TieneLecturas ;
            this.LlamaProcs = this.hijosNodo[0].LlamaProcs ;
            this.ModificaParametros = this.hijosNodo[0].ModificaParametros;
            this.AsignaParametros = this.hijosNodo[0].AsignaParametros ;
            

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
            strBldr.AppendLine(this.hijosNodo[0].Codigo);

      
            this.Codigo = strBldr.ToString();
        }
    }
}
