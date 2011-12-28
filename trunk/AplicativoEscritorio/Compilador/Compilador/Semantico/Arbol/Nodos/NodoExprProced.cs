using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.Arbol.Nodos.Auxiliares;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoExprProced: NodoArbolSemantico
    {
        public NodoExprProced(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.ListaFirma = new List<Firma>();
            this.EsPasajeParametrosAProcOFunc = true;
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
    
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (this.hijosNodo.Count > 1)
            {
                this.ListaFirma.Add(new Firma(this.hijosNodo[0].Lexema, this.hijosNodo[0].TipoDato, this.hijosNodo[0].EsArregloEnParametro));
                this.ListaFirma.AddRange(this.hijosNodo[1].ListaFirma);
            }
            return this;
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.ListaFirma.AddRange(hijoASintetizar.ListaFirma);
            
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
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
