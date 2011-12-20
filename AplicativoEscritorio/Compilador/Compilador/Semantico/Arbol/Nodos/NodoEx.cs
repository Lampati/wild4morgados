using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Semantico.TablaDeSimbolos;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoEx : NodoArbolSemantico
    {
        public NodoEx(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre, elem)
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
            if (hijosNodo.Count > 1)
            {
                this.TipoDato = this.hijosNodo[1].TipoDato;
                this.Comparacion = this.hijosNodo[0].Comparacion;

                this.AsignaParametros = this.hijosNodo[1].AsignaParametros;

                this.Lugar = string.Copy(this.hijosNodo[1].Lugar);

                if (this.Lugar == null || this.Lugar.Equals(string.Empty))
                {
                    this.Lugar = string.Copy(this.hijosNodo[1].Valor.ToString());
                }

                if (!(this.Comparacion == TipoComparacion.Igual || this.Comparacion == TipoComparacion.Distinto))
                {
                    if (this.TipoDato != NodoTablaSimbolos.TipoDeDato.Numero)
                    {
                        StringBuilder strbldr = new StringBuilder("Los comparadores mayor, mayor igual, menor y menor igual ");
                        strbldr.Append("solo pueden ser usados con expresiones del tipo numericas");
                        throw new ErrorSemanticoException(strbldr.ToString());
                    }

                }

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
                strBldr.Append(this.hijosNodo[1].Codigo);
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
