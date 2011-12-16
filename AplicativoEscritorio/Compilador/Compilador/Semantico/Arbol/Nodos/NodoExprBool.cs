using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Sintactico.Gramatica;
using Compilador.Auxiliares;
using Compilador.Semantico.TablaDeSimbolos;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoExprBool : NodoArbolSemantico
    {
        public bool EsPar { get; set; }

        public NodoExprBool(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
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

            if (this.hijosNodo.Count > 2)
            {
                this.Comparacion = TipoComparacion.None;

                this.Lugar = string.Copy(this.hijosNodo[2].Lugar);

                if (this.Lugar == null || this.Lugar.Equals(string.Empty))
                {
                    this.Lugar = string.Copy(this.hijosNodo[2].Valor.ToString());
                }

                
            }
            else
            {
                
                string parte2 = string.Copy(this.hijosNodo[1].Lugar);

                this.TipoDato = this.hijosNodo[0].TipoDato;

                this.Comparacion = this.hijosNodo[1].Comparacion;                


                this.Lugar = string.Copy(this.hijosNodo[0].Lugar);
               
                if (this.Lugar == null || this.Lugar.Equals(string.Empty))
                {
                    this.Lugar = string.Copy(this.hijosNodo[0].Valor.ToString());
                }



            }
            return this;
        }

        public override void ChequearAtributos(Terminal t)
        {
            StringBuilder strbldr;

            if (this.hijosNodo.Count == 2)
            {
                //Si la parte EX no tiene valor pq es lambda, es cuestion de no comprobar nada, y tomar el tipo de dato de EXPR
                if (this.hijosNodo[1].TipoDato != NodoTablaSimbolos.TipoDeDato.Ninguno)
                {

                    if (this.hijosNodo[0].TipoDato != this.hijosNodo[1].TipoDato)
                    {
                        strbldr = new StringBuilder("Se esta intentando comparar una expresion del tipo ").Append(EnumUtils.stringValueOf(this.hijosNodo[0].TipoDato));
                        strbldr.Append(" con una del tipo ").Append(EnumUtils.stringValueOf(this.hijosNodo[1].TipoDato));
                        throw new ErrorSemanticoException(strbldr.ToString());
                    }
                    else
                    {
                        //Entonces lo que se estaba haciendo era una comparacion, pongo que es un dato booleano.
                        this.TipoDato = NodoTablaSimbolos.TipoDeDato.Booleano;
                    }
                }
                else
                {
                    this.TipoDato = this.hijosNodo[0].TipoDato;
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

           

            this.Codigo = strBldr.ToString();
        }
    }
}
