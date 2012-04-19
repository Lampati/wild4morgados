using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.TablaDeSimbolos;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoReferencia : NodoArbolSemantico
    {
        public bool EsConRef { get; set; }

        public NodoReferencia(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {

            //if (this.hijosNodo[0].Lexema.ToUpper().Equals("REF"))
            //{
            //    EsConRef = true;
            //    //this.Lexema = this.hijosNodo[0].Lexema;
            //}

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

            if (EsConRef)
            {
                strBldr.Append(this.Lexema);
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
