using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Auxiliares;
using Compilador.Semantico.TablaDeSimbolos;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoExprBooleanas : NodoArbolSemantico
    {
        public bool EsPar { get; set; }

        public NodoExprBooleanas(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
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

            //Si el tipo de dato de ExprBoolExtra es booleano, es pq tiene un and, o un or. Por ende, este tambien sera booleano.
            //Tambien le pongo que es ninguna el tipo de operatoria. Cualquiera de los dos sirve.
            

            this.Comparacion = this.hijosNodo[1].Comparacion;
            this.Operacion = this.hijosNodo[1].Operacion;

            this.Lugar = string.Copy(this.hijosNodo[0].Lugar);
               
            if (this.Lugar == null || this.Lugar.Equals(string.Empty))
            {
                this.Lugar = string.Copy(this.hijosNodo[0].Valor.ToString());
            }

            if (this.hijosNodo[1].Operacion != TipoOperatoria.Ninguna)
            {
                this.TipoDato = NodoTablaSimbolos.TipoDeDato.Booleano;

                if (this.hijosNodo[0].TipoDato != NodoTablaSimbolos.TipoDeDato.Booleano || this.hijosNodo[1].TipoDato != NodoTablaSimbolos.TipoDeDato.Booleano)
                {
                    StringBuilder strbldr = new StringBuilder("Los operadores logicos and y or ");
                    strbldr.Append("solo pueden ser usados con expresiones del tipo booleanas");
                    throw new ErrorSemanticoException(strbldr.ToString());
                }
            }
            else
            {
                this.TipoDato = this.hijosNodo[0].TipoDato;
            }

            
            
            return this;
        }

        public override void ChequearAtributos(Terminal t)
        {
            StringBuilder strbldr;

            if (this.hijosNodo.Count == 2)
            {
                //Si el tipo de dato de ExprBoolExtra es booleano, este tambien debera serlo para poder compararlos 
                if (this.hijosNodo[1].TipoDato != NodoTablaSimbolos.TipoDeDato.Ninguno)
                {
                    if (this.hijosNodo[0].TipoDato != this.hijosNodo[1].TipoDato)
                    {
                        strbldr = new StringBuilder("Se esta intentando comparar una expresion del tipo ").Append(EnumUtils.stringValueOf(this.hijosNodo[0].TipoDato));
                        strbldr.Append(" con una del tipo ").Append(EnumUtils.stringValueOf(this.hijosNodo[1].TipoDato));
                        throw new ErrorSemanticoException(strbldr.ToString());
                    }
                }
            }
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            return this;
        }

        public override void CalcularCodigo()
        {
           
        }
    }
}
