using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compilador.Semantico.TablaDeSimbolos;
using Compilador.Sintactico.Gramatica;

namespace Compilador.Semantico.Arbol.Nodos
{
    class NodoY: NodoArbolSemantico
    {
        public NodoY(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }


        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
            
        }

        public override NodoArbolSemantico CalcularAtributos(Terminal t)
        {            
      

            this.EsFuncion = false;
            this.EsArreglo = false;

            if (this.hijosNodo.Count > 1)
            {
                string tipoEntrada = this.hijosNodo[0].Lexema;

                this.Lugar = this.hijosNodo[1].Lugar;

                switch (tipoEntrada)
                {
                    case "(":
                        this.EsFuncion = true;
                        this.ListaFirma = this.hijosNodo[1].ListaFirma;
                        break;

                    case "[":
                        this.EsArreglo = true;
                        this.IndiceArreglo = this.hijosNodo[1].Valor;
                        this.TipoDato = this.hijosNodo[1].TipoDato;
                        break;
                }
            }

            return this;
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            
            
        }

        public override void ChequearAtributos(Terminal t)
        {
            if (this.EsArreglo)
            {
                if (this.TipoDato != NodoTablaSimbolos.TipoDeDato.Numero)
                {
                    throw new ErrorSemanticoException(new StringBuilder("El indice del arreglo debe ser un numero.").ToString());
                }
            }
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
                if (this.EsArreglo)
                {
                    strBldr.Append("[");
                    strBldr.Append(this.hijosNodo[1].Codigo);
                    strBldr.Append("]");
                }
                else
                {
                    strBldr.Append("(");
                    strBldr.Append(this.hijosNodo[1].Codigo);
                    strBldr.Append(")");
                }
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
