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

                //Si le pongo un and, o le pongo un or, termina siendo siempre un tipo booleano el resultante.
                //this.TipoDato = this.hijosNodo[0].TipoDato;
                this.TipoDato = NodoTablaSimbolos.TipoDeDato.Booleano;

                this.Lugar = string.Copy(this.hijosNodo[1].Lugar);

                if (this.Lugar == null || this.Lugar.Equals(string.Empty))
                {
                    this.Lugar = string.Copy(this.hijosNodo[1].Valor.ToString());
                }

                if (this.hijosNodo[1].TipoDato != NodoTablaSimbolos.TipoDeDato.Booleano)
                {
                    StringBuilder strbldr = new StringBuilder("Los operadores logicos and y or ");
                    strbldr.Append("solo pueden ser usados con expresiones del tipo booleanas");
                    throw new ErrorSemanticoException(strbldr.ToString());
                }

                this.AsignaParametros = this.hijosNodo[1].AsignaParametros || this.hijosNodo[2].AsignaParametros;
            }
            else
            {
                this.Operacion = TipoOperatoria.Ninguna;
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
                strBldr.Append(" ( ");
                strBldr.Append(this.hijosNodo[1].Codigo);
                strBldr.Append(" ) ");
                strBldr.Append(this.hijosNodo[2].Codigo);

                
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
