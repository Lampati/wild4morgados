using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.Arbol.Nodos.Auxiliares;
using CompiladorGargar.Auxiliares;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoF : NodoArbolSemantico
    {
        public NodoF(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.ListaFirma = new List<Firma>();
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {
            Firma f = new Firma(this.hijosNodo[0].Lexema, this.hijosNodo[2].TipoDato);
            f.EsArreglo = this.hijosNodo[2].EsArreglo;

            this.ListaFirma.Add(f);

            return this;
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            hijoAHeredar.EsFirma = true;
    
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

            strBldr.Append(this.hijosNodo[0].LexemaVariable).Append(" "); // id
            strBldr.Append(":").Append(" "); // :
            strBldr.Append(this.hijosNodo[2].Codigo).Append(" "); // tipo
            

            this.Codigo = strBldr.ToString();
        }
    }
}
