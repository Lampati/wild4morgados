using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.Arbol.Nodos.Auxiliares;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoExprProc: NodoArbolSemantico
    {
        public NodoExprProc(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.ListaFirma = new List<Firma>();
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
    
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            if (this.hijosNodo.Count > 1)
            {
                /*
                if (this.hijosNodo[1].Temporal == null)
                {
                    this.ListaFirma.Add(new Firma(this.hijosNodo[1].Lexema, this.hijosNodo[1].TipoDato, this.hijosNodo[1].Valor));
                }
                else
                {
                    this.ListaFirma.Add(new Firma(this.hijosNodo[1].Temporal.Nombre, this.hijosNodo[1].TipoDato, this.hijosNodo[1].Valor));
                }
                 * */

                this.ListaFirma.Add(new Firma(this.hijosNodo[1].Lugar, this.hijosNodo[1].TipoDato, this.hijosNodo[1].Valor));
                this.ListaFirma.AddRange(this.hijosNodo[2].ListaFirma);
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
                strBldr.Append(", ");
                strBldr.Append(this.hijosNodo[1].Codigo);
                strBldr.Append(this.hijosNodo[2].Codigo);
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
