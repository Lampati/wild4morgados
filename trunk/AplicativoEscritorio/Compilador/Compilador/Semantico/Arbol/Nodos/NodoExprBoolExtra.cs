using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Auxiliares;
using Compilador.Semantico.TablaDeSimbolos;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoExprBoolExtra : NodoArbolSemantico
    {


        public NodoExprBoolExtra(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
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
                this.Comparacion = TipoComparacion.None;
                this.Operacion = this.hijosNodo[0].Operacion;
                this.TipoDato = this.hijosNodo[0].TipoDato;

                this.Lugar = string.Copy(this.hijosNodo[1].Lugar);

                if (this.Lugar == null || this.Lugar.Equals(string.Empty))
                {
                    this.Lugar = string.Copy(this.hijosNodo[1].Valor.ToString());
                }                
            }
            
            return this;
        }

        public override void ChequearAtributos(Terminal t)
        {
            StringBuilder strbldr;

            if (this.hijosNodo.Count == 2)
            {
                if (this.hijosNodo[0].TipoDato != this.hijosNodo[1].TipoDato)
                {
                    strbldr = new StringBuilder("Se esta intentando comparar una expresion del tipo ").Append(EnumUtils.stringValueOf(this.hijosNodo[0].TipoDato));
                    strbldr.Append(" con una del tipo ").Append(EnumUtils.stringValueOf(this.hijosNodo[1].TipoDato));
                    throw new ErrorSemanticoException(strbldr.ToString(), t.Componente.Fila, t.Componente.Columna);
                }
            }
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            return this;
        }

        public override void CalcularCodigo()
        {
            string parte2 = this.hijosNodo[1].Lugar;

            StringBuilder strBldr = new StringBuilder();

            switch (this.Comparacion)
            {
                case TipoComparacion.None:
                    strBldr.Append(this.hijosNodo[2].Codigo);
                    strBldr.Append(GeneracionCodigoHelpers.GenerarEsPar(this.Lugar));
                    break;
                default:
                    strBldr.Append(this.hijosNodo[0].Codigo);
                    strBldr.Append(this.hijosNodo[1].Codigo);
                    strBldr.Append(GeneracionCodigoHelpers.ExprBool(this.Lugar, parte2));
                    break;
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
