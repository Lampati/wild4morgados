using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using System.Globalization;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoNumero : NodoArbolSemantico
    {
        public NodoNumero(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.TipoDato = hijoASintetizar.TipoDato;

            this.ValorConstanteNumerica = Convert.ToDouble(hijoASintetizar.Lexema, new CultureInfo("en-US"));
            this.Lexema = hijoASintetizar.Lexema;
            
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
            strBldr.Append(this.hijosNodo[0].Lexema);
            this.Codigo = strBldr.ToString();
        }
    }
}
